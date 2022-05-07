using LiteDB;
using Microsoft.Extensions.Logging;

namespace Spacetime.Settings;

public class SettingsService : ISettingsService
{
    private readonly ILogger<SettingsService> _log;
    public SettingsService(ILogger<SettingsService> log)
    {
        _log = log;
    }

    public async Task<SettingsDto> GetSettings()
    {
        try
        {
            _log.LogInformation("Loading settings");

            using var db = WithDatabase();
            var col = db.GetCollection<SettingsDto>("settings");
            var settings = col.FindAll().FirstOrDefault();
            if (settings == null)
            {
                _log.LogInformation("No settings found");
                settings = new SettingsDto();
            }

            return settings;
        }
        catch (Exception ex)
        {
            // log an error and use default settings
            _log.LogError(ex, "Failed to load settings");
            return new SettingsDto();
        }
    }

    public async Task SaveSettings(SettingsDto settings)
    {
        _log.LogInformation("Saving settings");

        using var db = WithDatabase();
        var col = db.GetCollection<SettingsDto>("settings");
        col.Upsert(settings);
    }

    private LiteDatabase WithDatabase()
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