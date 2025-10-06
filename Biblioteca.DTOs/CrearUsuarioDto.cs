namespace Biblioteca.DTOs
{
    public class CrearUsuarioDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int PersonaId { get; set; }
    }
}
