using Biblioteca.Data;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;
using System.Linq;

namespace Biblioteca.Domain.Services
{
    public class LibroService
    {
        // Dependencias de otros servicios
        private readonly AutorService _autorService;
        private readonly GeneroService _generoService;

        public LibroService()
        {
            _autorService = new AutorService();
            _generoService = new GeneroService();
        }

        public List<Libro> GetAll()
        {
            return LibroInMemory.Libros.ToList();
        }

        public Libro? GetById(int id)
        {
            return LibroInMemory.Libros.FirstOrDefault(l => l.Id == id);
        }

        public Libro Add(CrearLibroDto dto)
        {
            var autor = _autorService.GetById(dto.AutorId);
            var genero = _generoService.GetById(dto.GeneroId);
            var nuevoLibro = new Libro(0, dto.Titulo, dto.ISBN, autor, genero);
            nuevoLibro.SetId(GetNextId());
            LibroInMemory.Libros.Add(nuevoLibro);
            return nuevoLibro;
        }

        public bool Update(int id, CrearLibroDto dto)
        {
            var libroExistente = GetById(id);
            if (libroExistente == null)
            {
                return false; // No se encontró el libro
            }

            // Buscamos el nuevo autor y género
            var nuevoAutor = _autorService.GetById(dto.AutorId);
            var nuevoGenero = _generoService.GetById(dto.GeneroId);

            // Actualizamos el libro existente
            libroExistente.SetTitulo(dto.Titulo);
            libroExistente.SetISBN(dto.ISBN);
            libroExistente.SetAutor(nuevoAutor);
            libroExistente.SetGenero(nuevoGenero);

            return true;
        }

        public bool Delete(int id)
        {
            var libroAEliminar = GetById(id);
            if (libroAEliminar == null)
            {
                return false;
            }

            LibroInMemory.Libros.Remove(libroAEliminar);
            return true;
        }
        private int GetNextId()
        {
            if (LibroInMemory.Libros.Count == 0) return 1;
            return LibroInMemory.Libros.Max(l => l.Id) + 1;
        }
    }
}