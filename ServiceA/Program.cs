using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ServiceA
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.

                ServiceRuntime.RegisterServiceAsync("ServiceAType",
                    context =>
                    {
                        var serviceCollection = new ServiceCollection();
                        serviceCollection.AddSingleton<IRepository, Repository>();
                        serviceCollection.AddSingleton(context);
                        serviceCollection.AddTransient<ServiceA>();
                        serviceCollection.AddSingleton<IServiceCollection>(serviceCollection);
                        var serviceProvider = serviceCollection.BuildServiceProvider();
                        
                        return serviceProvider.GetRequiredService<ServiceA>();
                    });

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(ServiceA).Name);

                // Prevents this host process from terminating so services keep running.
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
