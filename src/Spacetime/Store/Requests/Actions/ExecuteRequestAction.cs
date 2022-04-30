using Spacetime.Core.Infrastructure;

namespace Spacetime.Store.Requests.Actions;

public class ExecuteRequestAction
{
    public ExecuteRequestAction(SpacetimeRequest request)
    {
        Request = request;
    }

    public SpacetimeRequest Request { get; private set; }
}