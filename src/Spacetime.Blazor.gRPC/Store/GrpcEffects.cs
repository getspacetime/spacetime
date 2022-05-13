using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.Extensions.Logging;
using Spacetime.Core.gRPC.Interfaces;

namespace Spacetime.Blazor.gRPC.Store;

public class GrpcEffects
{
    private readonly ILogger<GrpcEffects> _log;
    private readonly IProtobufService _svc;

    public GrpcEffects(ILogger<GrpcEffects> log, IProtobufService svc)
    {
        _log = log;
        _svc = svc;
    }

    [EffectMethod(typeof(FetchServicesAction))]
    public async Task HandleFetchServicesAction(IDispatcher dispatcher)
    {
        var services = await _svc.GetServiceDefinitions();
        dispatcher.Dispatch(new FetchServicesSuccessAction { Services = services.ToList() });
    }
}
