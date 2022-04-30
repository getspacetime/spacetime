namespace Spacetime.Store.Requests.Actions;

public class ShowRequestAction
{
    public ShowRequestAction(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}