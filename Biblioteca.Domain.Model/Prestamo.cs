namespace Biblioteca.Domain.Model
{
    public class Prestamo
    {
        private int _id;
        private int _libroId;
        private int _socioId;
        private DateTime _fechaPrestamo;
        private DateTime _fechaDevolucionPrevista;
        private DateTime? _fechaDevolucionReal;
        private Libro? _libro;
        private Usuario? _socio;

        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        public int LibroId
        {
            get => _libroId;
            private set => _libroId = value;
        }

        public int SocioId
        {
            get => _socioId;
            private set => _socioId = value;
        }

        public DateTime FechaPrestamo
        {
            get => _fechaPrestamo;
            private set => _fechaPrestamo = value;
        }

        public DateTime FechaDevolucionPrevista
        {
            get => _fechaDevolucionPrevista;
            private set => _fechaDevolucionPrevista = value;
        }

        public DateTime? FechaDevolucionReal
        {
            get => _fechaDevolucionReal;
            private set => _fechaDevolucionReal = value;
        }

        public Libro? Libro
        {
            get => _libro;
            private set => _libro = value;
        }

        public Usuario? Socio
        {
            get => _socio;
            private set => _socio = value;
        }

        protected Prestamo() { }

        public Prestamo(int id, int libroId, int socioId, DateTime fechaPrestamo, DateTime fechaDevolucionPrevista, DateTime? fechaDevolucionReal = null)
        {
            SetId(id);
            SetLibroId(libroId);
            SetSocioId(socioId);
            SetFechaPrestamo(fechaPrestamo);
            SetFechaDevolucionPrevista(fechaDevolucionPrevista);
            SetFechaDevolucionReal(fechaDevolucionReal);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            _id = id;
        }

        public void SetLibroId(int libroId)
        {
            if (libroId <= 0)
                throw new ArgumentException("El LibroId debe ser mayor que 0.", nameof(libroId));
            _libroId = libroId;
        }

        public void SetSocioId(int socioId)
        {
            if (socioId <= 0)
                throw new ArgumentException("El SocioId debe ser mayor que 0.", nameof(socioId));
            _socioId = socioId;
        }

        public void SetFechaPrestamo(DateTime fechaPrestamo)
        {
            _fechaPrestamo = fechaPrestamo;
        }

        public void SetFechaDevolucionPrevista(DateTime fechaDevolucionPrevista)
        {
            if (fechaDevolucionPrevista <= _fechaPrestamo)
                throw new ArgumentException("La fecha de devolución prevista debe ser posterior a la fecha de préstamo.", nameof(fechaDevolucionPrevista));
            _fechaDevolucionPrevista = fechaDevolucionPrevista;
        }

        public void SetFechaDevolucionReal(DateTime? fechaDevolucionReal)
        {
            _fechaDevolucionReal = fechaDevolucionReal;
        }

        public void SetLibro(Libro libro)
        {
            if (libro == null)
                throw new ArgumentNullException(nameof(libro), "El libro no puede ser nulo.");
            _libro = libro;
            _libroId = libro.Id;
        }

        public void SetSocio(Usuario socio)
        {
            if (socio == null)
                throw new ArgumentNullException(nameof(socio), "El socio no puede ser nulo.");
            _socio = socio;
            _socioId = socio.Id;
        }
    }
}
