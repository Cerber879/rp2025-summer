namespace GeometryLib.Tests;

public class Point3DTests
{
  [Theory]
  [MemberData(nameof(DistanceToTestData))]
  public void Can_calculate_distance_to_another_point(Point3D point1, Point3D point2, double expectedDistance)
  {
    double result = point1.DistanceTo(point2);
    Assert.Equal(expectedDistance, result, precision: Point3D.Precision);
  }

  public static TheoryData<Point3D, Point3D, double> DistanceToTestData()
  {
    return new TheoryData<Point3D, Point3D, double>
        {
            // Расстояние от точки до самой себя
            { new Point3D(0, 0, 0), new Point3D(0, 0, 0), 0 },
            { new Point3D(1, 2, 3), new Point3D(1, 2, 3), 0 },
            { new Point3D(-5, 10, -15), new Point3D(-5, 10, -15), 0 },

            // Расстояние до точки на оси
            { new Point3D(0, 0, 0), new Point3D(3, 0, 0), 3 },
            { new Point3D(0, 0, 0), new Point3D(0, 4, 0), 4 },
            { new Point3D(0, 0, 0), new Point3D(0, 0, 5), 5 },

            // Расстояние в пространстве
            { new Point3D(0, 0, 0), new Point3D(3, 4, 0), 5 },
            { new Point3D(0, 0, 0), new Point3D(1, 1, 1), Math.Sqrt(3) },
            { new Point3D(1, 2, 3), new Point3D(4, 6, 9), Math.Sqrt(61) },

            // Отрицательные координаты
            { new Point3D(-1, -2, -3), new Point3D(1, 2, 3), Math.Sqrt(56) },
            { new Point3D(-5, 0, 0), new Point3D(5, 0, 0), 10 },

            // Большие числа
            { new Point3D(100, 200, 300), new Point3D(101, 201, 301), Math.Sqrt(3) },
        };
  }

  [Theory]
  [MemberData(nameof(EqualityTestData))]
  public void Can_check_points_equality(Point3D point1, Point3D point2, bool expectedEqual)
  {
    Assert.Equal(expectedEqual, point1 == point2);
    Assert.Equal(expectedEqual, point1.Equals(point2));
    Assert.Equal(expectedEqual, point2.Equals(point1));
    Assert.Equal(!expectedEqual, point1 != point2);
  }

  public static TheoryData<Point3D, Point3D, bool> EqualityTestData()
  {
    return new TheoryData<Point3D, Point3D, bool>
        {
            // Равные точки
            { new Point3D(0, 0, 0), new Point3D(0, 0, 0), true },
            { new Point3D(1, 2, 3), new Point3D(1, 2, 3), true },
            { new Point3D(-5, 10, -15), new Point3D(-5, 10, -15), true },

            // Точки в пределах Tolerance
            { new Point3D(0, 0, 0), new Point3D(Point3D.Tolerance / 2, 0, 0), true },
            { new Point3D(1, 1, 1), new Point3D(1 + Point3D.Tolerance / 2, 1, 1), true },

            // Неравные точки
            { new Point3D(0, 0, 0), new Point3D(1, 0, 0), false },
            { new Point3D(1, 2, 3), new Point3D(3, 2, 1), false },
            { new Point3D(0, 0, 0), new Point3D(Point3D.Tolerance * 2, 0, 0), false },
        };
  }
}
