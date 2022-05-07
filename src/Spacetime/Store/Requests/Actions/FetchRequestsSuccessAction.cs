using Spacetime.Core;

namespace Spacetime.Store.Requests.Actions;

public class FetchRequestsSuccessAction
{
    public FetchRequestsSuccessAction(List<SpacetimeRequest> requests)
    {
        Requests = requests;
    }

    public List<SpacetimeRequest> Requests { get; set; }
}