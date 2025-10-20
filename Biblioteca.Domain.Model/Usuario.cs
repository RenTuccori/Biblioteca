using System.Security.Cryptography;

namespace Biblioteca.Domain.Model
{
    public class Usuario
    {
        private int _id;
        private string _nombreUsuario = string.Empty;
        private string _rol = string.Empty;
        private int _personaId;
        private Persona? _persona;

        // Password seguros
        private byte[] _passwordHash = Array.Empty<byte>();
        private byte[] _salt = Array.Empty<byte>();

        // Auditoría
        public DateTime FechaCreacion { get; private set; } = DateTime.UtcNow;
        public bool Activo { get; private set; } = true;

        // Grupos y permisos
        private readonly HashSet<GrupoPermiso> _grupos = new();
        public IReadOnlyCollection<GrupoPermiso> Grupos => _grupos.ToList().AsReadOnly();

        public int Id => _id;
        public string NombreUsuario => _nombreUsuario;
        public string Rol => _rol;
        public int PersonaId => _personaId;
        public Persona? Persona => _persona;

        public byte[] PasswordHash => _passwordHash;
        public byte[] Salt => _salt;

        protected Usuario() { }

        public Usuario(int id, string nombreUsuario, string password, string rol, int personaId)
        {
            SetId(id);
            SetNombreUsuario(nombreUsuario);
            SetPassword(password);
            SetRol(rol);
            SetPersonaId(personaId);
        }

        public Usuario(int id, string nombreUsuario, string rol, int personaId)
        {
            SetId(id);
            SetNombreUsuario(nombreUsuario);
            _passwordHash = Array.Empty<byte>();
            _salt = Array.Empty<byte>();
            SetRol(rol);
            SetPersonaId(personaId);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            _id = id;
        }

        public void SetNombreUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(nombreUsuario));
            _nombreUsuario = nombreUsuario;
        }

        public void SetRol(string rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
                throw new ArgumentException("El rol no puede estar vacío.", nameof(rol));
            if (rol != "bibliotecario" && rol != "socio" && rol != "administrador")
                throw new ArgumentException("El rol debe ser 'bibliotecario', 'socio' o 'administrador'.", nameof(rol));
            _rol = rol;
        }

        public void SetPersonaId(int personaId)
        {
            if (personaId <= 0)
                throw new ArgumentException("El PersonaId debe ser mayor que 0.", nameof(personaId));
            _personaId = personaId;
        }

        public void SetPersona(Persona persona)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona), "La persona no puede ser nula.");
            _persona = persona;
            _personaId = persona.Id;
        }

        public void Desactivar() => Activo = false;
        public void Activar() => Activo = true;

        public void SetAudit(DateTime fechaCreacion, bool activo)
        {
            FechaCreacion = fechaCreacion;
            Activo = activo;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La clave no puede estar vacía.", nameof(password));
            if (password.Length < 6)
                throw new ArgumentException("La clave debe tener al menos 6 caracteres.", nameof(password));

            _salt = RandomNumberGenerator.GetBytes(16);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, _salt, 100_000, HashAlgorithmName.SHA256);
            _passwordHash = pbkdf2.GetBytes(32);
        }

        public bool ValidatePassword(string password)
        {
            if (_salt == null || _salt.Length == 0 || _passwordHash == null || _passwordHash.Length == 0)
                return false;
            using var pbkdf2 = new Rfc2898DeriveBytes(password, _salt, 100_000, HashAlgorithmName.SHA256);
            var computed = pbkdf2.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(computed, _passwordHash);
        }

        public void ApplyCredentials(byte[] passwordHash, byte[] salt)
        {
            _passwordHash = passwordHash ?? Array.Empty<byte>();
            _salt = salt ?? Array.Empty<byte>();
        }

        // Grupos/Permisos
        public void SetGrupos(IEnumerable<GrupoPermiso> grupos)
        {
            _grupos.Clear();
            foreach (var g in grupos) _grupos.Add(g);
        }

        public void AgregarGrupo(GrupoPermiso grupo)
        {
            if (grupo == null) throw new ArgumentNullException(nameof(grupo));
            _grupos.Add(grupo);
        }

        public void RemoverGrupo(GrupoPermiso grupo)
        {
            if (grupo == null) throw new ArgumentNullException(nameof(grupo));
            _grupos.Remove(grupo);
        }

        public bool TienePermiso(string permisoNombre)
        {
            return _grupos.Any(g => g.TienePermiso(permisoNombre));
        }

        public IEnumerable<string> ObtenerTodosLosPermisos()
        {
            return _grupos.SelectMany(g => g.ObtenerNombresPermisos()).Distinct(StringComparer.OrdinalIgnoreCase);
        }

        public IEnumerable<string> ObtenerNombreGrupo()
        {
            return _grupos.Select(g => g.Nombre);
        }
    }
}
