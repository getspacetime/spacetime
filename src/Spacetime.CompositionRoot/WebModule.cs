using Autofac;
using Spacetime.Blazor.Shared;
using Spacetime.Blazor.Shared.Themes;

namespace Spacetime.CompositionRoot
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultTheme>();
            builder.RegisterType<ScriptUtils>();
        }
    }
}
