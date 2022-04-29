using Microsoft.Extensions.Logging;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Spacetime.Core.Services;

public class SettingsService
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
            var path = GetPath();

            _log.LogInformation("Loading settings from path {path}", path);

            if (!File.Exists(path))
            {
                _log.LogInformation("No settings found at {path}, creating default settings", path);
                await SaveSettings(new SettingsDto());
            }

            var yaml = await File.ReadAllTextAsync(path);
            _log.LogDebug("Loaded settings {settings}", yaml);

            var settings = FromYaml<SettingsDto>(yaml);
            if (settings == null)
            {
                _log.LogInformation("Restoring default settings in existing file");
                settings = new SettingsDto();
                await SaveSettings(settings);
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
        var yaml = ToYaml(settings);
        var path = GetPath();

        _log.LogInformation("Saving settings to {path}", path);

        await File.WriteAllTextAsync(path, yaml);
    }

    private string GetPath()
    {
        var directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var path = Path.Combine(directory, "settings.yaml");
        return path;
    }

    private T FromYaml<T>(string yaml)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var t = deserializer.Deserialize<T>(yaml);

        return t;
    }

    private string ToYaml(SettingsDto settings)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var yaml = serializer.Serialize(settings);
        return yaml;
    }
}

public class SettingsDto
{
    public bool DarkMode { get; set; }
    public bool ValidateCertificates { get; set; } = true;
    public bool EnableLogging { get; set; } = true;
    public bool EnableMetrics { get; set; } = true;
}
