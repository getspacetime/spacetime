using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluxor;
using MudBlazor.Services;
using Spacetime.Core;

namespace Spacetime.CompositionRoot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSpacetime(this IServiceCollection services)
        {
            // add third party libraries
            //services.AddFluxor(options => options.ScanAssemblies(
            //    typeof(RequestState).Assembly, 
            //    typeof(GrpcState).Assembly));

            services.AddMudServices();

            // register http clients
            services.AddHttpClient<ISpacetimeService, SpacetimeRestService>();

            return services;
        }
    }
}
