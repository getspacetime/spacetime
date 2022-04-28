using Fluxor;
using MudBlazor;
using Spacetime.Store.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Themes
{
    public class DefaultTheme
    {
        private readonly IState<SettingState> _settings;
        public DefaultTheme(IState<SettingState> settings)
        {
            _settings = settings;
        }

        private static readonly MudTheme _theme = new()
        {
            PaletteDark = new Palette
            {
                Primary = "#3498DB",
                Black = "#000a12",
                Background = "#27272a",
                BackgroundGrey = "#27272f",
                Surface = "#212121",
                DrawerBackground = "#27272f",
                DrawerText = "rgba(255,255,255, 0.50)",
                DrawerIcon = "rgba(255,255,255, 0.50)",
                AppbarBackground = "#27272f",
                AppbarText = "rgba(255,255,255, 0.70)",
                TextPrimary = "rgba(255,255,255, 0.70)",
                TextSecondary = "rgba(255,255,255, 0.50)",
                ActionDefault = "#adadb1",
                ActionDisabled = "rgba(255,255,255, 0.26)",
                ActionDisabledBackground = "rgba(255,255,255, 0.12)",
                Divider = "rgba(255,255,255, 0.12)",
                DividerLight = "rgba(255,255,255, 0.06)",
                TableLines = "rgba(255,255,255, 0.12)",
                LinesDefault = "rgba(255,255,255, 0.12)",
                LinesInputs = "rgba(255,255,255, 0.3)",
                TextDisabled = "rgba(255,255,255, 0.2)",
                Info = "#3299ff",
                Success = "#0bba83",
                Warning = "#ffa800",
                Error = "#f64e62",
                Dark = "#212121",

            }
        };

        public MudTheme Theme => _theme;

        // calling this swatch bc I hate the word palette
        public Palette Swatch => _settings.Value.Settings.DarkMode ? _theme.PaletteDark : _theme.Palette;
    }
}
