using System.Fabric;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ServiceA
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ServiceA : StatefulService
    {
        private readonly IRepository _repository;
        private readonly IServiceCollection _serviceCollection;

        public ServiceA(StatefulServiceContext context,
            IRepository repository,
            IServiceCollection serviceCollection)
            : base(context)
        {
            _repository = repository;
            _serviceCollection = serviceCollection;
        }

        ~ServiceA()
        {

        }

    /// <summary>
    /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
    /// </summary>
    /// <remarks>
    /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
    /// </remarks>
    /// <returns>A collection of listeners.</returns>
    protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[0];
        }

        protected override Task OnOpenAsync(ReplicaOpenMode openMode, CancellationToken cancellationToken)
        {
            _repository.Warmup();
            return Task.CompletedTask;
        }

        protected override Task OnCloseAsync(CancellationToken cancellationToken)
        {
            //_serviceProvider.Dispose();
            var sp = _serviceCollection.BuildServiceProvider();
            sp.Dispose();
            _serviceCollection.Clear();
            return base.OnCloseAsync(cancellationToken);
        }


        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override  Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            return Task.CompletedTask;
        }
    }
}
