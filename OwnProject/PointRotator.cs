using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// Rotates a point around a specified center by a given angle in radians.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="angleInRadians">The rotation angle in radians.</param>
        /// <param name="center">The center of rotation.</param>
        /// <returns>The rotated point as a new <see cref="PointF"/>.</returns>
        private static PointF RotatePointInternal(PointF point, double angleInRadians, PointF center)
        {
            double offsetX = point.X - center.X;
            double offsetY = point.Y - center.Y;

            double rotatedX = offsetX * Math.Cos(angleInRadians) - offsetY * Math.Sin(angleInRadians) + center.X;
            double rotatedY = offsetX * Math.Sin(angleInRadians) + offsetY * Math.Cos(angleInRadians) + center.Y;

            return AdjustForPrecision(new PointF((float)rotatedX, (float)rotatedY));
        }

        /// <summary>
        /// Rotates a point around the origin or a specified center by a specified angle in degrees.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="angleInDegrees">The rotation angle in degrees.</param>
        /// <param name="center">The center point around which to rotate (default is the origin).</param>
        /// <param name="clockwise">True for clockwise rotation, false for counterclockwise.</param>
        /// <returns>The rotated point as a new <see cref="PointF"/>.</returns>
        public static PointF RotatePoint(PointF point, double angleInDegrees, PointF? center = null, bool clockwise = true)
        {
            double angleInRadians = DegreesToRadians(clockwise ? -angleInDegrees : angleInDegrees);
            return RotatePointInternal(point, angleInRadians, center ?? new PointF(0, 0));
        }

        /// <summary>
        /// Rotates a collection of points around the origin or a specified center by a specified angle in degrees.
        /// </summary>
        /// <param name="points">The collection of points to rotate.</param>
        /// <param name="angleInDegrees">The rotation angle in degrees.</param>
        /// <param name="center">The center point around which to rotate (default is the origin).</param>
        /// <param name="clockwise">True for clockwise rotation, false for counterclockwise.</param>
        /// <returns>A new collection of rotated points.</returns>
        public static List<PointF> RotatePoints(List<PointF> points, double angleInDegrees, PointF? center = null, bool clockwise = true)
        {
            double angleInRadians = DegreesToRadians(clockwise ? -angleInDegrees : angleInDegrees);
            PointF rotationCenter = center ?? new PointF(0, 0);

            return points.Select(point => RotatePointInternal(point, angleInRadians, rotationCenter)).ToList();
        }

        /// <summary>
        /// Rotates a collection of points around a specified center multiple times, dividing a full rotation into equal increments.
        /// </summary>
        /// <param name="points">The collection of points to rotate.</param>
        /// <param name="numOfRotations">The number of rotations to divide the full circle into.</param>
        /// <param name="center">The center point around which to rotate (default is the origin).</param>
        /// <param name="clockwise">True for clockwise rotation, false for counterclockwise.</param>
        /// <returns>A new collection of points with all rotated positions, including the original points.</returns>
        public static List<PointF> RotatePoints(List<PointF> points, int numOfRotations, PointF? center = null, bool clockwise = true)
        {
            if (numOfRotations <= 0)
                throw new ArgumentException("Number of rotations must be greater than zero.", nameof(numOfRotations));

            double stepAngleInDegrees = 360.0 / numOfRotations;
            PointF rotationCenter = center ?? new PointF(0, 0);

            // Collect all rotated points
            List<PointF> result = new List<PointF>(points);
            for (int i = 1; i < numOfRotations; i++) // Start from the first rotation
            {
                double currentAngleInRadians = DegreesToRadians(clockwise ? -stepAngleInDegrees * i : stepAngleInDegrees * i);
                result.AddRange(points.Select(point => RotatePointInternal(point, currentAngleInRadians, rotationCenter)));
            }

            return result.Distinct().ToList();
        }

        /// <summary>
        /// Adjusts a point's coordinates to mitigate floating-point precision errors by rounding very small values to zero.
        /// </summary>
        /// <param name="point">The point to adjust for precision.</param>
        /// <param name="epsilon">The threshold below which values are treated as zero.</param>
        /// <returns>The adjusted point with small values rounded to zero.</returns>
        private static PointF AdjustForPrecision(PointF point, double epsilon = 1e-6)
        {
            return new PointF(
                Math.Abs(point.X) < epsilon ? 0 : point.X,
                Math.Abs(point.Y) < epsilon ? 0 : point.Y
            );
        }
    }
}
