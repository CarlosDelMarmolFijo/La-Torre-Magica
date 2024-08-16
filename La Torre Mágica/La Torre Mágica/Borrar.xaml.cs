using Firebase.Database;
using System;
using System.Linq;
using System.Windows;

namespace La_Torre_Mágica
{
    public partial class Borrar : Window
    {
        private FirebaseClient _firebaseClient;

        public event Action DataDeleted; // Evento para notificar que los datos se han eliminado

        public Borrar()
        {
            InitializeComponent();
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            // Inicializa el cliente de Firebase con la URL de la base de datos
            _firebaseClient = new FirebaseClient("https://la-torre-magica-default-rtdb.firebaseio.com/");
        }

        private async void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            string nombreCompleto = TextBoxNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombreCompleto))
            {
                MessageBox.Show("Por favor, ingrese un nombre y apellido válidos (separados por un espacio).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Separar el nombre completo en nombre y apellido
            string[] partesNombre = nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (partesNombre.Length < 2)
            {
                MessageBox.Show("Por favor, ingrese el nombre completo y el apellido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string nombre = string.Join(" ", partesNombre.Take(partesNombre.Length - 1)).Trim();
            string apellido = partesNombre.Last().Trim();

            try
            {
                var itemsToDelete = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<Usuario>();

                var itemsMatchingNameAndSurname = itemsToDelete
                    .Where(item =>
                        item.Object.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                        item.Object.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (itemsMatchingNameAndSurname.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún cliente con ese nombre y apellido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                foreach (var item in itemsMatchingNameAndSurname)
                {
                    await _firebaseClient.Child($"usuarios_tienda/{item.Key}").DeleteAsync();
                }

                // Notificar que los datos se han eliminado
                DataDeleted?.Invoke();

                MessageBox.Show("Cliente eliminado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
