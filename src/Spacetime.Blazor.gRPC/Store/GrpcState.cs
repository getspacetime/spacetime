using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluxor;
using Spacetime.Core.Shared;

namespace Spacetime.Blazor.gRPC.Store
{
    [FeatureState]
    public class GrpcState
    {
        public GrpcRequest SelectedRequest { get; set; }
    }

    public static class GrpcReducers
    {
        [ReducerMethod]
        public static GrpcState ShowRequest(GrpcState state, ShowRequestAction action)
        {
            return new GrpcState
            {
                SelectedRequest = new GrpcRequest
                {
                    Service = action.Service,
                    Method = action.Method
                }
            };
        }
    }

    public class GrpcRequest : SpacetimeRequest
    {
        public string Service { get; set; }
    }

    public class ShowRequestAction
    {
        public string Service { get; set; }
        public string Method { get; set; }
        public ShowRequestAction(string service, string method)
        {
            Service = service;
            Method = method;
        }
    }
}
