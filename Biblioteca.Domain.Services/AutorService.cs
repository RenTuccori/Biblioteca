// EN: Biblioteca.Domain.Services/AutorService.cs

using Biblioteca.Domain.Model;
using Biblioteca.Data;
using System.Linq; // Asegúrate de tener este using

namespace Biblioteca.Domain.Services
{
    public class AutorService
    {
        public List<Autor> GetAll()
        {
            return AutorInMemory.Autores.ToList();
        }

        public Autor? GetById(int id)
        {
            // Devolvemos el autor o null si no se encuentra (sin excepción).
            return AutorInMemory.Autores.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Autor autor)
        {
            autor.SetId(GetNextId());
            AutorInMemory.Autores.Add(autor);
        }

        // --- MÉTODO UPDATE CORREGIDO (devuelve bool) ---
        public bool Update(Autor autor)
        {
            Autor? autorExistente = AutorInMemory.Autores.FirstOrDefault(a => a.Id == autor.Id);
            if (autorExistente != null)
            {
                // Si lo encontramos, actualizamos y devolvemos true
                autorExistente.SetNombre(autor.Nombre);
                autorExistente.SetApellido(autor.Apellido);
                return true;
            }
            // Si no lo encontramos, simplemente devolvemos false
            return false;
        }

        // --- MÉTODO DELETE CORREGIDO (devuelve bool) ---
        public bool Delete(int id)
        {
            Autor? autorAEliminar = AutorInMemory.Autores.FirstOrDefault(a => a.Id == id);
            if (autorAEliminar != null)
            {
                // Si lo encontramos, lo eliminamos y devolvemos true
                AutorInMemory.Autores.Remove(autorAEliminar);
                return true;
            }
            // Si no lo encontramos, devolvemos false
            return false;
        }

        private int GetNextId()
        {
            if (AutorInMemory.Autores.Count == 0)
            {
                return 1;
            }
            return AutorInMemory.Autores.Max(a => a.Id) + 1;
        }
    }
}