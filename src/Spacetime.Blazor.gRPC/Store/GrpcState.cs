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
        public bool IsSaving { get; set; }
        public List<GrpcServiceDefinition> Services { get; set; } = new();
    }

    public class FetchServicesSuccessAction
    {
        public List<GrpcServiceDefinition> Services { get; set; } = new();
    }

    public class FetchServicesAction
    {

    }

    public class SaveServicesAction
    {
        public List<GrpcServiceDefinition> Services { get; set; } = new();
    }

    public class SaveServicesSuccessAction
    {
    }

    public class SaveServicesFailedAction
    {
    }

    public class SaveServicesCompleteAction
    {
    }

    public class RemoveServiceAction
    {
        public Guid Id { get; set; }
    }
}
