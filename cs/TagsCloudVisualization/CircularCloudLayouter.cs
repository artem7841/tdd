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

    private Size GetSizeForImage()
    {
        int maxHeight = 0;
        int maxWidth = 0;
        for (int i = 0; i<rectangles.Count; i++)
        {
            int maxDistXFromCenter = Math.Max(Math.Abs(center.Y - rectangles[i].Top), Math.Abs(center.Y - rectangles[i].Bottom));
            int maxDistYFromCenter = Math.Max(Math.Abs(center.X - rectangles[i].Left), Math.Abs(center.X - rectangles[i].Right));
            
            maxHeight = Math.Max(maxHeight, maxDistYFromCenter);
            maxWidth = Math.Max(maxWidth, maxDistXFromCenter);
        }
        return new Size(maxWidth*2, maxHeight*2);
    }

    private Point GetLeftTopCornerFromCenter(Point center, Size size)
    {
        int X = center.X -  size.Width / 2;
        int Y = center.Y -  size.Height / 2;
        Point result = new Point(X, Y);
        return result;
    }
    
    private Point GetCenterOfRectangle(Rectangle rectangle)
    {
        int X = rectangle.X + rectangle.Width / 2;
        int Y = rectangle.Y + rectangle.Height / 2;
        Point result = new Point(X, Y);
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

        Rectangle rectangle = rec;
        int prevX = rectangle.X;
        int prevY = rectangle.Y;
        while (GetCenterOfRectangle(rectangle).X != center.X)
        {
            
            prevX = rectangle.X;
            if (center.X - GetCenterOfRectangle(rectangle).X > 0)
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
        while (GetCenterOfRectangle(rectangle).Y != center.Y)
        {
            prevY = rectangle.Y;
            if (center.Y - GetCenterOfRectangle(rectangle).Y > 0)
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
        double angleInRadians = Math.PI / 180 * angle;
        double cosine = Math.Cos(angleInRadians);
        double sine = Math.Sin(angleInRadians);

        int x =  (int)(aCoef * angleInRadians * cosine + center.X);
        int y =  (int)(aCoef * angleInRadians * sine +  center.Y);
        
        return new Point(x, y);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
        {
            throw new ArgumentOutOfRangeException("rectangleSize is <= 0");
        }
        
        Point leftTopCorner = GetLeftTopCornerFromCenter(center, rectangleSize);
        Rectangle rectangle = new Rectangle(leftTopCorner, rectangleSize);
        
        if (rectangles.Count == 0)
        {
            rectangles.Add(rectangle);
            return rectangle;
        }
        
        double angle = 180;
        while (IsIntersectingWithOtherRectangles(rectangle))
        {
            angle++;
            Point nextPointOnSpiral = GetNextPointOnSpiral(angle);
            Point newRecPosition = GetLeftTopCornerFromCenter(nextPointOnSpiral,  rectangleSize);
            rectangle.X = newRecPosition.X;
            rectangle.Y = newRecPosition.Y;
        }

        Rectangle result = PushRectangleToCenter(rectangle);

        
        rectangles.Add(result);
        return result;
    }

    public void GenerateImage(String pathToSave)
    {
        Size imageSize = GetSizeForImage();
        Bitmap bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        Graphics graphics = Graphics.FromImage(bitmap);
        Pen pen = new Pen(Color.Black, 1);
        Brush brush = new SolidBrush(Color.OrangeRed);
        
        int offsetX = imageSize.Width / 2 - center.X;
        int offsetY = imageSize.Height / 2 - center.Y;
        
        for (int i = 0; i < rectangles.Count; i++)
        {
            Rectangle OffsetRectangle = new Rectangle(
                new Point(rectangles[i].X + offsetX, rectangles[i].Y + offsetY), 
                new Size(rectangles[i].Width, rectangles[i].Height
                ));
            graphics.FillRectangle(brush, OffsetRectangle);
            graphics.DrawRectangle(pen, OffsetRectangle);
        }
        bitmap.Save(pathToSave, ImageFormat.Png);
        
        bitmap.Dispose();
        graphics.Dispose();
        pen.Dispose();
        brush.Dispose();
    }
}