using Biblioteca.Data;
using Biblioteca.Domain.Model;
using System.Linq;

namespace Biblioteca.Domain.Services
{
    public class GeneroService
    {
        // Método para obtener la lista completa de géneros.
        public List<Genero> GetAll()
        {
            return GeneroInMemory.Generos.ToList();
        }

        // Método para obtener un género por su ID.
        
        public Genero GetById(int id)
        {
            return GeneroInMemory.Generos.FirstOrDefault(g => g.Id == id) 
                   ?? throw new KeyNotFoundException($"Género con ID {id} no encontrado.");
        }

        // Método para agregar un nuevo género.
        public void Add(Genero genero)
        {
            genero.SetId(GetNextId());
            GeneroInMemory.Generos.Add(genero);
        }

        // Función auxiliar para generar el próximo ID.
        private int GetNextId()
        {
            if (GeneroInMemory.Generos.Count == 0)
            {
                return 1;
            }
            return GeneroInMemory.Generos.Max(g => g.Id) + 1;
        }

        // Metodo para actualizar un género existente.
        public bool Update(Genero generoAActualizar)
        {
            Genero? generoExistente = GeneroInMemory.Generos.FirstOrDefault(g => g.Id == generoAActualizar.Id);

            if (generoExistente != null)
            {
                generoExistente.SetNombre(generoAActualizar.Nombre);
                return true;
            }

            return false;
        }

        // Metodo para eliminar un género por ID.
        public bool Delete(int id)
        {
            Genero? generoAEliminar = GeneroInMemory.Generos.FirstOrDefault(g => g.Id == id);

            if (generoAEliminar != null)
            {
                GeneroInMemory.Generos.Remove(generoAEliminar);
                return true;
            }

            return false;
        }
    }
}