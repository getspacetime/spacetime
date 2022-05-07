using Spacetime.Core.gRPC.Interfaces;
using Spacetime.Core.Infrastructure;


namespace Spacetime.Core.gRPC
{
    public class LiteDbProtobufService : LiteDbService, IProtobufService
    {
        public Task<IEnumerable<GrpcServiceDefinition>> GetServiceDefinitions()
        {
            var defs = new List<GrpcServiceDefinition>();
            using (var db = WithDatabase())
            {
                var col = db.GetCollection<GrpcServiceDefinition>("serviceDefinitions");
                defs = col.FindAll().ToList();
            }

            return Task.FromResult(defs.AsEnumerable());
        }

        public Task Save(List<GrpcServiceDefinition> services)
        {
            using var db = WithDatabase();
            var col = db.GetCollection<GrpcServiceDefinition>("serviceDefinitions");
            foreach (var svc in services)
            {
                col.Upsert(svc);
            }

            return Task.CompletedTask;
        }
    }
}
