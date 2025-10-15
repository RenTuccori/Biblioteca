using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.Data
{
    public static class DatabaseConfiguration
    {
        public static string GetConnectionString()
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                
                // Si no se encuentra el archivo o la cadena de conexión, usar SQL Server por defecto
                if (string.IsNullOrEmpty(connectionString))
                {
                    return "Server=(localdb)\\mssqllocaldb;Database=Biblioteca;Trusted_Connection=true;MultipleActiveResultSets=true";
                }
                
                return connectionString;
            }
            catch (Exception)
            {
                // Fallback a SQL Server si hay algún error al leer la configuración
                return "Server=(localdb)\\mssqllocaldb;Database=Biblioteca;Trusted_Connection=true;MultipleActiveResultSets=true";
            }
        }

        public static BibliotecaContext CreateDbContext()
        {
            var connectionString = GetConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
            
            // Usar SQL Server
            optionsBuilder.UseSqlServer(connectionString);
            
            var context = new BibliotecaContext(optionsBuilder.Options);
            
            // Asegurar que la base de datos existe
            context.Database.EnsureCreated();
            
            return context;
        }

        public static DbContextOptions<BibliotecaContext> GetDbContextOptions()
        {
            var connectionString = GetConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
            
            // Usar SQL Server
            optionsBuilder.UseSqlServer(connectionString);
            
            return optionsBuilder.Options;
        }
    }
}