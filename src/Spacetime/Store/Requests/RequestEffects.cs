using Fluxor;
using Microsoft.Extensions.Logging;
using Spacetime.Core;
using Spacetime.Core.Services;
using Spacetime.Store.Requests.Actions;

namespace Spacetime.Store.Requests
{
    /// <summary>
    /// Effects for <see cref="RequestState"/>
    /// </summary>
    public class RequestEffects
    {
        private readonly ILogger<RequestEffects> _log;
        private readonly RequestService _requests;
        private readonly ISpacetimeService _spacetime;
        public RequestEffects(ILogger<RequestEffects> log, ISpacetimeService spacetime, RequestService requests)
        {
            _log = log;
            _spacetime = spacetime;
            _requests = requests;
        }

        [EffectMethod(typeof(FetchRequestsAction))]
        public async Task HandleFetchRequestsAction(Fluxor.IDispatcher dispatcher)
        {
            var requests = await _requests.GetRequests();
            dispatcher.Dispatch(new FetchRequestsSuccessAction(requests.ToList()));
        }

        [EffectMethod]
        public Task HandleDeleteRequestAction(DeleteRequestAction action, Fluxor.IDispatcher dispatcher)
        {
            _requests.DeleteRequest(action.Id);
            dispatcher.Dispatch(new DeleteRequestSuccessAction(action.Id));

            return Task.CompletedTask;
        }

        [EffectMethod]
        public async Task HandleAddRequestAction(AddRequestAction action, Fluxor.IDispatcher dispatcher)
        {
            await _requests.AddRequest(action.Request);
            dispatcher.Dispatch(new AddRequestSuccessAction(action.Request));
        }

        [EffectMethod]
        public async Task HandleUpdateRequestAction(UpdateRequestAction action, Fluxor.IDispatcher dispatcher)
        {
            await _requests.UpdateRequest(action.Request);
            dispatcher.Dispatch(new UpdateRequestSuccessAction(action.Request));
        }

        [EffectMethod]
        public async Task HandleExecuteRequestAction(ExecuteRequestAction action, Fluxor.IDispatcher dispatcher)
        {
            try
            {
                var response = await _spacetime.Execute(action.Request);
                dispatcher.Dispatch(new ExecuteRequestSuccessAction(action.Request.Id, response));
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Failed to execute request {id}", action?.Request?.Id);
                dispatcher.Dispatch(new ExecuteRequestFailAction(action?.Request?.Id, null));
            }
            finally
            {
                if (action != null)
                {
                    dispatcher.Dispatch(new UpdateRequestAction(action.Request));
                }
            }
        }
    }
}
