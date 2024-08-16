using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace La_Torre_Mágica
{
    public partial class Añadir : Window
    {
        private static FirebaseClient _firebaseClient;

        public event Action DataAdded;

        public Añadir()
        {
            InitializeComponent();
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            _firebaseClient = new FirebaseClient("https://la-torre-magica-default-rtdb.firebaseio.com/");
        }

        private async Task SaveDataToFirebaseAsync()
        {
            try
            {
                // Obtener la parte entera y decimal del precio total
                string totalEntero = TextBoxTotalEntero.Text.Trim();
                string totalDecimal = TextBoxTotalDecimal.Text.Trim();

                // Validar y corregir la longitud de la parte decimal
                if (totalDecimal.Length == 1)
                {
                    totalDecimal += "0"; // Si solo tiene un dígito, agregar un 0 al final
                }
                else if (totalDecimal.Length == 0)
                {
                    totalDecimal = "00"; // Si está vacío, considerar como 00
                }

                string nombre = TextBoxNombre.Text.Trim();
                string apellido = TextBoxApellido.Text.Trim();

                // Comprobar si ya existe un usuario con el mismo nombre y apellido
                var existingUsers = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<Usuario>();

                var userExists = existingUsers.Any(u =>
                    u.Object.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    u.Object.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase));

                if (userExists)
                {
                    MessageBox.Show("Ya existe un cliente con este nombre y apellido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Crear el objeto con los datos a guardar
                var usuario = new Usuario
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Telefono = TextBoxTelefono.Text.Trim(),
                    Direccion = TextBoxDireccion.Text.Trim(),
                    CodigoPostal = TextBoxCodigoPostal.Text.Trim(),
                    Poblacion = TextBoxPoblacion.Text.Trim(),
                    TotalPrecio = $"{totalEntero},{totalDecimal}", // Combinar ambas partes para guardar el total del precio
                    Puntos = TextBoxPuntos.Text.Trim(),
                    Nivel = CalculateNivel(TextBoxPuntos.Text.Trim())
                };

                // Guardar los datos en Firebase
                await _firebaseClient.Child("usuarios_tienda").PostAsync(usuario);

                // Notificar que los datos se han añadido
                DataAdded?.Invoke();

                // Mostrar un mensaje de éxito
                MessageBox.Show("Datos guardados exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error
                MessageBox.Show($"Error al guardar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Error al guardar datos: {ex.Message}");
            }
        }

        private void ValidateFieldsAndCalculatePoints(object sender, TextChangedEventArgs e)
        {
            // Obtener la parte entera y decimal del precio total
            string enteroText = TextBoxTotalEntero.Text.Trim();
            string decimalText = TextBoxTotalDecimal.Text.Trim();

            // Validar y corregir la longitud de la parte decimal
            if (decimalText.Length == 1)
            {
                decimalText += "0"; // Si solo tiene un dígito, agregar un 0 al final
            }
            else if (decimalText.Length == 0)
            {
                decimalText = "00"; // Si está vacío, considerar como 00
            }

            // Limitar enteroText a los primeros dos dígitos para el cálculo de puntos
            string puntosBaseText = enteroText.Length >= 3 ? enteroText.Substring(0, 3) : enteroText;

            // Intentar convertir los dos primeros dígitos a un número entero para calcular los puntos
            if (int.TryParse(puntosBaseText, out int puntosBase))
            {
                // Calcular los puntos basados en los dos primeros dígitos de la parte entera
                int puntos = puntosBase * 3; // 3 puntos por cada "decena" de euros

                // Establecer los puntos calculados en el TextBox
                TextBoxPuntos.Text = puntos.ToString();
            }
            else
            {
                // Si la conversión falla, limpiar el TextBox de puntos
                TextBoxPuntos.Text = "0";
            }

            // Validar los campos
            ValidateFields();
        }

        private void ValidateFields()
        {
            // Verificar si todos los campos necesarios están llenos
            bool allFieldsFilled = !string.IsNullOrWhiteSpace(TextBoxNombre.Text) &&
                                   !string.IsNullOrWhiteSpace(TextBoxApellido.Text) &&
                                   !string.IsNullOrWhiteSpace(TextBoxTelefono.Text) &&
                                   !string.IsNullOrWhiteSpace(TextBoxDireccion.Text) &&
                                   !string.IsNullOrWhiteSpace(TextBoxCodigoPostal.Text) &&
                                   !string.IsNullOrWhiteSpace(TextBoxPoblacion.Text) &&
                                   !string.IsNullOrWhiteSpace(TextBoxTotalEntero.Text) &&
                                   !string.IsNullOrWhiteSpace(TextBoxTotalDecimal.Text);

            // Habilitar o deshabilitar el botón "Guardar" basado en el estado de los campos
            ButtonGuardar.IsEnabled = allFieldsFilled;
        }

        private async void ButtonGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Guardar los datos en Firebase
            await SaveDataToFirebaseAsync();

            // Cerrar la ventana después de guardar
            this.Close();
        }

        private string CalculateNivel(string puntosText)
        {
            // Intentar convertir el texto de puntos a un número entero
            if (int.TryParse(puntosText, out int puntos))
            {
                if (puntos <= 1000)
                    return "CATAN";
                else if (puntos <= 2000)
                    return "AZUL";
                else
                    return "DIXIT";
            }

            return "Desconocido";
        }
    }
}
