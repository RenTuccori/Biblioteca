using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.UI.Desktop
{
    public static class ServiceProvider
    {
        private static IServiceProvider? _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>() where T : class
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("ServiceProvider not initialized");
            
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}