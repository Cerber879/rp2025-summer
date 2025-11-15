namespace GeometryLib.Tests;

public class Sphere3DTests
{
  [Fact]
  public void Cannot_create_sphere_with_non_positive_radius()
  {
    Point3D center = new Point3D(0, 0, 0);
    Assert.Throws<ArgumentException>(() => new Sphere3D(center, 0));
    Assert.Throws<ArgumentException>(() => new Sphere3D(center, -1));
    Assert.Throws<ArgumentException>(() => new Sphere3D(center, -0.0001));
  }

  [Theory]
  [MemberData(nameof(DiameterTestData))]
  public void Can_get_diameter(Point3D center, double radius, double expectedDiameter)
  {
    Sphere3D sphere = new Sphere3D(center, radius);
    Assert.Equal(expectedDiameter, sphere.Diameter, precision: Point3D.Precision);
  }

  public static TheoryData<Point3D, double, double> DiameterTestData()
  {
    return new TheoryData<Point3D, double, double>
        {
            { new Point3D(0, 0, 0), 1, 2 },
            { new Point3D(0, 0, 0), 5, 10 },
            { new Point3D(10, 20, 30), 0.5, 1 },
            { new Point3D(-5, -10, -15), 2.5, 5 },
        };
  }

  [Theory]
  [MemberData(nameof(AreaTestData))]
  public void Can_get_area(Point3D center, double radius, double expectedArea)
  {
    Sphere3D sphere = new Sphere3D(center, radius);
    Assert.Equal(expectedArea, sphere.Area, precision: Point3D.Precision);
  }

  public static TheoryData<Point3D, double, double> AreaTestData()
  {
    return new TheoryData<Point3D, double, double>
        {
            { new Point3D(0, 0, 0), 1, 4 * Math.PI },
            { new Point3D(0, 0, 0), 2, 16 * Math.PI },
            { new Point3D(10, 20, 30), 0.5, Math.PI },
            { new Point3D(-5, -10, -15), 3, 36 * Math.PI },
        };
  }

  [Theory]
  [MemberData(nameof(VolumeTestData))]
  public void Can_get_volume(Point3D center, double radius, double expectedVolume)
  {
    Sphere3D sphere = new Sphere3D(center, radius);
    Assert.Equal(expectedVolume, sphere.Volume, precision: Point3D.Precision);
  }

  public static TheoryData<Point3D, double, double> VolumeTestData()
  {
    return new TheoryData<Point3D, double, double>
        {
            { new Point3D(0, 0, 0), 1, (4.0 / 3.0) * Math.PI },
            { new Point3D(0, 0, 0), 2, (4.0 / 3.0) * Math.PI * 8 },
            { new Point3D(10, 20, 30), 0.5, (4.0 / 3.0) * Math.PI * 0.125 },
            { new Point3D(-5, -10, -15), 3, (4.0 / 3.0) * Math.PI * 27 },
        };
  }

  [Theory]
  [MemberData(nameof(DistanceToPointTestData))]
  public void Can_calculate_distance_to_point(Point3D center, double radius, Point3D point, double expectedDistance)
  {
    Sphere3D sphere = new Sphere3D(center, radius);
    double result = sphere.DistanceTo(point);
    Assert.Equal(expectedDistance, result, precision: Point3D.Precision);
  }

  public static TheoryData<Point3D, double, Point3D, double> DistanceToPointTestData()
  {
    return new TheoryData<Point3D, double, Point3D, double>
        {
            // Точка в центре
            { new Point3D(0, 0, 0), 1, new Point3D(0, 0, 0), 0 },

            // Точка на поверхности
            { new Point3D(0, 0, 0), 1, new Point3D(1, 0, 0), 0 },
            { new Point3D(0, 0, 0), 2, new Point3D(0, 2, 0), 0 },

            // Точка внутри шара
            { new Point3D(0, 0, 0), 2, new Point3D(1, 0, 0), 0 },
            { new Point3D(0, 0, 0), 5, new Point3D(3, 0, 0), 0 },

            // Точка вне шара
            { new Point3D(0, 0, 0), 1, new Point3D(2, 0, 0), 1 },
            { new Point3D(0, 0, 0), 2, new Point3D(5, 0, 0), 3 },
            { new Point3D(1, 1, 1), 1, new Point3D(3, 3, 3), Math.Sqrt(12) - 1 },

            // Шар не в начале координат
            { new Point3D(10, 10, 10), 2, new Point3D(10, 10, 10), 0 },
            { new Point3D(10, 10, 10), 2, new Point3D(13, 10, 10), 1 },
        };
  }

  [Theory]
  [MemberData(nameof(DistanceToSphereTestData))]
  public void Can_calculate_distance_to_sphere(Point3D center1, double radius1, Point3D center2, double radius2, double expectedDistance)
  {
    Sphere3D sphere1 = new Sphere3D(center1, radius1);
    Sphere3D sphere2 = new Sphere3D(center2, radius2);
    double result = sphere1.DistanceTo(sphere2);
    Assert.Equal(expectedDistance, result, precision: Point3D.Precision);
  }

  public static TheoryData<Point3D, double, Point3D, double, double> DistanceToSphereTestData()
  {
    return new TheoryData<Point3D, double, Point3D, double, double>
        {
            // Шары касаются внешне
            { new Point3D(0, 0, 0), 1, new Point3D(3, 0, 0), 2, 0 },
            { new Point3D(0, 0, 0), 2, new Point3D(5, 0, 0), 3, 0 },

            // Шары пересекаются
            { new Point3D(0, 0, 0), 2, new Point3D(3, 0, 0), 2, 0 },
            { new Point3D(0, 0, 0), 5, new Point3D(8, 0, 0), 5, 0 },

            // Шары не пересекаются
            { new Point3D(0, 0, 0), 1, new Point3D(5, 0, 0), 1, 3 },
            { new Point3D(0, 0, 0), 2, new Point3D(10, 0, 0), 3, 5 },

            // Один шар внутри другого
            { new Point3D(0, 0, 0), 5, new Point3D(2, 0, 0), 1, 0 },
            { new Point3D(0, 0, 0), 10, new Point3D(0, 0, 0), 1, 0 },

            // Концентрические шары
            { new Point3D(0, 0, 0), 5, new Point3D(0, 0, 0), 2, 0 },
        };
  }

  [Theory]
  [MemberData(nameof(ContainsPointTestData))]
  public void Can_check_if_point_is_inside(Point3D center, double radius, Point3D point, bool expectedContains)
  {
    Sphere3D sphere = new Sphere3D(center, radius);
    bool result = sphere.Contains(point);
    Assert.Equal(expectedContains, result);
  }

  public static TheoryData<Point3D, double, Point3D, bool> ContainsPointTestData()
  {
    return new TheoryData<Point3D, double, Point3D, bool>
        {
            // Точка в центре
            { new Point3D(0, 0, 0), 1, new Point3D(0, 0, 0), true },

            // Точка на поверхности
            { new Point3D(0, 0, 0), 1, new Point3D(1, 0, 0), true },
            { new Point3D(0, 0, 0), 2, new Point3D(0, 2, 0), true },

            // Точка внутри
            { new Point3D(0, 0, 0), 2, new Point3D(1, 0, 0), true },
            { new Point3D(0, 0, 0), 5, new Point3D(3, 0, 0), true },
            { new Point3D(0, 0, 0), 5, new Point3D(1, 1, 1), true },

            // Точка вне шара
            { new Point3D(0, 0, 0), 1, new Point3D(2, 0, 0), false },
            { new Point3D(0, 0, 0), 2, new Point3D(5, 0, 0), false },
            { new Point3D(1, 1, 1), 1, new Point3D(3, 3, 3), false },

            // Шар не в начале координат
            { new Point3D(10, 10, 10), 2, new Point3D(10, 10, 10), true },
            { new Point3D(10, 10, 10), 2, new Point3D(11, 10, 10), true },
            { new Point3D(10, 10, 10), 2, new Point3D(13, 10, 10), false },
        };
  }

  [Theory]
  [MemberData(nameof(IntersectsWithTestData))]
  public void Can_check_if_spheres_intersect(Point3D center1, double radius1, Point3D center2, double radius2, bool expectedIntersects)
  {
    Sphere3D sphere1 = new Sphere3D(center1, radius1);
    Sphere3D sphere2 = new Sphere3D(center2, radius2);
    bool result = sphere1.IntersectsWith(sphere2);
    Assert.Equal(expectedIntersects, result);
  }

  public static TheoryData<Point3D, double, Point3D, double, bool> IntersectsWithTestData()
  {
    return new TheoryData<Point3D, double, Point3D, double, bool>
        {
            // Шары пересекаются
            { new Point3D(0, 0, 0), 2, new Point3D(3, 0, 0), 2, true },
            { new Point3D(0, 0, 0), 5, new Point3D(8, 0, 0), 5, true },
            { new Point3D(0, 0, 0), 3, new Point3D(4, 0, 0), 3, true },

            // Шары касаются внешне
            { new Point3D(0, 0, 0), 1, new Point3D(3, 0, 0), 2, true },
            { new Point3D(0, 0, 0), 2, new Point3D(5, 0, 0), 3, true },

            // Шары не пересекаются
            { new Point3D(0, 0, 0), 1, new Point3D(5, 0, 0), 1, false },
            { new Point3D(0, 0, 0), 2, new Point3D(10, 0, 0), 3, false },

            // Один шар внутри другого
            { new Point3D(0, 0, 0), 5, new Point3D(2, 0, 0), 1, true },
            { new Point3D(0, 0, 0), 10, new Point3D(0, 0, 0), 1, true },

            // Концентрические шары
            { new Point3D(0, 0, 0), 5, new Point3D(0, 0, 0), 2, true },

            // Один шар полностью внутри другого (граничный случай)
            { new Point3D(0, 0, 0), 5, new Point3D(2, 0, 0), 3, true },
        };
  }

  [Theory]
  [MemberData(nameof(ContainsSphereTestData))]
  public void Can_check_if_sphere_contains_another(Point3D center1, double radius1, Point3D center2, double radius2, bool expectedContains)
  {
    Sphere3D sphere1 = new Sphere3D(center1, radius1);
    Sphere3D sphere2 = new Sphere3D(center2, radius2);
    bool result = sphere1.Contains(sphere2);
    Assert.Equal(expectedContains, result);
  }

  public static TheoryData<Point3D, double, Point3D, double, bool> ContainsSphereTestData()
  {
    return new TheoryData<Point3D, double, Point3D, double, bool>
        {
            // Один шар полностью внутри другого
            { new Point3D(0, 0, 0), 5, new Point3D(2, 0, 0), 1, true },
            { new Point3D(0, 0, 0), 10, new Point3D(0, 0, 0), 1, true },
            { new Point3D(0, 0, 0), 10, new Point3D(5, 0, 0), 3, true },

            // Концентрические шары
            { new Point3D(0, 0, 0), 5, new Point3D(0, 0, 0), 2, true },
            { new Point3D(0, 0, 0), 5, new Point3D(0, 0, 0), 5, true },

            // Шар не полностью внутри
            { new Point3D(0, 0, 0), 5, new Point3D(2, 0, 0), 4, false },
            { new Point3D(0, 0, 0), 5, new Point3D(8, 0, 0), 1, false },

            // Шары не пересекаются
            { new Point3D(0, 0, 0), 1, new Point3D(5, 0, 0), 1, false },
            { new Point3D(0, 0, 0), 2, new Point3D(10, 0, 0), 3, false },

            // Шары пересекаются, но не один внутри другого
            { new Point3D(0, 0, 0), 3, new Point3D(4, 0, 0), 3, false },
        };
  }
}
