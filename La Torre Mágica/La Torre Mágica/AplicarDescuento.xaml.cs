using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace La_Torre_Mágica
{
    public partial class AplicarDescuento : Window
    {
        private FirebaseClient _firebaseClient;

        public AplicarDescuento()
        {
            InitializeComponent();
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            _firebaseClient = new FirebaseClient("https://la-torre-magica-default-rtdb.firebaseio.com/");
        }

        private async void ButtonConsultarDescuento_Click(object sender, RoutedEventArgs e)
        {
            string nombreCompleto = TextBoxNombreCliente.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                string mensaje = "Por favor, ingrese el nombre completo del cliente.";
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.WriteLine(mensaje); // Mostrar en consola
                return;
            }

            // Separar el nombre completo en nombre y apellido
            string[] partesNombre = nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (partesNombre.Length < 2)
            {
                MessageBox.Show("Por favor, ingrese tanto el nombre como el apellido, separados por un espacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string nombre = string.Join(" ", partesNombre.Take(partesNombre.Length - 1)).Trim();
            string apellido = partesNombre.Last().Trim();

            try
            {
                var clientes = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<dynamic>();

                var cliente = clientes.FirstOrDefault(c =>
                    c.Object.Nombre.ToString().Trim().Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    c.Object.Apellido.ToString().Trim().Equals(apellido, StringComparison.OrdinalIgnoreCase));

                if (cliente != null)
                {
                    var clienteData = cliente.Object;
                    string nivel = clienteData.Nivel?.ToString();
                    decimal puntos = decimal.TryParse(clienteData.Puntos?.ToString(), out decimal parsedPuntos) ? parsedPuntos : 0;

                    // Calcular el descuento basado en el nivel y los puntos
                    decimal descuentoCalculado = CalculateDescuento(nivel, puntos);
                    TextBoxDescuentoDisponible.Text = descuentoCalculado.ToString("F2") + "€";
                }
                else
                {
                    string mensaje = "Cliente no encontrado.";
                    MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Console.WriteLine(mensaje); // Mostrar en consola
                }
            }
            catch (Exception ex)
            {
                string mensaje = $"Error al consultar descuento: {ex.Message}";
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(mensaje); // Mostrar en consola
            }
        }

        private async void ButtonConfirmar_Click(object sender, RoutedEventArgs e)
        {
            string nombreCompleto = TextBoxNombreCliente.Text.Trim();
            string descuentoDisponibleStr = TextBoxDescuentoDisponible.Text.Trim();
            string descuentoUsarStr = TextBoxDescuentoUsar.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreCompleto) || string.IsNullOrWhiteSpace(descuentoDisponibleStr))
            {
                MessageBox.Show("Por favor, ingrese el nombre completo del cliente y consulte el descuento disponible.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.WriteLine("Error: Por favor, ingrese el nombre completo del cliente y consulte el descuento disponible.");
                return;
            }

            // Separar nombre y apellido
            string[] partesNombre = nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (partesNombre.Length < 2)
            {
                MessageBox.Show("Por favor, ingrese tanto el nombre como el apellido, separados por un espacio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string nombre = string.Join(" ", partesNombre.Take(partesNombre.Length - 1)).Trim();
            string apellido = partesNombre.Last().Trim();

            decimal descuentoDisponible;

            // Eliminar el símbolo de euro y posibles espacios adicionales
            string descuentoDisponibleSinSimbolo = descuentoDisponibleStr.Replace("€", "").Trim();

            var cultureInfo = new CultureInfo("es-ES");
            if (!decimal.TryParse(descuentoDisponibleSinSimbolo, NumberStyles.AllowDecimalPoint, cultureInfo, out descuentoDisponible))
            {
                string mensaje = $"El descuento disponible no es válido. Valor recibido: '{descuentoDisponibleStr}'";
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.WriteLine($"Error: {mensaje}");
                return;
            }

            decimal descuentoUsar = 0;

            if (CheckBoxUsarTodo.IsChecked == true)
            {
                // Establecer el descuento disponible a 0
                descuentoUsar = descuentoDisponible;
                descuentoDisponible = 0;
            }
            else
            {
                // Eliminar el símbolo de euro y posibles espacios adicionales del valor a usar
                string descuentoUsarSinSimbolo = descuentoUsarStr.Replace("€", "").Trim();

                if (!string.IsNullOrWhiteSpace(descuentoUsarStr) && decimal.TryParse(descuentoUsarSinSimbolo, NumberStyles.AllowDecimalPoint, cultureInfo, out decimal parsedDescuentoUsar))
                {
                    descuentoUsar = parsedDescuentoUsar;
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad válida de descuento a usar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Console.WriteLine("Error: Ingrese una cantidad válida de descuento a usar.");
                    return;
                }

                if (descuentoUsar > descuentoDisponible)
                {
                    MessageBox.Show("No puede usar más descuento del que está disponible.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Console.WriteLine("Error: No puede usar más descuento del que está disponible.");
                    return;
                }

                descuentoDisponible -= descuentoUsar;
            }

            try
            {
                await UpdateDescuentoEnBaseDeDatos(nombre, apellido, descuentoDisponible.ToString("0"));

                MessageBox.Show("Descuento actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                Console.WriteLine("Descuento actualizado correctamente.");

                // Limpiar el TextBox de descuento a usar
                TextBoxDescuentoUsar.Text = string.Empty;

                // Actualizar el TextBox con el nuevo valor de descuento
                TextBoxDescuentoDisponible.Text = descuentoDisponible.ToString("F2") + "€";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar descuento: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Error: Error al actualizar descuento: {ex.Message}");
            }
        }

        // Método para actualizar el campo descuento en la base de datos
        private async Task UpdateDescuentoEnBaseDeDatos(string nombre, string apellido, string descuento)
        {
            try
            {
                var clientes = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<dynamic>();

                var cliente = clientes.FirstOrDefault(c =>
                    c.Object.Nombre.ToString().Trim().Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    c.Object.Apellido.ToString().Trim().Equals(apellido, StringComparison.OrdinalIgnoreCase));

                if (cliente != null)
                {
                    await _firebaseClient
                        .Child("usuarios_tienda")
                        .Child(cliente.Key)
                        .PatchAsync(new { Descuento = descuento });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el campo Descuento: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Error: Error al actualizar el campo Descuento: {ex.Message}"); // Mostrar en consola
            }
        }

        // Método para actualizar el TextBox con el descuento disponible actual
        private async Task UpdateDescuentoDisponible(string nombre, string apellido)
        {
            try
            {
                var clientes = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<dynamic>();

                var cliente = clientes.FirstOrDefault(c =>
                    c.Object.Nombre.ToString().Trim().Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    c.Object.Apellido.ToString().Trim().Equals(apellido, StringComparison.OrdinalIgnoreCase));

                if (cliente != null)
                {
                    var clienteData = cliente.Object;
                    string descuentoStr = clienteData.Descuento?.ToString() ?? "0";
                    TextBoxDescuentoDisponible.Text = descuentoStr + "€";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el descuento disponible: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Error: Error al actualizar el descuento disponible: {ex.Message}"); // Mostrar en consola
            }
        }

        // Método para calcular el descuento basado en el nivel y los puntos
        private decimal CalculateDescuento(string nivel, decimal puntos)
        {
            decimal descuento = 0m;

            // Calcular el descuento según el nivel y los puntos
            switch (nivel?.ToUpper())
            {
                case "CATAN":
                    descuento = puntos * 0.01m; // 1 céntimo por punto
                    break;
                case "AZUL":
                    descuento = puntos * 0.02m; // 2 céntimos por punto
                    break;
                case "TERRAFORMING MARS":
                    descuento = puntos * 0.03m; // 3 céntimos por punto
                    break;
                default:
                    // Si el nivel no es reconocido, no aplicar descuento
                    break;
            }

            return descuento;
        }
    }
}
