using Spacetime.Core;

namespace Spacetime.Store.Requests.Actions;

public class ExecuteRequestSuccessAction
{
    public ExecuteRequestSuccessAction(int id, SpacetimeResponse response)
    {
        Id = id;
        Response = response;
    }

    public int Id { get; set; }
    public SpacetimeResponse Response { get; private set; }
}