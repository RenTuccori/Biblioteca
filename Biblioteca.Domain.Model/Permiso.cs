namespace Biblioteca.Domain.Model
{
    public class Permiso
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty; // ej: autores.leer
        public string Descripcion { get; private set; } = string.Empty;
        public string Categoria { get; private set; } = string.Empty; // ej: autores
        public bool Activo { get; private set; } = true;

        protected Permiso() { }

        public Permiso(int id, string nombre, string descripcion, string categoria, bool activo = true)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre requerido", nameof(nombre));
            if (string.IsNullOrWhiteSpace(categoria)) throw new ArgumentException("Categoria requerida", nameof(categoria));

            Id = id;
            Nombre = nombre.Trim();
            Descripcion = descripcion?.Trim() ?? string.Empty;
            Categoria = categoria.Trim();
            Activo = activo;
        }

        public override string ToString() => $"{Categoria}.{Nombre}";

        public void Desactivar() => Activo = false;
        public void Activar() => Activo = true;
    }
}
