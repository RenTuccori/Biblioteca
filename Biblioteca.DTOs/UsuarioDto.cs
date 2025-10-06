namespace Biblioteca.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int PersonaId { get; set; }
        public string PersonaNombreCompleto { get; set; } = string.Empty;
    }
}
