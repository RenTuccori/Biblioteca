using Biblioteca.Data.Entities;
using Biblioteca.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private readonly BibliotecaContext _context;

        public UsuarioRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (NombreUsuarioExists(usuario.NombreUsuario))
                throw new InvalidOperationException($"Ya existe un usuario con el nombre '{usuario.NombreUsuario}'.");

            if (usuario.PasswordHash == null || usuario.PasswordHash.Length == 0 || usuario.Salt == null || usuario.Salt.Length == 0)
                throw new ArgumentException("El usuario debe tener una contraseña establecida.");

            var usuarioEntity = UsuarioEntity.FromUsuario(usuario);
            _context.Usuarios.Add(usuarioEntity);
        }

        public Usuario? Get(int id)
        {
            var usuarioEntity = _context.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Grupos)
                    .ThenInclude(g => g.Permisos)
                .FirstOrDefault(u => u.Id == id);

            return usuarioEntity?.ToUsuario();
        }

        public Usuario? GetByNombreUsuario(string nombreUsuario)
        {
            var usuarioEntity = _context.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Grupos)
                    .ThenInclude(g => g.Permisos)
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
            return usuarioEntity?.ToUsuario();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Grupos)
                    .ThenInclude(g => g.Permisos)
                .OrderBy(u => u.NombreUsuario)
                .ToList()
                .Select(u => u.ToUsuario());
        }

        public bool Update(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            var existing = _context.Usuarios.Find(usuario.Id);
            if (existing == null)
                return false;

            if (NombreUsuarioExists(usuario.NombreUsuario, usuario.Id))
                throw new InvalidOperationException($"Ya existe otro usuario con el nombre '{usuario.NombreUsuario}'.");

            existing.NombreUsuario = usuario.NombreUsuario;
            existing.Rol = usuario.Rol;
            existing.PersonaId = usuario.PersonaId;
            existing.Activo = usuario.Activo;
            existing.FechaCreacion = usuario.FechaCreacion;

            if (usuario.PasswordHash != null && usuario.PasswordHash.Length > 0 &&
                usuario.Salt != null && usuario.Salt.Length > 0)
            {
                existing.PasswordHash = usuario.PasswordHash;
                existing.Salt = usuario.Salt;
            }

            return true;
        }

        public bool Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return false;

            var tienePrestamos = _context.Prestamos.Any(p => p.SocioId == id);
            if (tienePrestamos)
                throw new InvalidOperationException("No se puede eliminar el usuario porque tiene préstamos asociados.");

            _context.Usuarios.Remove(usuario);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool NombreUsuarioExists(string nombreUsuario, int? excludeId = null)
        {
            var query = _context.Usuarios.Where(u => u.NombreUsuario == nombreUsuario);
            
            if (excludeId.HasValue)
                query = query.Where(u => u.Id != excludeId.Value);

            return query.Any();
        }

        public IEnumerable<Usuario> GetByCriteria(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return GetAll();

            return _context.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Grupos)
                    .ThenInclude(g => g.Permisos)
                .Where(u => u.NombreUsuario.Contains(criterio) || 
                           u.Rol.Contains(criterio) ||
                           u.Persona!.Nombre.Contains(criterio) ||
                           u.Persona!.Apellido.Contains(criterio))
                .OrderBy(u => u.NombreUsuario)
                .ToList()
                .Select(u => u.ToUsuario());
        }

        public IEnumerable<Usuario> GetByRol(string rol)
        {
            return _context.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.Grupos)
                    .ThenInclude(g => g.Permisos)
                .Where(u => u.Rol == rol)
                .OrderBy(u => u.NombreUsuario)
                .ToList()
                .Select(u => u.ToUsuario());
        }
    }
}
