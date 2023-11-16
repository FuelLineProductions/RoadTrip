using MudBlazor;

namespace RoadTrip.Components.Layout
{
    public static class Theme
    {
        public static MudTheme CurrentTheme()
        {
            return new MudTheme()
            {
                Palette = new PaletteLight()
                {
                    Primary = "#ed420e",
                    Secondary = "#409946",
                    Tertiary = "#4316d9",
                    Success = "#8bf216",
                    Error = "#bd120f",
                    Warning = "#e7eb2a",

                },
                PaletteDark = new PaletteDark()
                {

                }

            };
        }
    }
}
