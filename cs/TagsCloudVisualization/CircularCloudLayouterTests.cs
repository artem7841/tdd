using System.Drawing;
using FluentAssertions;


namespace TagsCloudVisualization;

public class CircularCloudLayouterTests
{
    
    private Point center;
    private CircularCloudLayouter layouter;
    private Size[] sizes;
    
    [SetUp]
    public void Setup()
    {
        center = new Point(0, 0);
        layouter = new CircularCloudLayouter(center);
        
        int len = 500;
        sizes = new Size[len];
        Random random = new Random();
        for (int i =0; i < len; i++)
        {
            sizes[i] =  new Size(random.Next(40, 240), random.Next(15, 30));
        }
        
    }

    public Point GetRectangleCenter(Rectangle rectangle)
    {
        int x = rectangle.X + rectangle.Width / 2;
        int y = rectangle.Y + rectangle.Height / 2;
        return new Point(x, y);
    }
    
    public bool TwoRectanglesIsPressed(Rectangle firstRectangle, Rectangle secondRectangle)
    {

        bool IsPressedX = (firstRectangle.X + firstRectangle.Width == secondRectangle.X ||
                           secondRectangle.X + secondRectangle.Width == firstRectangle.X);
        bool IsPressedY = (firstRectangle.Y + firstRectangle.Height == secondRectangle.Y ||
                           secondRectangle.Y + secondRectangle.Height == firstRectangle.Y);
        
        return IsPressedY || IsPressedX;
    }

    public double GetTotalAreaOfRectangles(Rectangle[] rectangles)
    {
        int totalAreaOfRectangles = 0;
        for (int i = 0; i < rectangles.Length; i++)
        {
            totalAreaOfRectangles += rectangles[i].Height  *  rectangles[i].Width;
        }

        return totalAreaOfRectangles;
    }



    public double GetDistanceBetweenPoints(Point firstPoint, Point secondPoint)
    {
        int distanceX =  firstPoint.X - secondPoint.X;
        int distanceY = firstPoint.Y - secondPoint.Y;
        return Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
    }
    
    public double GetAreaOfRound(Rectangle[] rectangles, Point center)
    {

        double maxRadius = 0;
        
        for (int i = 0; i < rectangles.Length; i++)
        {
            Point[] points =
            {
                new Point(rectangles[i].Left, rectangles[i].Top),
                new Point(rectangles[i].Left, rectangles[i].Bottom),
                new Point(rectangles[i].Right, rectangles[i].Top),
                new Point(rectangles[i].Right, rectangles[i].Bottom)
            };
            foreach (var point in points)
            {
                double radius = GetDistanceBetweenPoints(center, point);
                maxRadius = Math.Max(radius, maxRadius);
            }
        }
        
        
        return Math.PI * maxRadius * maxRadius;
    }
    
    
    [TestCase(-30, -10)]
    [TestCase(-30, 10)]
    [TestCase(30, -10)]
    [TestCase(0, 10)]
    [TestCase(10, 0)]
    [TestCase(0, 0)]
    public void PutNextRectangle_RectangleWithInvalidSize_ShouldBeThrowException(int width, int height)
    {
        var newRecSize = new Size(width, height);
    
        Action act = () => layouter.PutNextRectangle(newRecSize);

        act.Should().Throw<ArgumentException>();
    }

    [TestCase(-200, -100)]
    [TestCase(-300, 100)]
    [TestCase(30, -10)]
    [TestCase(0, 0)]
    public void PutNextRectangle_FirstRectangle_ShouldBeOnCenter(int x, int y)
    {
        Point newCenter = new Point(x, y);
        layouter = new CircularCloudLayouter(newCenter);
        var newRecSize = new Size(30, 10);

        Rectangle rectangle = layouter.PutNextRectangle(newRecSize);
        
        Point rectangleCenter = GetRectangleCenter(rectangle);
        rectangleCenter.X.Should().Be(x);
        rectangleCenter.Y.Should().Be(y);
    }
    
    [Test]
    public void PutNextRectangle_SecondAndFirstRectangles_ShouldNotIntersect()
    {
        var firstRecSize = new Size(30, 10);
        var secondRecSize = new Size(40, 20);

        Rectangle firstRectangle = layouter.PutNextRectangle(firstRecSize);
        Rectangle secondRectangle = layouter.PutNextRectangle(secondRecSize);
        
        firstRectangle.IntersectsWith(secondRectangle).Should().BeFalse();
    }

    

    
    [Test]
    public void PutNextRectangle_SomeRectangles_ShouldNotIntersect()
    {
        
        var rectangles = new Rectangle[sizes.Length];
        for (int i = 0; i < sizes.Length; i++)
        {
            
            rectangles[i] = layouter.PutNextRectangle(sizes[i]);
        }
        
        for (int i = 0; i < rectangles.Length; i++)
        {
            for (int j = i + 1; j < rectangles.Length; j++)
            {
                
                rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse(rectangles[i] + " " + i + " and " + rectangles[j] + " " + j + " intersect");
            }
        }
    }
    
    [Test]
    public void PutNextRectangle_EachRectangles_ShouldPressedToOtherRectangle()
    {
        
        var rectangles = new Rectangle[sizes.Length];
        for (int i = 0; i < sizes.Length; i++)
        {
            rectangles[i] = layouter.PutNextRectangle(sizes[i]);
        }
        
        for (int i = 0; i < rectangles.Length; i++)
        {
            bool touches = false;
            
            for (int j = 0; j < rectangles.Length; j++)
            {
                if (i != j && TwoRectanglesIsPressed(rectangles[i], rectangles[j]))
                {
                    touches = true;
                    break; 
                }
            }
            touches.Should().BeTrue("rec" + rectangles[i] + " not  touch");
        }
    }
    
    [Test]
    public void PutNextRectangle_Cloude_ShouldBeTightly()
    {
        
        var rectangles = new Rectangle[sizes.Length];
        for (int i = 0; i < sizes.Length; i++)
        {
            rectangles[i] = layouter.PutNextRectangle(sizes[i]);
        }

        double totalAreaOfRectangles = GetTotalAreaOfRectangles(rectangles);
        double areaOfRound = GetAreaOfRound(rectangles, center);
        double aspectOfAreas =  totalAreaOfRectangles / areaOfRound;
        
        aspectOfAreas.Should().BeGreaterThan(0.5);
    }

    [Test]
    public void GenerateImage_ShouldSaveImage()
    {
        var rectangles = new Rectangle[sizes.Length];
        for (int i = 0; i < sizes.Length; i++)
        {
            rectangles[i] = layouter.PutNextRectangle(sizes[i]);
        }
        string imagePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "cloude3.png");
        
        layouter.GenerateImage(imagePath);
        
        File.Exists(imagePath).Should().BeTrue("image not create");
    }
}