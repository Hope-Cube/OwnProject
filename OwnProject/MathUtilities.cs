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
    }
}