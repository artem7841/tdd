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
        center = new Point(300, 200);
        layouter = new CircularCloudLayouter(center);
        sizes = new[] 
        {
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(45, 19),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(185, 23),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(185, 23),
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24), 
            new Size(45, 19),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(45, 19),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(185, 23),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(185, 23),
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24), 
            new Size(45, 19),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(45, 19),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(185, 23),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(185, 23),
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24), 
            new Size(45, 19),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(45, 19),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(185, 23),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(185, 23),
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24), 
            new Size(45, 19),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(238, 26),
            new Size(32, 18),
            new Size(180, 24),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),
            new Size(175, 23),
            new Size(105, 21),
            new Size(190, 24),
            new Size(70, 19),
            new Size(160, 22),
            new Size(125, 21),
            new Size(205, 24),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(45, 19),
            new Size(60, 19),
            new Size(145, 22),
            new Size(110, 21),
            new Size(185, 23),
            new Size(210, 25),
            new Size(65, 20),
            new Size(150, 22),
            new Size(85, 21),
            new Size(195, 23),
            new Size(120, 21),
            new Size(225, 25),
            new Size(55, 19),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(165, 22),
            new Size(95, 20),
            new Size(200, 24),
            new Size(75, 20),
            new Size(140, 21),
            new Size(215, 25),
            new Size(50, 18),

            
        };
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
            Console.WriteLine(rectangles[i]);
        }
        
        for (int i = 0; i < rectangles.Length; i++)
        {
            for (int j = i + 1; j < rectangles.Length; j++)
            {
                
                rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse(rectangles[i] + " and " + rectangles[j] + " intersect");
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
}