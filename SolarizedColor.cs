using System.Windows.Media;

namespace Rei
{
    public class SolarizedColor
    {
        public static readonly SolarizedColor Base02 = new SolarizedColor("#073642");
        public static readonly SolarizedColor Base03 = new SolarizedColor("#002b36");
        public static readonly SolarizedColor Base01 = new SolarizedColor("#586e75");
        public static readonly SolarizedColor Base00 = new SolarizedColor("#657b83");
        public static readonly SolarizedColor Base0 = new SolarizedColor("#839496");
        public static readonly SolarizedColor Base1 = new SolarizedColor("#93a1a1");
        public static readonly SolarizedColor Base2 = new SolarizedColor("#eee8d5");
        public static readonly SolarizedColor Base3 = new SolarizedColor("#fdf6e3");
        public static readonly SolarizedColor Yellow = new SolarizedColor("#b58900");
        public static readonly SolarizedColor Orange = new SolarizedColor("#cb4b16");
        public static readonly SolarizedColor Red = new SolarizedColor("#dc322f");
        public static readonly SolarizedColor Magenta = new SolarizedColor("#d33682");
        public static readonly SolarizedColor Violet = new SolarizedColor("#6c71c4");
        public static readonly SolarizedColor Blue = new SolarizedColor("#268bd2");
        public static readonly SolarizedColor Cyan = new SolarizedColor("#2aa198");
        public static readonly SolarizedColor Green = new SolarizedColor("#859900");
        internal readonly string Hex;

        public SolarizedColor(string hex)
        {
            Hex = hex;
        }

        public static implicit operator Brush(SolarizedColor color)
        {
            return (SolidColorBrush) new BrushConverter().ConvertFrom(color.Hex);
        }
    }
}