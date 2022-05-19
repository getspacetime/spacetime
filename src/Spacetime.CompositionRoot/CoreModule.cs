using Autofac;
using Spacetime.Core;
using Spacetime.Core.Formatters;
using Spacetime.Core.gRPC;
using Spacetime.Core.gRPC.Dynamic;
using Spacetime.Core.gRPC.Interfaces;
using Spacetime.Core.Services;
using Spacetime.Settings;

namespace Spacetime.CompositionRoot
{
    // All the code in this file is included in all platforms.
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>();
            builder.RegisterType<SpacetimeRestService>().As<ISpacetimeService>();
            builder.RegisterType<UrlBuilder>();
            builder.RegisterType<GrpcExplorer>().As<IGrpcExplorer>();
            builder.RegisterType<DynamicGrpcFactory>().As<IDynamicGrpcFactory>();
            builder.RegisterType<LiteDbProtobufService>().As<IProtobufService>();
            builder.RegisterType<FormatterFactory>().As<IFormatterFactory>();
            builder.RegisterType<EmptyFormatter>().Keyed<IFormatter>(FormatterType.Default);
            builder.RegisterType<JsonFormatter>().Keyed<IFormatter>(FormatterType.Json);
        }
    }
}