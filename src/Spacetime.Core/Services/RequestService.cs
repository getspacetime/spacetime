using LiteDB;
using Microsoft.Extensions.Logging;
using Spacetime.Core.Infrastructure;
namespace Spacetime.Core.Services;

public class RequestService : LiteDbService
{
    private readonly ILogger<RequestService> _log;
    public RequestService(ILogger<RequestService> log)
    {
        _log = log;
    }

    public Task<IEnumerable<SpacetimeRequest>> GetRequests()
    {
        _log.LogInformation("Fetching requests");

        var requests = new List<SpacetimeRequest>();
        using (var db = WithDatabase())
        {
            var col = db.GetCollection<SpacetimeRequest>("requests");
            requests = col.FindAll().ToList();
        }

        _log.LogInformation("Fetched {count} requests", requests?.Count);

        return Task.FromResult(requests.AsEnumerable());
    }

    public Task AddRequest(SpacetimeRequest request)
    {
        _log.LogInformation("Adding request");

        using (var db = WithDatabase())
        {
            var col = db.GetCollection<SpacetimeRequest>("requests");
            col.Insert(request);
        }

        _log.LogInformation("Added request");

        return Task.CompletedTask;
    }

    public Task UpdateRequest(SpacetimeRequest request)
    {
        SpacetimeRequest original;
        using (var db = WithDatabase())
        {
            var col = GetCollection(db);
            original = col.FindById(request.Id);
            col.Update(request);
        }

        return Task.CompletedTask;
    }

    public void DeleteRequest(int requestId)
    {
        try
        {
            using (var db = WithDatabase())
            {
                var col = GetCollection(db);
                col.Delete(requestId);
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Failed to delete request {requestId}", requestId);
        }
    }

    private ILiteCollection<SpacetimeRequest> GetCollection(LiteDatabase db)
    {
        return db.GetCollection<SpacetimeRequest>("requests");
    }
}

