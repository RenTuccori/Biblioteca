using System.Collections.ObjectModel;

namespace Biblioteca.Domain.Model
{
    public class GrupoPermiso
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public bool Activo { get; private set; } = true;
        public DateTime FechaCreacion { get; private set; } = DateTime.UtcNow;

        private readonly HashSet<Permiso> _permisos = new();
        public IReadOnlyCollection<Permiso> Permisos => new ReadOnlyCollection<Permiso>(_permisos.ToList());

        protected GrupoPermiso() { }

        public GrupoPermiso(int id, string nombre, string descripcion, IEnumerable<Permiso>? permisos = null)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre requerido", nameof(nombre));
            Id = id;
            Nombre = nombre.Trim();
            Descripcion = descripcion?.Trim() ?? string.Empty;
            if (permisos != null)
            {
                foreach (var p in permisos) _permisos.Add(p);
            }
        }

        public void AgregarPermiso(Permiso permiso)
        {
            if (permiso == null) throw new ArgumentNullException(nameof(permiso));
            _permisos.Add(permiso);
        }

        public void RemoverPermiso(Permiso permiso)
        {
            if (permiso == null) throw new ArgumentNullException(nameof(permiso));
            _permisos.Remove(permiso);
        }

        public bool TienePermiso(string permisoNombre) => _permisos.Any(p => string.Equals($"{p.Categoria}.{p.Nombre}", permisoNombre, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<string> ObtenerNombresPermisos() => _permisos.Select(p => $"{p.Categoria}.{p.Nombre}");

        public void Desactivar() => Activo = false;
        public void Activar() => Activo = true;
    }
}
