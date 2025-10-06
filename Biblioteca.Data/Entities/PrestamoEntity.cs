namespace Biblioteca.Data.Entities
{
    public class PrestamoEntity
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public int SocioId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionPrevista { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }

        // Propiedades de navegación
        public LibroEntity? Libro { get; set; }
        public UsuarioEntity? Socio { get; set; }

        // Método para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Prestamo ToPrestamo()
        {
            var prestamo = new Biblioteca.Domain.Model.Prestamo(
                Id, 
                LibroId, 
                SocioId, 
                FechaPrestamo, 
                FechaDevolucionPrevista, 
                FechaDevolucionReal);
            
            if (Libro != null)
                prestamo.SetLibro(Libro.ToLibro());
            
            if (Socio != null)
                prestamo.SetSocio(Socio.ToUsuario());
            
            return prestamo;
        }

        // Método estático para crear desde entidad de dominio
        public static PrestamoEntity FromPrestamo(Biblioteca.Domain.Model.Prestamo prestamo)
        {
            return new PrestamoEntity
            {
                Id = prestamo.Id,
                LibroId = prestamo.LibroId,
                SocioId = prestamo.SocioId,
                FechaPrestamo = prestamo.FechaPrestamo,
                FechaDevolucionPrevista = prestamo.FechaDevolucionPrevista,
                FechaDevolucionReal = prestamo.FechaDevolucionReal
            };
        }
    }
}
