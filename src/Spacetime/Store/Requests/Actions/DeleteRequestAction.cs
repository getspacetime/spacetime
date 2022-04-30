namespace Spacetime.Store.Requests.Actions;

public class DeleteRequestAction
{
    public DeleteRequestAction(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}