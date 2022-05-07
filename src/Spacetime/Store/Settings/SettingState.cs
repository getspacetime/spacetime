using Fluxor;
using Spacetime.Settings;

namespace Spacetime.Store.Settings
{
    [FeatureState]
    public class SettingState
    {
        public bool Loading { get; set; }
        public SettingsDto Settings { get; set; }

        public SettingState()
        {
            // set initial state
            Settings = new SettingsDto
            {
                DarkMode = true
            };
        }

        public SettingState(bool loading, SettingsDto settings)
        {
            Loading = loading;
            Settings = settings;
        }
    }
}
