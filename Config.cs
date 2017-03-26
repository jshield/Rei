using System.Windows.Input;
using System.Windows.Media;

namespace Rei
{
    public static class Config
    {
        public static readonly string ApplicationName = "零";

        public static readonly Brush Background = SolarizedColor.Base01;
        public static readonly Brush LineBackground = SolarizedColor.Base01;
        public static readonly Brush BufferBackground = SolarizedColor.Base02;
        public static readonly Brush CommandBackground = SolarizedColor.Base03;

        public static readonly Brush Foreground = SolarizedColor.Base2;
        public static readonly Brush LineForeground = SolarizedColor.Base2;
        public static readonly Brush BufferForeground = SolarizedColor.Base2;
        public static readonly Brush CommandForeground = SolarizedColor.Base3;
        public static readonly FontFamily FontFamily = new FontFamily("Consolas");


        public static readonly HotKey[] HotKeys =
        {
            new HotKey() { ModifierKeys = ModifierKeys.Control, Key = Key.S, Command = ">"}, 
            new HotKey() { ModifierKeys = ModifierKeys.Control, Key = Key.R, Command = "<"}, 
        };

    }
}