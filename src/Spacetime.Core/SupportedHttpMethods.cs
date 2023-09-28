namespace Spacetime.Core;

public static class SupportedHttpMethods
{
    public static readonly Dictionary<string, HttpMethod> AllMethods = new ()
    {
        {"GET", HttpMethod.Get },
        {"POST", HttpMethod.Post },
        {"PUT", HttpMethod.Put },
        {"PATCH", HttpMethod.Patch },
        {"DELETE", HttpMethod.Delete },
        {"HEAD", HttpMethod.Head },
        {"OPTIONS", HttpMethod.Options }
    };
}