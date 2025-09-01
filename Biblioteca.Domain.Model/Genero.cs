namespace Biblioteca.Domain.Model
{
    public class Genero
    {
        private int _id;
        private string _nombre = string.Empty;

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

        // Constructor sin parámetros para Entity Framework
        protected Genero() { }

        // El constructor se usa para crear nuevos objetos válidos.
        public Genero(int id, string nombre)
        {
            SetId(id);
            SetNombre(nombre);
        }

        // Métodos para establecer y validar los datos.
        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            _id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del género no puede ser nulo o vacío.", nameof(nombre));
            _nombre = nombre;
        }
    }
}