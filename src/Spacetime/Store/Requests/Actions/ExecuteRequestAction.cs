using Spacetime.Core.Infrastructure;
using Spacetime.Core.Services;

namespace Spacetime.Store.Requests.Actions;

public class ExecuteRequestAction
{
    public ExecuteRequestAction(SpacetimeRequest request, ResponseOptions options = null)
    {
        Request = request;
        ResponseOptions = options ?? new ResponseOptions();
    }

    public ResponseOptions ResponseOptions { get; set; }
    public SpacetimeRequest Request { get; private set; }
}