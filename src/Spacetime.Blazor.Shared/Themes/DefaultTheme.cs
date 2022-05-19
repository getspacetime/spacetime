using MudBlazor;

namespace Spacetime.Blazor.Shared.Themes
{
    public class DefaultTheme
    {
        public bool DarkMode { get; set; }
        private static readonly MudTheme _theme = new()
        {
            PaletteDark = new Palette
            {
                Primary = "#3498DB",
                Black = "#000a12",
                Background = "#27272a",
                BackgroundGrey = "#27272f",
                Surface = "#212121",
                DrawerBackground = "#212121",
                DrawerText = "rgba(255,255,255, 0.50)",
                DrawerIcon = "rgba(255,255,255, 0.50)",
                AppbarBackground = "#212121",
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
        public Palette Swatch => DarkMode ? _theme.PaletteDark : _theme.Palette;
    }
}
