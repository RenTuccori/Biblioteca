using Biblioteca.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public static class DatabaseConfiguration
    {
        // Obtener la ruta del directorio raíz de la solución de forma más robusta
        private static string GetSolutionRoot()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var directory = new DirectoryInfo(currentDir);
            
            // Buscar hacia arriba hasta encontrar el directorio "Biblioteca" que contiene todos los proyectos
            while (directory != null)
            {
                // Verificar si este directorio contiene los proyectos principales
                var hasWebApi = Directory.Exists(Path.Combine(directory.FullName, "Biblioteca.WebAPI"));
                var hasDesktop = Directory.Exists(Path.Combine(directory.FullName, "Biblioteca.UI.Desktop"));
                var hasData = Directory.Exists(Path.Combine(directory.FullName, "Biblioteca.Data"));
                
                if (hasWebApi && hasDesktop && hasData)
                {
                    return directory.FullName;
                }
                
                directory = directory.Parent;
            }
            
            // Fallback: usar el directorio actual
            return currentDir;
        }
        
        // Ruta centralizada de la base de datos en la raíz de la solución
        public static readonly string DatabasePath = Path.Combine(GetSolutionRoot(), "biblioteca.db");
        
        public static readonly string ConnectionString = $"Data Source={DatabasePath}";

        public static BibliotecaContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
            optionsBuilder.UseSqlite(ConnectionString);
            
            var context = new BibliotecaContext(optionsBuilder.Options);
            
            // Log de la ruta para debugging
            Console.WriteLine($"Base de datos ubicada en: {DatabasePath}");
            
            // Asegurar que el directorio existe
            var directory = Path.GetDirectoryName(DatabasePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            // Asegurar que la base de datos existe
            context.Database.EnsureCreated();
            
            return context;
        }

        public static DbContextOptions<BibliotecaContext> GetDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
            optionsBuilder.UseSqlite(ConnectionString);
            return optionsBuilder.Options;
        }
    }
}