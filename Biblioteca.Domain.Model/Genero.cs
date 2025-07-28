namespace Biblioteca.Domain.Model
{
    public class Genero
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

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
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del género no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }
    }
}