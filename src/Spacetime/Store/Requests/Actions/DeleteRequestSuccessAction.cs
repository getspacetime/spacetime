namespace Spacetime.Store.Requests.Actions;

public class DeleteRequestSuccessAction
{
    public DeleteRequestSuccessAction(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}