using System;
using System.Drawing;

namespace BoDoiApp.Helpers
{
    public class ImageProcess
    {
        public Bitmap ChangeBackGroundColor(Bitmap origin,Color targetColor, int tolerance,Color newColor)
        {
            Bitmap result = new Bitmap(origin.Width, origin.Height);
            
            for(int y = 0;y < origin.Height; y++)
            {
                for (int x = 0; x < origin.Width; x++)
                {
                    Color pixelColor = origin.GetPixel(x, y);
                    if (IsColorMatch(pixelColor, targetColor, tolerance))
                    {
                        result.SetPixel(x, y, newColor);
                    }
                    else
                    {
                        result.SetPixel(x, y, pixelColor);
                    }
                }
            }
            return result;
        }

        private bool IsColorMatch(Color pixelColor, Color targetColor, int tolerance)
        {
            return Math.Abs(pixelColor.R - targetColor.R) <= tolerance &&
                   Math.Abs(pixelColor.G - targetColor.G) <= tolerance &&
                   Math.Abs(pixelColor.B - targetColor.B) <= tolerance;
        }
    }
}
