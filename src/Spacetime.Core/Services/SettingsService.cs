using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Spacetime.Core.Services;

public class SettingsService
{
    public async Task<SettingsDto> GetSettings()
    {
        var path = GetPath();
        if (!File.Exists(path))
        {
            return new SettingsDto();
        }

        var yaml = await File.ReadAllTextAsync(path);
        
        var settings = FromYaml<SettingsDto>(yaml);

        return settings;
    }

    public async Task SaveSettings(SettingsDto settings)
    {
        var yaml = ToYaml(settings);
        var path = GetPath();
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
