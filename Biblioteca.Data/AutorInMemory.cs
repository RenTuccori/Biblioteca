using Biblioteca.Domain.Model;

namespace Biblioteca.Data
{
    public class AutorInMemory
    {
        // La lista es estática para que los datos no se pierdan entre llamadas.
        public static List<Autor> Autores { get; } = new List<Autor>();

        // El constructor estático se ejecuta una sola vez para precargar datos.
        static AutorInMemory()
        {
            Autores.Add(new Autor(1, "Gabriel", "García Márquez"));
            Autores.Add(new Autor(2, "Jorge Luis", "Borges"));
        }
    }
}