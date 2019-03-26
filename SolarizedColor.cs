using System.Windows.Media;

namespace Rei
{
    public class SolarizedColor
    {
        public static readonly SolidColorBrush Base00 = new SolarizedColor("#657b83");
        public static readonly SolidColorBrush Base01 = new SolarizedColor("#586e75");
        public static readonly SolidColorBrush Base02 = new SolarizedColor("#073642");
        public static readonly SolidColorBrush Base03 = new SolarizedColor("#002b36");
        public static readonly SolidColorBrush Base0 = new SolarizedColor("#839496");
        public static readonly SolidColorBrush Base1 = new SolarizedColor("#93a1a1");
        public static readonly SolidColorBrush Base2 = new SolarizedColor("#eee8d5");
        public static readonly SolidColorBrush Base3 = new SolarizedColor("#fdf6e3");
        public static readonly SolidColorBrush Yellow = new SolarizedColor("#b58900");
        public static readonly SolidColorBrush Orange = new SolarizedColor("#cb4b16");
        public static readonly SolidColorBrush Red = new SolarizedColor("#dc322f");
        public static readonly SolidColorBrush Magenta = new SolarizedColor("#d33682");
        public static readonly SolidColorBrush Violet = new SolarizedColor("#6c71c4");
        public static readonly SolidColorBrush Blue = new SolarizedColor("#268bd2");
        public static readonly SolidColorBrush Cyan = new SolarizedColor("#2aa198");
        public static readonly SolidColorBrush Green = new SolarizedColor("#859900");
        private readonly string _hex;


        private SolarizedColor(string hex)
        {
            _hex = hex;
        }

        public static implicit operator SolidColorBrush(SolarizedColor color)
        {
            return (SolidColorBrush) new BrushConverter().ConvertFrom(color._hex);
        }

        public static implicit operator Brush(SolarizedColor color)
        {
            return (SolidColorBrush) color;
        }
    }
}