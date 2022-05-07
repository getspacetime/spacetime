using Spacetime.Core;

namespace Spacetime.Store.Requests.Actions;

public class UpdateRequestSuccessAction
{
    public UpdateRequestSuccessAction(SpacetimeRequest request)
    {
        Request = request;
    }

    public SpacetimeRequest Request { get; private set; }
}