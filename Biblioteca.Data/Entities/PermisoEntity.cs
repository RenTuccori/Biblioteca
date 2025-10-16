namespace Biblioteca.Data.Entities
{
    public class PermisoEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty; // leer, agregar, actualizar, eliminar
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty; // autores, libros, etc.
        public bool Activo { get; set; } = true;

        public ICollection<GrupoPermisoEntity> Grupos { get; set; } = new List<GrupoPermisoEntity>();

        public Biblioteca.Domain.Model.Permiso ToPermiso()
        {
            return new Biblioteca.Domain.Model.Permiso(Id, Nombre, Descripcion, Categoria, Activo);
        }

        public static PermisoEntity FromPermiso(Biblioteca.Domain.Model.Permiso permiso)
        {
            return new PermisoEntity
            {
                Id = permiso.Id,
                Nombre = permiso.Nombre,
                Descripcion = permiso.Descripcion,
                Categoria = permiso.Categoria,
                Activo = permiso.Activo
            };
        }
    }
}
