using Fluxor;
using Spacetime.Core;

namespace Spacetime.Store.Requests
{
    [FeatureState]
    public class RequestState
    {
        /// <summary>
        /// Indicates data is being fetched.
        /// </summary>
        public bool Loading { get; set; }

        public string Filter { get; set; }
        public SpacetimeRequest SelectedRequest { get; set; }
        public List<SpacetimeRequest> Requests { get; set; }
        public List<SpacetimeRequest> FilteredRequests { get; set; }

        public RequestState()
        {
            // set initial state
            Loading = false;
            Requests = new List<SpacetimeRequest>();
            FilteredRequests = new List<SpacetimeRequest>();
        }
    }
}
