using Autofac.Features.Indexed;
using Spacetime.Core.Formatters;

namespace Spacetime.Container;

/// <summary>
/// Register the IFormatter using Keyed IIndex support in Autofac 
/// </summary>
public class FormatterFactory : IFormatterFactory
{
    private readonly IIndex<FormatterType, IFormatter> _index;

    public FormatterFactory(IIndex<FormatterType, IFormatter> index)
    {
        _index = index;
    }

    public IFormatter Get(FormatterType type)
    {
        return _index[type];
    }
}