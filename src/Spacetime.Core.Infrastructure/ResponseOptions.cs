namespace Spacetime.Core.Infrastructure;

public class ResponseOptions
{
    /// <summary>
    /// Enables formatting the response, if formatting for the content-type is supported.
    /// </summary>
    public bool Pretty { get; set; } = true;
}