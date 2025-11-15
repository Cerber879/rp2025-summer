namespace GeometryLib;

public class Sphere3D
{
  private readonly Point3D center;
  private readonly double radius;

  public Sphere3D(Point3D center, double radius)
  {
    if (radius <= 0)
    {
      throw new ArgumentException("Radius must be positive", nameof(radius));
    }

    this.center = center;
    this.radius = radius;
  }

  public Point3D Center => center;

  public double Radius => radius;

  public double Diameter => 2.0 * radius;

  public double Area => 4.0 * Math.PI * radius * radius;

  public double Volume => (4.0 / 3.0) * Math.PI * radius * radius * radius;

  public double DistanceTo(Point3D p)
  {
    double distanceToCenter = center.DistanceTo(p);
    double distanceToSurface = distanceToCenter - radius;
    return Math.Max(0.0, distanceToSurface);
  }

  public double DistanceTo(Sphere3D other)
  {
    double distanceBetweenCenters = center.DistanceTo(other.center);
    double distanceBetweenSurfaces = distanceBetweenCenters - radius - other.radius;
    return Math.Max(0.0, distanceBetweenSurfaces);
  }

  public bool Contains(Point3D p)
  {
    double distanceToCenter = center.DistanceTo(p);
    return distanceToCenter <= radius;
  }

  public bool IntersectsWith(Sphere3D other)
  {
    double distanceBetweenCenters = center.DistanceTo(other.center);
    double sumOfRadii = radius + other.radius;
    return distanceBetweenCenters <= sumOfRadii;
  }

  public bool Contains(Sphere3D other)
  {
    double distanceBetweenCenters = center.DistanceTo(other.center);
    return distanceBetweenCenters + other.radius <= radius;
  }
}
