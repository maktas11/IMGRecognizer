using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ImageDetector
{
    public class ScreenImageDetector
    {
        Vector2 scanStart, scanEnd; // scan area

        public ScreenImageDetector(Vector2 scanStart, Vector2 scanEnd) 
        {
            this.scanStart = scanStart;
            this.scanEnd = scanEnd;
        }

        public Rectangle2D ScanForImage(Bitmap imageToFind) // overload
        {
            return ScanForImage(imageToFind,scanStart,scanEnd);
        }

        public Rectangle2D ScanForImage(Bitmap imageToFind, Vector2 start, Vector2 end)
        {
            using (Bitmap screenShot = new Bitmap((int)(end.X - start.X), (int)(end.Y - start.Y)))
                using (Graphics graphics = Graphics.FromImage(screenShot))
            {
                graphics.CopyFromScreen(new Point((int)start.X, (int)start.Y), Point.Empty, screenShot.Size);

                for (int x = 0; x <= screenShot.Width - imageToFind.Width; x++)
                {
                    for (int y = 0; y <= screenShot.Height - imageToFind.Height; y++)
                    {
                        // check if imge is inside of the area
                        if (IsImageAtLocation(imageToFind,screenShot,x,y))
                        {
                            return new Rectangle2D { origin = new Vector2(start.X + x, start.Y + y)
                            ,end = new Vector2 (start.X + x + imageToFind.Width, start.Y + y + imageToFind.Height)};
                        }

                    }
                }
            }
            return new Rectangle2D { origin = Vector2.Zero, end = Vector2.Zero };
        }

        public bool IsImageAtLocation(Bitmap ImageToFind,Bitmap screenShot, int x, int y)
        {
            for (int i = 0; i < ImageToFind.Width; i++)
            {
                for (int j = 0; j < ImageToFind.Height; j++)
                {
                    Color imagePixel = ImageToFind.GetPixel(i, j);
                    Color screenPixel = screenShot.GetPixel(x + i,y + j);

                    if (imagePixel != screenPixel)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
