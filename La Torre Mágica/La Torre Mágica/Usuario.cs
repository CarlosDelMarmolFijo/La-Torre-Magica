using System.ComponentModel;

namespace La_Torre_Mágica
{
    public class Usuario : INotifyPropertyChanged
    {
        private string _nombre;
        private string _apellido;
        private string _telefono;
        private string _direccion;
        private string _codigoPostal;
        private string _poblacion;
        private string _totalPrecio;
        private string _puntos;
        private string _nivel;

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public string Apellido
        {
            get => _apellido;
            set
            {
                if (_apellido != value)
                {
                    _apellido = value;
                    OnPropertyChanged(nameof(Apellido));
                }
            }
        }

        public string Telefono
        {
            get => _telefono;
            set
            {
                if (_telefono != value)
                {
                    _telefono = value;
                    OnPropertyChanged(nameof(Telefono));
                }
            }
        }

        public string Direccion
        {
            get => _direccion;
            set
            {
                if (_direccion != value)
                {
                    _direccion = value;
                    OnPropertyChanged(nameof(Direccion));
                }
            }
        }

        public string CodigoPostal
        {
            get => _codigoPostal;
            set
            {
                if (_codigoPostal != value)
                {
                    _codigoPostal = value;
                    OnPropertyChanged(nameof(CodigoPostal));
                }
            }
        }

        public string Poblacion
        {
            get => _poblacion;
            set
            {
                if (_poblacion != value)
                {
                    _poblacion = value;
                    OnPropertyChanged(nameof(Poblacion));
                }
            }
        }

        public string TotalPrecio
        {
            get => _totalPrecio;
            set
            {
                if (_totalPrecio != value)
                {
                    _totalPrecio = value;
                    OnPropertyChanged(nameof(TotalPrecio));
                }
            }
        }

        public string Puntos
        {
            get => _puntos;
            set
            {
                if (_puntos != value)
                {
                    _puntos = value;
                    OnPropertyChanged(nameof(Puntos));
                }
            }
        }

        public string Nivel
        {
            get => _nivel;
            set
            {
                if (_nivel != value)
                {
                    _nivel = value;
                    OnPropertyChanged(nameof(Nivel));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
