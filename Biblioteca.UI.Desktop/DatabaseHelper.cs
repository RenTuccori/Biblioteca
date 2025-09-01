using Biblioteca.Data;

namespace Biblioteca.UI.Desktop
{
    public static class DatabaseHelper
    {
        public static BibliotecaContext CreateDbContext()
        {
            return DatabaseConfiguration.CreateDbContext();
        }
    }
}