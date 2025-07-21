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
            // Devolvemos una nueva lista para que el exterior no pueda modificar la original.
            return GeneroInMemory.Generos.ToList();
        }

        // Método para agregar un nuevo género.
        public void Add(Genero genero)
        {
            // Asignamos un nuevo ID.
            genero.SetId(GetNextId());
            GeneroInMemory.Generos.Add(genero);
        }

        // Función auxiliar para generar el próximo ID (igual que en tu ejemplo).
        private int GetNextId()
        {
            if (GeneroInMemory.Generos.Count == 0)
            {
                return 1;
            }
            return GeneroInMemory.Generos.Max(g => g.Id) + 1;
        }

        public bool Update(Genero generoAActualizar)
        {
            // Buscamos el género existente en nuestra "base de datos" en memoria.
            Genero? generoExistente = GeneroInMemory.Generos.FirstOrDefault(g => g.Id == generoAActualizar.Id);

            if (generoExistente != null)
            {
                // Si lo encontramos, actualizamos sus propiedades usando los setters del modelo.
                // Esto asegura que las validaciones del dominio se apliquen.
                generoExistente.SetNombre(generoAActualizar.Nombre);
                return true; // Indicamos que la operación fue exitosa.
            }

            return false; // Indicamos que no se encontró el género para actualizar.
        }

        // --- MÉTODO NUEVO: DELETE ---
        public bool Delete(int id)
        {
            // Buscamos el género a eliminar.
            Genero? generoAEliminar = GeneroInMemory.Generos.FirstOrDefault(g => g.Id == id);

            if (generoAEliminar != null)
            {
                // Si lo encontramos, lo removemos de la lista.
                GeneroInMemory.Generos.Remove(generoAEliminar);
                return true; // Éxito.
            }

            return false; // No se encontró.
        }
    }
}