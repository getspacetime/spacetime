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

    public class FetchServicesSuccessAction
    {
        public List<GrpcServiceDefinition> Services { get; set; }
    }

    public class FetchServicesAction
    {

    }

    public class SaveServicesAction
    {
        public List<GrpcServiceDefinition> Services { get; set; }
    }

    public class RemoveServiceAction
    {
        public int Id { get; set; }
    }
}
