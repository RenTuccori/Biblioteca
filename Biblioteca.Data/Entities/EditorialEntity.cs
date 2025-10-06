namespace Biblioteca.Data.Entities
{
    public class EditorialEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Método para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Editorial ToEditorial()
        {
            return new Biblioteca.Domain.Model.Editorial(Id, Nombre);
        }

        // Método estático para crear desde entidad de dominio
        public static EditorialEntity FromEditorial(Biblioteca.Domain.Model.Editorial editorial)
        {
            return new EditorialEntity
            {
                Id = editorial.Id,
                Nombre = editorial.Nombre
            };
        }
    }
}
