namespace Biblioteca.Domain.Model
{
    public class Usuario
    {
        private int _id;
        private string _nombreUsuario = string.Empty;
        private string _clave = string.Empty;
        private string _rol = string.Empty;
        private int _personaId;
        private Persona? _persona;

        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public string NombreUsuario
        {
            get => _nombreUsuario;
            private set => _nombreUsuario = value;
        }

        public string Clave
        {
            get => _clave;
            private set => _clave = value;
        }

        public string Rol
        {
            get => _rol;
            private set => _rol = value;
        }

        public int PersonaId
        {
            get => _personaId;
            private set => _personaId = value;
        }

        public Persona? Persona
        {
            get => _persona;
            private set => _persona = value;
        }

        protected Usuario() { }

        public Usuario(int id, string nombreUsuario, string clave, string rol, int personaId)
        {
            SetId(id);
            SetNombreUsuario(nombreUsuario);
            SetClave(clave);
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

        public void SetClave(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("La clave no puede estar vacía.", nameof(clave));
            if (clave.Length < 6)
                throw new ArgumentException("La clave debe tener al menos 6 caracteres.", nameof(clave));
            _clave = clave;
        }

        public void SetRol(string rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
                throw new ArgumentException("El rol no puede estar vacío.", nameof(rol));
            if (rol != "bibliotecario" && rol != "socio")
                throw new ArgumentException("El rol debe ser 'bibliotecario' o 'socio'.", nameof(rol));
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
    }
}
