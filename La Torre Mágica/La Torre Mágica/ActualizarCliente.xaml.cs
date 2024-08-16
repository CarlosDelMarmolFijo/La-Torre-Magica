using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace La_Torre_Mágica
{
    public partial class ActualizarCliente : Window
    {
        private static FirebaseClient _firebaseClient;

        // Definición del evento que se lanzará cuando los datos se actualicen
        public event Action DataUpdated;

        public ActualizarCliente()
        {
            InitializeComponent();
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            // Inicializa la conexión a Firebase
            _firebaseClient = new FirebaseClient("https://la-torre-magica-default-rtdb.firebaseio.com/");
        }

        private async void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nombreCompleto = TextBoxNombreCliente.Text.Trim();
            string cantidadGastadaStr = TextBoxCantidadGastada.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreCompleto) || string.IsNullOrWhiteSpace(cantidadGastadaStr))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Separar el nombre completo en nombre y apellido
            string[] partesNombre = nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (partesNombre.Length < 2)
            {
                MessageBox.Show("Por favor, ingrese el nombre completo y el apellido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string nombre = string.Join(" ", partesNombre.Take(partesNombre.Length - 1)).Trim();
            string apellido = partesNombre.Last().Trim();

            if (!decimal.TryParse(cantidadGastadaStr.Replace(",", "."), out decimal cantidadGastada))
            {
                MessageBox.Show("La cantidad ingresada no es válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Buscar el cliente en la base de datos
                var clientes = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<dynamic>();

                var cliente = clientes.FirstOrDefault(c =>
                    c.Object.Nombre.ToString().Trim().Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    c.Object.Apellido.ToString().Trim().Equals(apellido, StringComparison.OrdinalIgnoreCase));

                if (cliente != null)
                {
                    // Leer los datos actuales del cliente
                    var clienteData = cliente.Object;

                    // Obtener el total precio actual
                    string totalPrecioActualStr = clienteData.TotalPrecio?.ToString().Replace(",", ".");
                    decimal totalPrecioActual = decimal.TryParse(totalPrecioActualStr, out decimal parsedTotal) ? parsedTotal : 0;

                    // Ignorar los decimales y convertir la cantidad gastada a entero
                    int cantidadGastadaEntero = (int)Math.Floor(cantidadGastada);

                    // Sumar la cantidad gastada (ignorando decimales)
                    int totalPrecioActualEntero = (int)Math.Floor(totalPrecioActual);
                    int nuevoTotalPrecioEntero = totalPrecioActualEntero + cantidadGastadaEntero;

                    // Formatear el nuevo total precio para mostrar dos decimales
                    string nuevoTotalPrecioStr = (nuevoTotalPrecioEntero / 100.0m).ToString("F2", System.Globalization.CultureInfo.InvariantCulture).Replace(".", ",");

                    // Calcular los puntos adicionales basados en la cantidad gastada
                    int puntosAdicionales = CalcularPuntos(cantidadGastada);
                    int puntosActuales = int.TryParse(clienteData.Puntos?.ToString(), out int parsedPuntos) ? parsedPuntos : 0;
                    int puntosActualizados = puntosActuales + puntosAdicionales;

                    // Calcular el nuevo nivel basado en los puntos actualizados
                    string nivelCalculado = CalcularNivel(puntosActualizados);

                    // Leer el descuento actual del cliente, si existe
                    decimal descuentoActual = 0;
                    bool descuentoExiste = decimal.TryParse(clienteData.Descuento?.ToString().Replace(",", "."), out decimal descuentoParsed);
                    if (descuentoExiste)
                    {
                        descuentoActual = descuentoParsed;
                    }

                    // Calcular el nuevo descuento basado en los puntos adicionales obtenidos por el gasto reciente
                    decimal nuevoDescuento = CalculateDescuento(nivelCalculado, puntosAdicionales);

                    // Sumar el nuevo descuento al descuento existente
                    decimal descuentoActualizado = descuentoActual + nuevoDescuento;

                    // Formatear el descuento actualizado
                    string descuentoActualizadoStr = descuentoActualizado.ToString("F2", System.Globalization.CultureInfo.InvariantCulture).Replace(".", ",");

                    // Crear el objeto de actualización con los campos necesarios
                    var updates = new Dictionary<string, object>
                    {
                        { "TotalPrecio", nuevoTotalPrecioStr },
                        { "Puntos", puntosActualizados.ToString() },
                        { "Descuento", descuentoActualizadoStr }
                    };

                    // Incluir el campo Nivel solo si ha cambiado
                    string nivelActual = clienteData.Nivel?.ToString();
                    if (!string.Equals(nivelActual, nivelCalculado, StringComparison.OrdinalIgnoreCase))
                    {
                        updates["Nivel"] = nivelCalculado;
                    }

                    // Realizar la actualización en Firebase
                    await _firebaseClient
                        .Child("usuarios_tienda")
                        .Child(cliente.Key)
                        .PatchAsync(updates);

                    // Notificar que los datos han sido actualizados
                    DataUpdated?.Invoke();
                    MessageBox.Show("Total precio, puntos y descuento actualizados correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el cliente: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Error al actualizar el cliente: {ex.ToString()}");
            }
        }

        // Método para calcular los puntos basados en la cantidad gastada
        private int CalcularPuntos(decimal cantidadGastada)
        {
            int parteEntera = (int)Math.Floor(cantidadGastada);
            string parteEnteraStr = parteEntera.ToString();
            string puntosBaseText = parteEnteraStr.Length > 2 ? parteEnteraStr.Substring(0, parteEnteraStr.Length - 2) : parteEnteraStr;
            return int.TryParse(puntosBaseText, out int puntosBase) ? puntosBase * 3 : 0;
        }

        // Método para calcular el nivel del cliente basado en los puntos
        private string CalcularNivel(int puntos)
        {
            if (puntos <= 1000)
                return "CATAN";
            else if (puntos <= 2000)
                return "AZUL";
            else
                return "DIXIT";
        }

        // Método para calcular el descuento basado en el nivel y los puntos
        private decimal CalculateDescuento(string nivel, decimal puntos)
        {
            decimal descuento = 0m;

            // Calcular el descuento según el nivel y los puntos
            switch (nivel.ToUpper())
            {
                case "CATAN":
                    descuento = puntos * 0.01m; // 1 céntimo por punto
                    break;
                case "AZUL":
                    descuento = puntos * 0.02m; // 2 céntimos por punto
                    break;
                case "DIXIT":
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