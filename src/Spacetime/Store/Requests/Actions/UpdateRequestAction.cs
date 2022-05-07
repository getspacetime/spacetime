using Spacetime.Core;

namespace Spacetime.Store.Requests.Actions;

public class UpdateRequestAction
{
    public UpdateRequestAction(SpacetimeRequest request)
    {
        Request = request;
    }

    public SpacetimeRequest Request { get; private set; }
}