using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Rei
{
    public static class Config
    {
        static Config()
        {
            BufferExtensions.Dispatch = action => Application.Current.Dispatcher.Invoke(action);
        }

        public static readonly string ApplicationName = "零";

        public static readonly Brush Background = SolarizedColor.Base01;
        public static readonly Brush LineBackground = SolarizedColor.Base01;
        public static readonly Brush BufferBackground = SolarizedColor.Base02;
        public static readonly Brush CommandBackground = SolarizedColor.Base03;

        public static readonly Brush Foreground = SolarizedColor.Base2;
        public static readonly Brush LineForeground = SolarizedColor.Base2;
        public static readonly Brush BufferForeground = SolarizedColor.Base2;
        public static readonly Brush CommandForeground = SolarizedColor.Base3;

        public static readonly Brush OutputBackground = SolarizedColor.Base02.AdjustBrightness(1.1);
        public static readonly Brush OutputForeground = SolarizedColor.Base2.AdjustBrightness(0.9);

        public static readonly FontFamily FontFamily = new FontFamily("Consolas");


        public static readonly HotKey[] HotKeys =
        {
            new HotKey(ModifierKeys.None, Key.F11, "*"),
            new HotKey(ModifierKeys.Control, Key.S, ">"),
            new HotKey(ModifierKeys.Control, Key.R, "<"),
        };
    }
}