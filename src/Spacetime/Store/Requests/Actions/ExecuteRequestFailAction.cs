using Spacetime.Core.Shared;

namespace Spacetime.Store.Requests.Actions;

public class ExecuteRequestFailAction
{
    public ExecuteRequestFailAction(int? id, SpacetimeResponse response)
    {
        Id = id;
        Response = response;
    }

    public int? Id { get; set; }
    public SpacetimeResponse Response { get; private set; }
}