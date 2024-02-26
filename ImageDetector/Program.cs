using ImageDetector;
using System.Drawing;
using System.Numerics;

Bitmap imageToFind = new Bitmap(@"C:\Users\xivo\Documents\Capture.PNG");
Vector2 start = new Vector2(0, 0);
Vector2 end = new Vector2(1920, 1080);

Renderer renderer = new Renderer();
renderer.Start().Wait();

ScreenImageDetector imageDetector = new ScreenImageDetector(start,end);

while(true)
{
    Rectangle2D match = imageDetector.ScanForImage(imageToFind);
    if (match.origin != Vector2.Zero )
    {
        renderer.AddMatch(match);
    }
}
