using LiteDB;
using Microsoft.Extensions.Logging;
using Spacetime.Core.gRPC.Interfaces;

namespace Spacetime.Core.gRPC;
public class LiteDbProtobufService : LiteDbService, IProtobufService
{
    private readonly ILogger<LiteDbProtobufService> _log;

    public LiteDbProtobufService(ILogger<LiteDbProtobufService> log)
    {
        _log = log;
    }

    public Task<IEnumerable<GrpcServiceDefinition>> GetServiceDefinitions()
    {
        var defs = new List<GrpcServiceDefinition>();

        try
        {
            using (var db = WithDatabase())
            {
                var col = db.WithCollection();
                defs = col.FindAll().ToList();
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Failed to load service definitions");
        }

        return Task.FromResult(defs.AsEnumerable());
    }

    public Task Save(List<GrpcServiceDefinition> services)
    {
        using var db = WithDatabase();
        var col = db.WithCollection();
        foreach (var svc in services)
        {
            col.Upsert(svc);
        }

        return Task.CompletedTask;
    }

    public Task Remove(int serviceId)
    {
        using var db = WithDatabase();
        var col = db.WithCollection();
        col.Delete(serviceId);

        return Task.CompletedTask;
    }
}

public static class LiteDbExtensions
{
    public static ILiteCollection<GrpcServiceDefinition> WithCollection(this LiteDatabase db)
    {
        return db.GetCollection<GrpcServiceDefinition>("serviceDefinitions");
    }
}
