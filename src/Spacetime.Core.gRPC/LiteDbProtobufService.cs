using LiteDB;
using Spacetime.Core.gRPC.Interfaces;

namespace Spacetime.Core.gRPC;
public class LiteDbProtobufService : LiteDbService, IProtobufService
{
    public Task<IEnumerable<GrpcServiceDefinition>> GetServiceDefinitions()
    {
        var defs = new List<GrpcServiceDefinition>();
        using (var db = WithDatabase())
        {
            var col = db.WithCollection();
            defs = col.FindAll().ToList();
        }

        return Task.FromResult(defs.AsEnumerable());
    }

    public Task Save(List<GrpcServiceDefinition> services)
    {
        using var db = WithDatabase();
        var col = db.WithCollection();
        col.Insert(services);

        return Task.CompletedTask;
    }

    public Task Remove(Guid serviceId)
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

public class GrpcRequestService : LiteDbService
{
    public async Task<List<GrpcRequest>> GetRequests()
    {
        using var db = WithDatabase();
        var col = db.GetCollection<GrpcRequest>();
        return col.FindAll().ToList();
    }

    public async Task SaveRequest(GrpcRequest request)
    {
        using var db = WithDatabase();
        var col = db.GetCollection<GrpcRequest>();

        col.Upsert(request);
    }
}