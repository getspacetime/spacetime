using Fluxor;
using Spacetime.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
