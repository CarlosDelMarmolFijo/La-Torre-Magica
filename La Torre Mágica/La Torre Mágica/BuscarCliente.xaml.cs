using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace La_Torre_Mágica
{
    public partial class BuscarCliente : Window
    {
        private static FirebaseClient _firebaseClient;

        public BuscarCliente()
        {
            InitializeComponent();
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            _firebaseClient = new FirebaseClient("https://la-torre-magica-default-rtdb.firebaseio.com/");
        }

        private async void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener el nombre completo del cliente ingresado
                string nombreCompleto = TextBoxNombreCliente.Text.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(nombreCompleto))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del cliente.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Buscar al cliente en Realtime Database
                string clienteInfo = await GetClienteInfoAsync(nombreCompleto);

                if (clienteInfo != null)
                {
                    // Mostrar la información del cliente
                    TextBoxInformacionCliente.Text = clienteInfo;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    TextBoxInformacionCliente.Clear();
                }
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje de error
                MessageBox.Show($"Se ha producido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana de búsqueda
            this.Close();
        }

        private async Task<string> GetClienteInfoAsync(string nombreCompleto)
        {
            try
            {
                // Buscar al cliente en Realtime Database
                var clientes = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<dynamic>();

                foreach (var cliente in clientes)
                {
                    // Obtener el nombre y apellido del cliente en la base de datos
                    string nombreClienteEnDb = cliente.Object.Nombre?.ToString().ToLower();
                    string apellidoClienteEnDb = cliente.Object.Apellido?.ToString().ToLower();

                    // Comprobar si el nombre completo o solo el apellido coinciden
                    if (nombreCompleto == $"{nombreClienteEnDb} {apellidoClienteEnDb}" ||
                        nombreCompleto == apellidoClienteEnDb)
                    {
                        // Obtener el valor de TotalPrecio y transformarlo para mostrar con dos decimales
                        decimal totalPrecioDecimal = ConvertToDecimal(cliente.Object.TotalPrecio.ToString());
                        string formattedTotalPrecio = FormatTotalPrecio(totalPrecioDecimal);

                        // Obtener los puntos y nivel
                        decimal puntos = ConvertToDecimal(cliente.Object.Puntos.ToString());
                        string nivel = cliente.Object.Nivel?.ToString() ?? "N/A";

                        // Verificar si existe el campo "Descuento"
                        string descuentoFormateado;
                        if (cliente.Object.Descuento != null)
                        {
                            // Si existe el campo "Descuento", usar su valor
                            descuentoFormateado = cliente.Object.Descuento.ToString();
                        }
                        else
                        {
                            // Si no existe, calcular el descuento en base al nivel y los puntos
                            decimal descuentoDecimal = CalculateDescuento(nivel, puntos);
                            descuentoFormateado = FormatTotalPrecio(descuentoDecimal);
                        }

                        // Mensajes de depuración
                        Console.WriteLine($"Nombre: {cliente.Object.Nombre}, Descuento: {descuentoFormateado}€");

                        // Construir el string de información del cliente
                        return $"-- Datos Personales --\n\n" +
                               $"Nombre: {cliente.Object.Nombre}\n" +
                               $"Apellido: {cliente.Object.Apellido}\n" +
                               $"Teléfono: {cliente.Object.Telefono}\n" +
                               $"Dirección: {cliente.Object.Direccion}\n" +
                               $"Código Postal: {cliente.Object.CodigoPostal}\n" +
                               $"Población: {cliente.Object.Poblacion}\n\n" +
                               $"-- Sistema de Puntos --\n\n" +
                               $"Total Gastado Actual: {formattedTotalPrecio}€\n" +
                               $"Puntos Totales: {puntos}pts\n" +
                               $"Nivel: {nivel}\n" +
                               $"Descuento Aplicable: {descuentoFormateado}€";
                    }
                }

                return null;
            }

            catch (Exception ex)
            {
                // Manejo del error
                MessageBox.Show($"Error al obtener información del cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private async Task ActualizarClienteAsync(string nombre, string apellido, decimal cantidadGastada)
        {
            try
            {
                // Obtener la lista de clientes
                var clientes = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<dynamic>();

                // Encontrar al cliente específico
                var cliente = clientes.FirstOrDefault(c =>
                    c.Object.Nombre.ToString().Trim().Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    c.Object.Apellido.ToString().Trim().Equals(apellido, StringComparison.OrdinalIgnoreCase));

                if (cliente != null)
                {
                    // Obtener datos actuales del cliente
                    decimal totalGastado = ConvertToDecimal(cliente.Object.TotalPrecio.ToString());
                    decimal puntos = ConvertToDecimal(cliente.Object.Puntos.ToString());
                    string nivel = cliente.Object.Nivel?.ToString() ?? "N/A";

                    // Recalcular total gastado, puntos y nivel actualizado
                    totalGastado += cantidadGastada;
                    puntos += CalcularPuntos(cantidadGastada);
                    nivel = CalcularNivel((int)puntos);

                    // Calcular el nuevo descuento
                    decimal nuevoDescuento = CalculateDescuento(nivel, puntos);

                    // Obtener el descuento existente
                    decimal descuentoExistente = ConvertToDecimal(cliente.Object.Descuento?.ToString() ?? "0");

                    // Solo actualizar el descuento si el nuevo descuento calculado es mayor a cero
                    decimal descuentoFinal = nuevoDescuento > 0 ? descuentoExistente + nuevoDescuento : descuentoExistente;

                    // Actualizar los valores en la base de datos
                    await _firebaseClient
                        .Child("usuarios_tienda")
                        .Child(cliente.Key)
                        .PutAsync(new
                        {
                            Nombre = cliente.Object.Nombre,
                            Apellido = cliente.Object.Apellido,
                            Telefono = cliente.Object.Telefono,
                            Direccion = cliente.Object.Direccion,
                            CodigoPostal = cliente.Object.CodigoPostal,
                            Poblacion = cliente.Object.Poblacion,
                            TotalPrecio = FormatTotalPrecio(totalGastado),
                            Puntos = puntos,
                            Nivel = nivel,
                            Descuento = descuentoFinal > 0 ? FormatTotalPrecio(descuentoFinal) : (object)null // Solo actualizar el descuento si es mayor a 0
                        });
                }
                else
                {
                    // Si el cliente no existe, se crea uno nuevo
                    decimal puntosIniciales = CalcularPuntos(cantidadGastada);
                    string nivelNuevo = CalcularNivel((int)puntosIniciales);
                    decimal descuentoNuevo = CalculateDescuento(nivelNuevo, puntosIniciales);

                    await _firebaseClient
                        .Child("usuarios_tienda")
                        .PostAsync(new
                        {
                            Nombre = nombre,
                            Apellido = apellido,
                            Telefono = "N/A", // Asignar valores predeterminados si es necesario
                            Direccion = "N/A",
                            CodigoPostal = "N/A",
                            Poblacion = "N/A",
                            TotalPrecio = FormatTotalPrecio(cantidadGastada),
                            Puntos = puntosIniciales,
                            Nivel = nivelNuevo,
                            Descuento = descuentoNuevo > 0 ? FormatTotalPrecio(descuentoNuevo) : (object)null // Solo agregar el descuento si es mayor a 0
                        });
                }
            }
            catch (Exception ex)
            {
                // Manejo del error
                MessageBox.Show($"Error al actualizar la información del cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int CalcularPuntos(decimal cantidadGastada)
        {
            // Aquí defines cómo calcular los puntos basados en la cantidad gastada
            return (int)(cantidadGastada * 3); // Ejemplo: 3 puntos por cada unidad de dinero
        }

        private string CalcularNivel(int puntos)
        {
            if (puntos <= 1000)
                return "CATAN";
            else if (puntos <= 2000)
                return "AZUL";
            else
                return "DIXIT";
        }

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

            // Convertir el descuento a entero (en céntimos) y formatear con coma
            return descuento;
        }

        private string FormatTotalPrecio(decimal totalPrecioDecimal)
        {
            // Convertir el decimal a entero (en céntimos)
            long totalPrecioInt = (long)(totalPrecioDecimal * 100);
            return FormatTotalPrecio(totalPrecioInt);
        }

        private string FormatTotalPrecio(long totalPrecioInt)
        {
            // Convertir el entero a una cadena y agregar una coma en la posición correcta
            string totalPrecioStr = totalPrecioInt.ToString();

            if (totalPrecioStr.Length <= 2)
            {
                // Si el valor es menor o igual a 99, agregar ceros a la izquierda
                totalPrecioStr = totalPrecioStr.PadLeft(2, '0');
                return $"0,{totalPrecioStr}";
            }

            // Insertar una coma antes de los dos últimos dígitos
            string integerPart = totalPrecioStr.Substring(0, totalPrecioStr.Length - 2);
            string decimalPart = totalPrecioStr.Substring(totalPrecioStr.Length - 2);

            return $"{integerPart},{decimalPart}";
        }

        private decimal ConvertToDecimal(string value)
        {
            decimal result;
            if (!decimal.TryParse(value.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = 0;
            }
            return result;
        }
    }
}
