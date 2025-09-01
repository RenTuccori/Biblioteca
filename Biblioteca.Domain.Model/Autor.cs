namespace Biblioteca.Domain.Model
{
    public class Autor
    {
        private int _id;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;

        public int Id 
        { 
            get => _id; 
            private set => _id = value; 
        }
        
        public string Nombre 
        { 
            get => _nombre; 
            private set => _nombre = value; 
        }
        
        public string Apellido 
        { 
            get => _apellido; 
            private set => _apellido = value; 
        }

        // Constructor sin parámetros para Entity Framework
        protected Autor() { }

        public Autor(int id, string nombre, string apellido)
        {
            SetId(id);
            SetNombre(nombre);
            SetApellido(apellido);
        }

        public void SetId (int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.");
            }
            _id = id;
        }
        
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }
            _nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new ArgumentException("El apellido no puede estar vacío.");
            }
            _apellido = apellido;
        }
    }
}