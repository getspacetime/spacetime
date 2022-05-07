namespace Spacetime.Settings;

public class SettingsDto
{
    public int Id { get; set; }
    public bool DarkMode { get; set; } = true;
    public bool ValidateCertificates { get; set; } = true;
    public bool EnableLogging { get; set; } = true;
    public bool EnableMetrics { get; set; } = true;
}