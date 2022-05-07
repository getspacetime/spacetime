using Spacetime.Core;

namespace Spacetime.Store.Requests.Actions;

public class AddRequestSuccessAction
{
    public AddRequestSuccessAction(SpacetimeRequest request)
    {
        Request = request;
    }

    public SpacetimeRequest Request { get; private set; }
}