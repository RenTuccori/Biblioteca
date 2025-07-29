using Biblioteca.Domain.Model;
using System.Linq;

namespace Biblioteca.Data
{
    public class LibroInMemory
    {
        public static List<Libro> Libros { get; } = new List<Libro>();

        static LibroInMemory()
        {
            // Buscamos en las otras "despensas" para obtener los objetos
            var autorBorges = AutorInMemory.Autores.FirstOrDefault(a => a.Apellido == "Borges");
            var generoFiccion = GeneroInMemory.Generos.FirstOrDefault(g => g.Nombre == "Ciencia Ficción");

            // Solo añadimos el libro si encontramos el autor y el género
            if (autorBorges != null && generoFiccion != null)
            {
                Libros.Add(new Libro(1, "El Aleph", "978-8420633130", autorBorges, generoFiccion));
            }
        }
    }
}