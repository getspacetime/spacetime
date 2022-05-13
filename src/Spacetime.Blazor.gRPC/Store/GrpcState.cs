using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluxor;
using Spacetime.Core.gRPC;
using Spacetime.Core.Shared;

namespace Spacetime.Blazor.gRPC.Store
{
    [FeatureState]
    public class GrpcState
    {
        public List<GrpcServiceDefinition> Services { get; set; }
    }

    public static class GrpcReducers
    {
        [ReducerMethod]
        public static GrpcState FetchServicesSuccess(GrpcState state, FetchServicesSuccessAction action)
        {
            return new GrpcState
            {
                Services = action.Services
            };
        }
    }

    public class FetchServicesSuccessAction
    {
        public List<GrpcServiceDefinition> Services { get; set; }
    }

    public class FetchServicesAction
    {

    }
}
