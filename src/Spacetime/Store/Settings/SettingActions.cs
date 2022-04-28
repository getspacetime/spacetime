using Spacetime.Core.Services;

namespace Spacetime.Store.Settings
{
    public class SetDarkModeAction
    {
        public bool Enabled { get; set; }
    }

    public class FetchSettingsAction
    {
    }

    public class FetchSettingsSuccessAction
    {
        public FetchSettingsSuccessAction(SettingsDto settings)
        {
            Settings = settings;
        }

        public SettingsDto Settings { get; }
    }

    public class SaveSettingsAction
    {
        public SaveSettingsAction(SettingsDto settings)
        {
            Settings = settings;
        }

        public SettingsDto Settings { get; }
    }

    public class SaveSettingsSuccessAction
    {
        public SaveSettingsSuccessAction(SettingsDto settings)
        {
            Settings = settings;
        }

        public SettingsDto Settings { get; }
    }
}
