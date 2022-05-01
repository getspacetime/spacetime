namespace Spacetime.Core.Formatters;

public interface IFormatterFactory
{
    public IFormatter Get(FormatterType type);
}