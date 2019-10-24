using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace Note.MathUtils
{
    public static class BasicGeometry
    {
        public static double RectangleArea(this double length, double width)
        {
            return length * width;
        }

        public static double RectanglePerimeter(this double length, double width)
        {
            return (2 * length) + (2 * width);
        }
        public static double ParellelogramArea(this double length, double height)
        {
            return RectangleArea(length, height);
        }

        public static double ParellelogramPerimeter(this double length, double width)
        {
            return RectanglePerimeter(length, width);
        }

        public static double TrapezoidArea(this double height, double base1, double base2)
        {
            return 0.5 * height * (base1 + base2);
        }

        public static double TrapezoidPerimeter(this double side1, double side2, double base1, double base2)
        {
            return side1 + side2 + base1 + base2;
        }

        public static double TriangleArea(this double height, double _base)
        {
            return TrapezoidArea(height, _base, 0);
        }
        public static double TrianglePerimeter(this double side1, double side2, double _base)
        {
            return TrapezoidPerimeter(side1, side2, _base, 0);
        }

        public static double CircleArea(this double radius)
        {
            return PI * radius * radius;
        }

        public static double CirclePerimeter(this double radius)
        {
            return 2 * PI * radius;
        }

        public static double EllipseArea(this double semiMajor, double semiMinor)
        {
            return PI * semiMajor * semiMinor;
        }

        public static double EllipseCircumference(this double semiMajor, double semiMinor)
        {
            double a = semiMajor;
            double b = semiMinor;
            return PI * (3 * (a + b) - Sqrt((a + 3 * b) * (b + 3 * a)));
        }

        public static double KiteArea(this double diagMajor, double diagMinor)
        {
            return (diagMajor * diagMinor) / 2;
        }

        public static double KitePerimeter(this double sideMajor, double sideMinor)
        {
            return 2 * (sideMajor + sideMinor);
        }

        public static double RegularPolygonArea(this double sideLength, double numSides)
        {
            double s = sideLength;
            double n = numSides;
            return (n * Pow(RegularPolygonCircumRadius(s, n), 2) * Sin((2 * PI) / n)) / 2;
        }

        public static double RegularPolygonCircumRadius(this double sideLength, double numSides)
        {
            double s = sideLength;
            double n = numSides;
            return (s * (1 / Sin(PI / n))) / 2;
        }

        public static double RectangularPrismVolume(this double length, double width, double height)
        {
            return length * width * height;
        }

        public static double RectangularPrismSurfaceArea(this double length, double width, double height)
        {
            return (2 * length * height) + (2 * width * height) + (2 * width * length);
        }

        public static double CircularCylinderVolume(this double radius, double height)
        {
            return CircleArea(radius) * height;
        }

        public static double CircularCylinderSurfaceArea(this double radius, double height)
        {
            return (2 * PI) * (radius * height) + (2 * PI) * (radius * radius);
        }

        public static double SphereVolume(this double radius)
        {
            return ((4 / 3) * PI) * (radius * radius * radius);
        }

        public static double SphereSurfaceArea(this double radius)
        {
            return (4 * PI) * (radius * radius);
        }

        public static double CircularConeVolume(this double radius, double height)
        {
            return  (PI * (radius * radius) * height) / 3;
        }

        public static double CircularConeSurfaceArea(this double radius, double height)
        {
            return PI * radius * Sqrt((radius * radius) + (height * height));
        }

        public static double RectangularPyramidVolume(this double length, double width, double height)
        {
            return (length * width * height) / 3;
        }

        public static double RectangularPyramidSurfaceArea(this double length, double width, double height)
        {
            double portionOne   = (length * width) + length;
            double portionTwo   = Sqrt(Pow(width / 2, 2) + (height * height));
            double portionThree = Sqrt(Pow(length / 2, 2) + (height * height));

            return (portionOne * portionTwo) + width * portionThree;
        }

        public static double CircleSector(double theta, double radius)
        {
            return (theta * PI * radius * radius) / 360;
        }

        public static double CircleSectorPerimeter(double length, double radius)
        {
            return (2 * radius) + length;
        }

        public static double PythagoreanForHypotenuse(double side1, double side2)
        {
            return Sqrt((side1 * side1) + (side2 * side2));
        }

        public static double PythagoreanWithHypotenuse(double side, double hypotenuse)
        {
            return Sqrt((hypotenuse * hypotenuse) - (side * side));
        }
    }
}
