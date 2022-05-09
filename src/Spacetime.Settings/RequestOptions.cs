namespace Spacetime.Settings;

public class RequestOptions
{
    /// <summary>
    /// Enables formatting the request, if formatting for the content-type is supported.
    /// </summary>
    public bool Pretty { get; set; } = true;
}