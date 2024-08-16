using Firebase.Database;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace La_Torre_Mágica
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Usuario> personas;
        private int currentPage = 1;
        private int itemsPerPage = 30;
        private FirebaseClient _firebaseClient;

        public MainWindow()
        {
            InitializeComponent();
            InitializeFirebase();
            LoadDataAsync();
        }

        private void InitializeFirebase()
        {
            _firebaseClient = new FirebaseClient("https://la-torre-magica-default-rtdb.firebaseio.com/");
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var firebaseData = await _firebaseClient
                    .Child("usuarios_tienda")
                    .OnceAsync<Usuario>();

                personas = new ObservableCollection<Usuario>(firebaseData.Select(item => item.Object));

                // Load the first page of data
                LoadPage(currentPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadPage(int pageNumber)
        {
            if (personas == null)
                return;

            var itemsToShow = personas.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            ListViewDatos.ItemsSource = itemsToShow;

            ButtonPrevious.IsEnabled = pageNumber > 1;
            ButtonNext.IsEnabled = pageNumber * itemsPerPage < personas.Count;
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPage(currentPage);
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage * itemsPerPage < personas.Count)
            {
                currentPage++;
                LoadPage(currentPage);
            }
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            Añadir ventanaAñadir = new Añadir();
            ventanaAñadir.DataAdded += OnDataAdded; // Suscribirse al evento
            ventanaAñadir.ShowDialog(); // Mostrar la ventana como modal
        }

        private async void OnDataAdded()
        {
            // Recargar los datos después de añadir
            await LoadDataAsync();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            Borrar ventanaBorrar = new Borrar();
            ventanaBorrar.DataDeleted += OnDataDeleted; // Suscribirse al evento
            ventanaBorrar.ShowDialog(); // Mostrar la ventana como modal
        }

        private async void OnDataDeleted()
        {
            // Recargar los datos después de eliminar
            await LoadDataAsync();
        }

        private void MenuItemSearch_Click(object sender, RoutedEventArgs e)
        {
            BuscarCliente buscarCliente = new BuscarCliente();
            buscarCliente.ShowDialog();
        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            ActualizarCliente actualizarCliente = new ActualizarCliente();
            actualizarCliente.DataUpdated += OnDataUpdated; // Suscribirse al evento
            actualizarCliente.ShowDialog(); // Mostrar la ventana como modal
        }

        private async void OnDataUpdated()
        {
            // Recargar los datos después de actualizar
            await LoadDataAsync();
        }


        private void MenuItemDescuento_Click(object sender, RoutedEventArgs e)
        {
            AplicarDescuento aplicarDescuento = new AplicarDescuento();
            aplicarDescuento.ShowDialog();
        }
    }
}
