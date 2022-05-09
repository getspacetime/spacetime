namespace Spacetime.Settings;

public interface ISettingsService
{
    Task<SettingsDto> GetSettings();
    Task SaveSettings(SettingsDto settings);
}