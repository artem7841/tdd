using System.Drawing;
using System.Drawing.Imaging;
namespace TagsCloudVisualization;

class CircularCloudLayouter
{
    private readonly Point center;
    private readonly List<Rectangle> rectangles;
    private int aCoef = 30;

    public CircularCloudLayouter(Point center)
    {
        this.center = center;
        this.rectangles = new List<Rectangle>();
    }
    
    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
        {
            throw new ArgumentOutOfRangeException("rectangleSize is <= 0");
        }
        
        var leftTopCorner = GetLeftTopCornerFromCenter(center, rectangleSize);
        var rectangle = new Rectangle(leftTopCorner, rectangleSize);
        
        if (rectangles.Count == 0)
        {
            rectangles.Add(rectangle);
            return rectangle;
        }
        
        var angle = 180;
        while (IsIntersectingWithOtherRectangles(rectangle))
        {
            angle++;
            var nextPointOnSpiral = GetNextPointOnSpiral(angle);
            var newRecPosition = GetLeftTopCornerFromCenter(nextPointOnSpiral,  rectangleSize);
            rectangle.X = newRecPosition.X;
            rectangle.Y = newRecPosition.Y;
        }

        var result = PushRectangleToCenter(rectangle);
        rectangles.Add(result);
        return result;
    }

    public void GenerateImage(String pathToSave)
    {
        if (string.IsNullOrWhiteSpace(pathToSave))
        {
            throw new ArgumentNullException("pathToSave can not be null or empty" );
        }

        if (rectangles.Count == 0)
        {
            throw new InvalidOperationException("rectangles can not be empty" );
        }
        
        var imageSize = GetSizeForImage();
        using var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = new Pen(Color.Black, 1);
        using var brush = new SolidBrush(Color.OrangeRed);
        
        var offsetX = imageSize.Width / 2 - center.X;
        var offsetY = imageSize.Height / 2 - center.Y;
        
        for (int i = 0; i < rectangles.Count; i++)
        {
            var offsetRectangle = rectangles[i];
            offsetRectangle.Offset(offsetX, offsetY);
            graphics.FillRectangle(brush, offsetRectangle);
            graphics.DrawRectangle(pen, offsetRectangle);
        }
        bitmap.Save(pathToSave, ImageFormat.Png);
    }

    private Size GetSizeForImage()
    {
        var maxHeight = 0;
        var maxWidth = 0;
        for (int i = 0; i < rectangles.Count; i++)
        {
            var maxDistYFromCenter = Math.Max(Math.Abs(center.Y - rectangles[i].Top), Math.Abs(center.Y - rectangles[i].Bottom));
            var maxDistXFromCenter = Math.Max(Math.Abs(center.X - rectangles[i].Left), Math.Abs(center.X - rectangles[i].Right));
            
            maxHeight = Math.Max(maxHeight, maxDistYFromCenter);
            maxWidth = Math.Max(maxWidth, maxDistXFromCenter);
        }
        return new Size(maxWidth*2, maxHeight*2);
    }

    private Point GetLeftTopCornerFromCenter(Point center, Size size)
    {
        var x = center.X - size.Width / 2;
        var y = center.Y - size.Height / 2;
        var result = new Point(x, y);
        return result;
    }

    private bool IsIntersectingWithOtherRectangles(Rectangle rectangle)
    {
        foreach (Rectangle otherRectangle in rectangles)
        {
            if (otherRectangle.IntersectsWith(rectangle))
            {
                return true;
            }
        }
        return false;
    }

    private Rectangle PushRectangleToCenter(Rectangle rec)
    {
        var rectangle = rec;
        var prevX = rectangle.X;
        var prevY = rectangle.Y;
        while (rectangle.GetCenter().X != center.X)
        {
            prevX = rectangle.X;
            if (center.X - rectangle.GetCenter().X > 0)
            {
                rectangle.X++;
            }
            else
            {
                rectangle.X--;
            }
            
            if (IsIntersectingWithOtherRectangles(rectangle))
            {
                rectangle.X = prevX;
                break;
            }
        }
        while (rectangle.GetCenter().Y != center.Y)
        {
            prevY = rectangle.Y;
            if (center.Y - rectangle.GetCenter().Y > 0)
            {
                rectangle.Y++;
            }
            else
            {
                rectangle.Y--;
            }
            
            if (IsIntersectingWithOtherRectangles(rectangle))
            {
                rectangle.Y = prevY;
                break;
            }
        }
        return rectangle;
    }

    private Point GetNextPointOnSpiral(double angle)
    {
        var angleInRadians = Math.PI / 180 * angle;
        var cosine = Math.Cos(angleInRadians);
        var sine = Math.Sin(angleInRadians);

        var x = (int)(aCoef * angleInRadians * cosine + center.X);
        var y = (int)(aCoef * angleInRadians * sine +  center.Y);
        
        return new Point(x, y);
    }
}