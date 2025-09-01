namespace Biblioteca.Data.Entities
{
    public class AutorEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;

        // M�todo para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Autor ToAutor()
        {
            return new Biblioteca.Domain.Model.Autor(Id, Nombre, Apellido);
        }

        // M�todo est�tico para crear desde entidad de dominio
        public static AutorEntity FromAutor(Biblioteca.Domain.Model.Autor autor)
        {
            return new AutorEntity
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
                Apellido = autor.Apellido
            };
        }
    }
}