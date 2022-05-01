namespace Spacetime.Core.Formatters;

public class EmptyFormatter : IFormatter
{
    public string Format(string text)
    {
        return text;
    }
}