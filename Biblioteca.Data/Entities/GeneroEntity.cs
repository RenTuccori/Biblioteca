namespace Biblioteca.Data.Entities
{
    public class GeneroEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Método para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Genero ToGenero()
        {
            return new Biblioteca.Domain.Model.Genero(Id, Nombre);
        }

        // Método estático para crear desde entidad de dominio
        public static GeneroEntity FromGenero(Biblioteca.Domain.Model.Genero genero)
        {
            return new GeneroEntity
            {
                Id = genero.Id,
                Nombre = genero.Nombre
            };
        }
    }
}