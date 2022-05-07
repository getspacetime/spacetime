using LiteDB;

namespace Spacetime.Core.gRPC
{
    public class LiteDbService
    {
        protected LiteDatabase WithDatabase()
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Spacetime");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = Path.Combine(directory, "Spacetime.db");
            return new LiteDatabase(path);
        }
    }
}
