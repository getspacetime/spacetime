namespace Spacetime.Store.Requests.Actions;

public class FilterRequestsAction
{
    public FilterRequestsAction(string filter)
    {
        Filter = filter;
    }

    public string Filter { get; set; }
}