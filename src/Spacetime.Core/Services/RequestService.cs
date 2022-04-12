using LiteDB;
using Spacetime.Core.Infrastructure;
namespace Spacetime.Core.Services;

public class RequestService
{
    public Task<IEnumerable<SpacetimeRequest>> GetRequests()
    {
        var requests = new List<SpacetimeRequest>();
        using (var db = WithDatabase())
        {
            var col = db.GetCollection<SpacetimeRequest>("requests");
            requests = col.FindAll().ToList();
        }

        return Task.FromResult(requests.AsEnumerable());
    }

    public Task AddRequest(SpacetimeRequest request)
    {
        using (var db = WithDatabase())
        {
            var col = db.GetCollection<SpacetimeRequest>("requests");
            col.Insert(request);
        }

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
            Console.WriteLine(ex.Message);
        }
    }

    private ILiteCollection<SpacetimeRequest> GetCollection(LiteDatabase db)
    {
        return db.GetCollection<SpacetimeRequest>("requests");
    }

    private LiteDatabase WithDatabase()
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyData.db");
        return new LiteDatabase(path);
    }
}

