using Fluxor;
using Spacetime.Core.Infrastructure;

namespace Spacetime.Store.Requests
{
    [FeatureState]
    public class RequestState
    {
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
