using System.Text.RegularExpressions;

namespace Biblioteca.Domain.Model
{
    public class Persona
    {
        private int _id;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;
        private string _dni = string.Empty;
        private string _email = string.Empty;

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

        public string Apellido
        {
            get => _apellido;
            private set => _apellido = value;
        }

        public string Dni
        {
            get => _dni;
            private set => _dni = value;
        }

        public string Email
        {
            get => _email;
            private set => _email = value;
        }

        protected Persona() { }

        public Persona(int id, string nombre, string apellido, string dni, string email)
        {
            SetId(id);
            SetNombre(nombre);
            SetApellido(apellido);
            SetDni(dni);
            SetEmail(email);
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
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            _nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío.", nameof(apellido));
            _apellido = apellido;
        }

        public void SetDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI no puede estar vacío.", nameof(dni));
            _dni = dni;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email no puede estar vacío.", nameof(email));

            // Regex simple y efectiva para validar emails comunes
            var pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                throw new ArgumentException("El email debe tener un formato válido.", nameof(email));

            _email = email;
        }
    }
}
