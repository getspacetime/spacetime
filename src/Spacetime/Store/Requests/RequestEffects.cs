using Fluxor;
using Spacetime.Core.Services;

namespace Spacetime.Store.Requests
{
    /// <summary>
    /// Effects for <see cref="RequestState"/>
    /// </summary>
    public class RequestEffects
    {
        private readonly RequestService _requests;
        public RequestEffects(RequestService requests)
        {
            _requests = requests;
        }

        [EffectMethod(typeof(FetchRequestsAction))]
        public async Task HandleFetchRequestsAction(IDispatcher dispatcher)
        {
            var requests = await _requests.GetRequests();
            dispatcher.Dispatch(new FetchRequestsSuccessAction(requests.ToList()));
        }

        [EffectMethod]
        public Task HandleDeleteRequestAction(DeleteRequestAction action, IDispatcher dispatcher)
        {
            _requests.DeleteRequest(action.Id);
            dispatcher.Dispatch(new DeleteRequestSuccessAction(action.Id));

            return Task.CompletedTask;
        }

        [EffectMethod]
        public async Task HandleAddRequestAction(AddRequestAction action, IDispatcher dispatcher)
        {
            await _requests.AddRequest(action.Request);
            dispatcher.Dispatch(new AddRequestSuccessAction(action.Request));
        }
    }
}
