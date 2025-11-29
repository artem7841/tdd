using System.Drawing;

namespace TagsCloudVisualization;

class CircularCloudLayouter
{
    private readonly Point center;
    private readonly List<Rectangle> rectangles;

    public CircularCloudLayouter(Point center)
    {
        this.center = center;
        this.rectangles = new List<Rectangle>();
    }
    

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        throw new NotImplementedException();
    }
}