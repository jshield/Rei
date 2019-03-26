using System.Windows.Media;

namespace Rei
{
    public static class SolidColorBrushExtensions
    {
        public static Brush AdjustBrightness(this SolidColorBrush brush, double factor)
        {
            var originalColour = brush.Color;
            Color adjustedColour = Color.FromArgb(originalColour.A,
                (byte) (originalColour.R * factor),
                (byte) (originalColour.G * factor),
                (byte) (originalColour.B * factor));
            return new SolidColorBrush(adjustedColour);
        }
    }
}