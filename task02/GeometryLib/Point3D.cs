using System.Diagnostics.CodeAnalysis;

namespace GeometryLib;

public readonly struct Point3D(double x, double y, double z)
    : IEquatable<Point3D>
{
  public const double Tolerance = 1e-10;
  public const int Precision = 10;

  public double X { get; } = x;

  public double Y { get; } = y;

  public double Z { get; } = z;

  public static bool operator ==(Point3D left, Point3D right) => left.Equals(right);

  public static bool operator !=(Point3D left, Point3D right) => !(left == right);

  public double DistanceTo(Point3D other)
  {
    double dx = X - other.X;
    double dy = Y - other.Y;
    double dz = Z - other.Z;
    return Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
  }

  public bool Equals(Point3D other)
  {
    return Math.Abs(X - other.X) < Tolerance
           && Math.Abs(Y - other.Y) < Tolerance
           && Math.Abs(Z - other.Z) < Tolerance;
  }

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj is Point3D other)
    {
      return Equals(other);
    }

    return false;
  }

  public override int GetHashCode()
  {
    return (X, Y, Z).GetHashCode();
  }

  public override string ToString()
  {
    return $"({X}, {Y}, {Z})";
  }
}
