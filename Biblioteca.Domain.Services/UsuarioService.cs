using Biblioteca.Data;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.Domain.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly PersonaRepository _personaRepository;

        public UsuarioService(UsuarioRepository usuarioRepository, PersonaRepository personaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _personaRepository = personaRepository;
        }

        public UsuarioDto Add(CrearUsuarioDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Verificar que la persona existe
            var persona = _personaRepository.Get(dto.PersonaId);
            if (persona == null)
                throw new ArgumentException($"No existe una persona con Id {dto.PersonaId}");

            var usuario = new Usuario(0, dto.NombreUsuario, dto.Clave, dto.Rol, dto.PersonaId);
            
            _usuarioRepository.Add(usuario);
            _usuarioRepository.SaveChanges();

            // Recuperar el Id asignado utilizando el nombre de usuario (único)
            var saved = _usuarioRepository.GetByNombreUsuario(dto.NombreUsuario);

            return new UsuarioDto
            {
                Id = saved?.Id ?? usuario.Id,
                NombreUsuario = saved?.NombreUsuario ?? usuario.NombreUsuario,
                Rol = saved?.Rol ?? usuario.Rol,
                PersonaId = saved?.PersonaId ?? usuario.PersonaId,
                PersonaNombreCompleto = $"{persona.Nombre} {persona.Apellido}"
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _usuarioRepository.Delete(id);
                if (result)
                    _usuarioRepository.SaveChanges();
                return result;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public UsuarioDto? Get(int id)
        {
            var usuario = _usuarioRepository.Get(id);
            if (usuario == null)
                return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario.Rol,
                PersonaId = usuario.PersonaId,
                PersonaNombreCompleto = usuario.Persona != null ? $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}" : ""
            };
        }

        public IEnumerable<UsuarioDto> GetAll()
        {
            var usuarios = _usuarioRepository.GetAll();
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Rol = u.Rol,
                PersonaId = u.PersonaId,
                PersonaNombreCompleto = u.Persona != null ? $"{u.Persona.Nombre} {u.Persona.Apellido}" : ""
            });
        }

        public bool Update(UsuarioDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Verificar que la persona existe
            var persona = _personaRepository.Get(dto.PersonaId);
            if (persona == null)
                throw new ArgumentException($"No existe una persona con Id {dto.PersonaId}");

            // No cambiamos password aquí; exponer endpoint específico si es necesario
            var usuario = new Usuario(dto.Id, dto.NombreUsuario, dto.Rol, dto.PersonaId);
            
            var result = _usuarioRepository.Update(usuario);
            if (result)
                _usuarioRepository.SaveChanges();
            
            return result;
        }

        public IEnumerable<UsuarioDto> GetByCriteria(BusquedaCriterioDto criterioDto)
        {
            var usuarios = _usuarioRepository.GetByCriteria(criterioDto.Texto);
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Rol = u.Rol,
                PersonaId = u.PersonaId,
                PersonaNombreCompleto = u.Persona != null ? $"{u.Persona.Nombre} {u.Persona.Apellido}" : ""
            });
        }

        public IEnumerable<UsuarioDto> GetByRol(string rol)
        {
            var usuarios = _usuarioRepository.GetByRol(rol);
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Rol = u.Rol,
                PersonaId = u.PersonaId,
                PersonaNombreCompleto = u.Persona != null ? $"{u.Persona.Nombre} {u.Persona.Apellido}" : ""
            });
        }

        public bool ChangePassword(int userId, string currentPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new ArgumentException("La contraseña actual es requerida.", nameof(currentPassword));
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("La nueva contraseña es requerida.", nameof(newPassword));

            var user = _usuarioRepository.Get(userId);
            if (user == null)
                return false;

            if (!user.ValidatePassword(currentPassword))
                return false;

            user.SetPassword(newPassword);
            var ok = _usuarioRepository.Update(user);
            if (ok)
                _usuarioRepository.SaveChanges();
            return ok;
        }
    }
}
