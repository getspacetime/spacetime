using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.Extensions.Logging;
using MudBlazor;
using Spacetime.Core.gRPC.Interfaces;

namespace Spacetime.Blazor.gRPC.Store;

public class GrpcEffects
{
    private readonly ILogger<GrpcEffects> _log;
    private readonly IProtobufService _svc;
    private readonly ISnackbar _snackbar;

    public GrpcEffects(ILogger<GrpcEffects> log, IProtobufService svc, ISnackbar snackbar)
    {
        _log = log;
        _svc = svc;
        _snackbar = snackbar;
    }

    [EffectMethod(typeof(FetchServicesAction))]
    public async Task HandleFetchServicesAction(IDispatcher dispatcher)
    {
        var services = await _svc.GetServiceDefinitions();
        dispatcher.Dispatch(new FetchServicesSuccessAction { Services = services.ToList() });
    }

    [EffectMethod]
    public async Task HandleRemoveServiceAction(RemoveServiceAction action, IDispatcher dispatcher)
    {
        await _svc.Remove(action.Id);

        _snackbar.Add("Removed service", Severity.Success);

        // reload services
        dispatcher.Dispatch(new FetchServicesAction());
    }

    [EffectMethod]
    public async Task HandleSaveServiceAction(SaveServicesAction action, IDispatcher dispatcher)
    {
        await _svc.Save(action.Services);

        _snackbar.Add("Saved services", Severity.Success);

        // reload services
        dispatcher.Dispatch(new FetchServicesAction());
    }
}
