using Biblioteca.Domain.Model;

namespace Biblioteca.Data
{
    public class GeneroInMemory
    {
        // La lista es estática para que los datos no se pierdan entre llamadas.
        public static List<Genero> Generos { get; } = new List<Genero>();

        // El constructor estático se ejecuta una sola vez para precargar datos.
        static GeneroInMemory()
        {
            Generos.Add(new Genero(1, "Novela"));
            Generos.Add(new Genero(2, "Ciencia Ficción"));
            Generos.Add(new Genero(3, "Ensayo"));
        }
    }
}