using Fluxor;
using Spacetime.Core.Infrastructure;

namespace Spacetime.Store.Requests
{
    /// <summary>
    /// Reducers for <see cref="RequestState"/>
    /// </summary>
    public static class RequestReducers
    {
        [ReducerMethod(typeof(FetchRequestsAction))]
        public static RequestState FetchRequests(RequestState state)
        {
            return new RequestState { 
                Loading = true, 
                Requests = state.Requests, 
                FilteredRequests = state.Requests,
                SelectedRequest = state.SelectedRequest
            };
        }

        [ReducerMethod]
        public static RequestState FetchRequestsSuccess(RequestState state, FetchRequestsSuccessAction action)
        {
            var draft = new RequestState { 
                Loading = false, 
                Requests = action.Requests, 
                Filter = state.Filter,
                SelectedRequest = state.SelectedRequest
            };
            draft.FilteredRequests = GetFilteredRequests(draft);

            return draft;
        }


        [ReducerMethod]
        public static RequestState FilterRequests(RequestState state, FilterRequestsAction action)
        {
            var draft = new RequestState
            {
                Loading = state.Loading,
                Requests = state.Requests,
                Filter = action.Filter,
                SelectedRequest = state.SelectedRequest
            };

            draft.FilteredRequests = GetFilteredRequests(draft);

            return draft;
        }

        [ReducerMethod]
        public static RequestState ShowRequest(RequestState state, ShowRequestAction action)
        {
            return new RequestState
            {
                Loading = state.Loading,
                Requests = state.Requests,
                Filter = state.Filter,
                FilteredRequests = state.FilteredRequests,
                SelectedRequest = state.Requests.FirstOrDefault(p => p.Id == action.Id)
            };
        }

        [ReducerMethod]
        public static RequestState DeleteRequestSuccess(RequestState state, DeleteRequestSuccessAction action)
        {
            var draft = new RequestState
            {
                Loading = state.Loading,
                Requests = state.Requests,
                Filter = state.Filter,
                FilteredRequests = state.FilteredRequests,
                SelectedRequest = state.SelectedRequest
            };

            var request = draft.Requests.FirstOrDefault(p => p.Id == action.Id);

            // reassign selected request
            if (draft.SelectedRequest != null && draft.SelectedRequest.Id == action.Id)
            {
                draft.SelectedRequest = FindClosestRequest(draft, request);
            }

            // remove the request after re-assignment
            // so we can find the closest request to this one
            if (draft.Requests.Remove(request))
            {
                draft.FilteredRequests = GetFilteredRequests(draft);
            }

            return draft;
        }

        [ReducerMethod]
        public static RequestState AddRequestSuccess(RequestState state, AddRequestSuccessAction action)
        {
            var draft = new RequestState
            {
                Loading = state.Loading,
                Requests = state.Requests,
                Filter = state.Filter,
                FilteredRequests = state.FilteredRequests,
                SelectedRequest = state.SelectedRequest
            };

            draft.Requests.Add(action.Request);
            draft.SelectedRequest = action.Request;

            return draft;
        }

        /// <summary>
        /// Find the closest request to src request
        /// </summary>
        /// <param name="state"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        private static SpacetimeRequest FindClosestRequest(RequestState state, SpacetimeRequest src)
        {
            SpacetimeRequest dest = null;
            // todo: need to test edge case when user has the list filtered and we delete it
            var index = state.Requests.IndexOf(src);
            if (index > 0)
            {
                // set the current request to the preceding
                dest = state.Requests[index - 1];
            }
            else if (state.Requests.TakeLast(1).First() != src)
            {
                // if this isn't the last one, set current request to the next one
                dest = state.Requests[index + 1];
            }
            else
            {
                dest = null;
            }

            return dest;
        }

        /// <summary>
        /// Filters requests based on the state filter
        /// </summary>
        private static List<SpacetimeRequest> GetFilteredRequests(RequestState state)
        {
            if (string.IsNullOrWhiteSpace(state.Filter))
            {
                return state.Requests;
            }

            return state.Requests.Where(p => p.Name.Contains(state.Filter, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
