using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.Data
{
    public static class DatabaseConfiguration
    {
        public static string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=(localdb)\\mssqllocaldb;Database=Biblioteca;Trusted_Connection=true;MultipleActiveResultSets=true";
        }

        public static BibliotecaContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());
            
            var context = new BibliotecaContext(optionsBuilder.Options);
            
            // Asegurar que la base de datos existe
            context.Database.EnsureCreated();
            
            return context;
        }

        public static DbContextOptions<BibliotecaContext> GetDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());
            return optionsBuilder.Options;
        }
    }
}