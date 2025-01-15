using System;

namespace OwnProject
{
    /// <summary>
    /// Provides common mathematical utility functions for general-purpose calculations.
    /// </summary>
    public static class MathUtilities
    {
        /// <summary>
        /// Converts an angle in degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The equivalent angle in radians.</returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        
        /// <summary>
        /// Calculates the angle in degrees between two points relative to a center point.
        /// </summary>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <param name="center">The center point.</param>
        /// <returns>The angle in degrees between the two points relative to the center.</returns>
        public static double CalculateAngle(PointD p1, PointD p2, PointD center)
        {
            // Calculate vectors relative to the center
            double vector1X = p1.X - center.X;
            double vector1Y = p1.Y - center.Y;
            double vector2X = p2.X - center.X;
            double vector2Y = p2.Y - center.Y;

            // Calculate the dot product and magnitudes of the vectors
            double dotProduct = vector1X * vector2X + vector1Y * vector2Y;
            double magnitude1 = Math.Sqrt(vector1X * vector1X + vector1Y * vector1Y);
            double magnitude2 = Math.Sqrt(vector2X * vector2X + vector2Y * vector2Y);

            // Prevent division by zero in case of zero-length vectors
            if (magnitude1 == 0 || magnitude2 == 0)
                throw new ArgumentException("One or both points are at the center, resulting in zero-length vectors.");

            // Calculate the angle in radians using the arccosine of the dot product divided by magnitudes
            double angleInRadians = Math.Acos(dotProduct / (magnitude1 * magnitude2));

            // Convert radians to degrees
            return angleInRadians * 180.0 / Math.PI;
        }

        /// <summary>
        /// Converts an angle in radians to degrees.
        /// </summary>
        /// <param name="angleInRadians">The angle in radians.</param>
        /// <returns>The equivalent angle in degrees.</returns>
        public static double RadiansInDegrees(double angleInRadians)
        {
            // Convert radians to degrees
            return angleInRadians * 180.0 / Math.PI;
        }

        /// <summary>
        /// Represents a point in 2D space with double precision coordinates.
        /// </summary>
        public readonly struct PointD
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
    }
}