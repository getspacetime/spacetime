using LiteDB;
namespace Spacetime.Core.Services;

public class LiteDbService
{
    protected LiteDatabase WithDatabase()
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyData.db");
        return new LiteDatabase(path);
    }
}

