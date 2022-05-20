using Fluxor;
using Spacetime.Blazor.gRPC.Store.Actions;

namespace Spacetime.Blazor.gRPC.Store;

public static class GrpcReducers
{
    [ReducerMethod]
    public static GrpcState FetchServicesSuccess(GrpcState state, FetchServicesSuccessAction action)
    {
        return new GrpcState
        {
            IsSaving = state.IsSaving,
            Services = action.Services,
            Requests = action.Requests
        };
    }

    [ReducerMethod]
    public static GrpcState SaveServices(GrpcState state, SaveServicesAction action)
    {
        return new GrpcState
        {
            IsSaving = true,
            Services = state.Services,
            Requests = state.Requests
        };
    }

    [ReducerMethod]
    public static GrpcState SaveServicesComplete(GrpcState state, SaveServicesCompleteAction action)
    {
        return new GrpcState
        {
            IsSaving = false,
            Services = state.Services,
            Requests = state.Requests
        };
    }


    [ReducerMethod]
    public static GrpcState SaveGrpcRequestSuccess(GrpcState state, SaveGrpcRequestSuccessAction action)
    {
        var draft = new GrpcState
        {
            IsSaving = state.IsSaving,
            Services = state.Services,
            Requests = state.Requests
        };

        // update the request in the list
        var index = draft.Requests.FindIndex(p => p.MethodDefinitionId == action.Request.MethodDefinitionId);
        if (index != -1)
        {
            draft.Requests[index] = action.Request;
        }
        else
        {
            // wasn't in the list, go ahead and add it
            draft.Requests.Add(action.Request);
        }

        return draft;
    }
}