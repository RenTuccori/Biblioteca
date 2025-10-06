namespace Biblioteca.Domain.Model
{
    public class Editorial
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

        protected Editorial() { }

        public Editorial(int id, string nombre)
        {
            SetId(id);
            SetNombre(nombre);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            _id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la editorial no puede estar vacío.", nameof(nombre));
            _nombre = nombre;
        }
    }
}
