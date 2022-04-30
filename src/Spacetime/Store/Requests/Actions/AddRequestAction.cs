using Spacetime.Core.Infrastructure;

namespace Spacetime.Store.Requests.Actions;

public class AddRequestAction
{
    public AddRequestAction(SpacetimeRequest request)
    {
        Request = request;
    }

    public SpacetimeRequest Request { get; private set; }
}