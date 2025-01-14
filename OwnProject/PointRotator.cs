using System;
using System.Collections.Generic;
using System.Linq;
using static OwnProject.MathUtilities;

namespace OwnProject
{
    /// <summary>
    /// Provides utility methods for rotating points or collections of points around a specified origin or center.
    /// Includes precision adjustment to handle floating-point inaccuracies.
    /// </summary>
    internal class PointRotator
    {
        /// <summary>
        /// Represents a point in 2D space with double precision coordinates.
        /// </summary>
        public struct PointD
        {
            /// <summary>
            /// The X coordinate of the point.
            /// </summary>
            public double X { get; }

            /// <summary>
            /// The Y coordinate of the point.
            /// </summary>
            public double Y { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="PointD"/> structure.
            /// </summary>
            /// <param name="x">The X coordinate of the point.</param>
            /// <param name="y">The Y coordinate of the point.</param>
            public PointD(double x, double y)
            {
                X = x;
                Y = y;
            }

            /// <summary>
            /// Returns a string representation of the point.
            /// </summary>
            /// <returns>A string in the format "(X, Y)".</returns>
            public override string ToString() => $"({X}, {Y})";
        }

        /// <summary>
        /// Adjusts a point's coordinates to mitigate floating-point precision errors by rounding very small values to zero.
        /// </summary>
        /// <param name="point">The point to adjust.</param>
        /// <param name="epsilon">The precision threshold (default is 1e-10).</param>
        /// <returns>The adjusted point with small values rounded to zero.</returns>
        private static PointD AdjustForPrecision(PointD point, double epsilon = 1e-10)
        {
            return new PointD(
                Math.Abs(point.X) < epsilon ? 0 : point.X,
                Math.Abs(point.Y) < epsilon ? 0 : point.Y
            );
        }

        /// <summary>
        /// Rotates a point around a specified center by a given angle in radians.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="angleInRadians">The rotation angle in radians.</param>
        /// <param name="center">The center of rotation.</param>
        /// <returns>The rotated point as a <see cref="PointD"/>.</returns>
        private static PointD RotatePointInternal(PointD point, double angleInRadians, PointD center)
        {
            // Offset the point from the center
            double offsetX = point.X - center.X;
            double offsetY = point.Y - center.Y;

            // Apply the rotation formula
            double rotatedX = offsetX * Math.Cos(angleInRadians) - offsetY * Math.Sin(angleInRadians) + center.X;
            double rotatedY = offsetX * Math.Sin(angleInRadians) + offsetY * Math.Cos(angleInRadians) + center.Y;

            return AdjustForPrecision(new PointD(rotatedX, rotatedY));
        }

        /// <summary>
        /// Rotates a point around a specified center by a given angle in degrees.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="angleInDegrees">The rotation angle in degrees.</param>
        /// <param name="center">The center point around which to rotate.</param>
        /// <param name="clockwise">True for clockwise rotation, false for counterclockwise.</param>
        /// <returns>The rotated point as a <see cref="PointD"/>.</returns>
        public static PointD RotatePoint(PointD point, double angleInDegrees, PointD center, bool clockwise = true)
        {
            double angleInRadians = DegreesToRadians(clockwise ? -angleInDegrees : angleInDegrees);
            return RotatePointInternal(point, angleInRadians, center);
        }

        /// <summary>
        /// Rotates a collection of points around a specified center by a specified angle in degrees.
        /// </summary>
        /// <param name="points">The collection of points to rotate.</param>
        /// <param name="angleInDegrees">The rotation angle in degrees.</param>
        /// <param name="center">The center point around which to rotate.</param>
        /// <param name="clockwise">True for clockwise rotation, false for counterclockwise.</param>
        /// <returns>A new collection of rotated points as <see cref="PointD"/> objects.</returns>
        public static List<PointD> RotatePoints(List<PointD> points, double angleInDegrees, PointD center, bool clockwise = true)
        {
            double angleInRadians = DegreesToRadians(clockwise ? -angleInDegrees : angleInDegrees);
            return points.Select(point => RotatePointInternal(point, angleInRadians, center)).ToList();
        }
    }
}
