namespace Biblioteca.Domain.Model
{
    public class Autor
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }

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
            Id = id;
        }
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new ArgumentException("El apellido no puede estar vacío.");
            }
            Apellido = apellido;
        }

    }
}