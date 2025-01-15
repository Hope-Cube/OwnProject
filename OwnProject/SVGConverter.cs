using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static OwnProject.MathUtilities;

namespace OwnProject
{
    /// <summary>
    /// Provides functionality for generating SVG strings from a set of points.
    /// The class constructs SVG paths using relative coordinates for efficiency.
    /// </summary>
    public static class SVGConverter
    {
        /// <summary>
        /// Generates an SVG string with a path defined by the given points.
        /// </summary>
        /// <param name="strokeWidth">The stroke width of the path.</param>
        /// <param name="strokeColor">The stroke color in hexadecimal format (e.g., "ff0000").</param>
        /// <param name="fillColor">The fill color in hexadecimal format (e.g., "00ff00").</param>
        /// <param name="size">The width and height of the SVG viewport.</param>
        /// <param name="points">A list of points defining the path.</param>
        /// <param name="isRounded">Specifies whether the coordinates should be rounded to 3 decimal places.</param>
        /// <returns>A complete SVG string with the specified path and styling.</returns>
        /// <exception cref="ArgumentException">Thrown when the points list is null or empty.</exception>
        public static string GenerateSVG(double strokeWidth, string strokeColor, string fillColor, double size, List<PointD> points, bool isRounded)
        {
            // Validate input
            if (points == null || points.Count == 0)
                throw new ArgumentException("The points list must contain at least one point.", nameof(points));

            // Calculate the viewBox dimensions
            double minX = points.Min(p => p.X);
            double minY = points.Min(p => p.Y);
            double width = points.Max(p => p.X) - minX;
            double height = points.Max(p => p.Y) - minY;

            // Apply rounding if specified
            if (isRounded)
            {
                minX = Math.Round(minX, 3);
                minY = Math.Round(minY, 3);
                width = Math.Round(width, 3);
                height = Math.Round(height, 3);
            }


            // Build the SVG header
            string svgHeader = $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"{minX.ToString(CultureInfo.InvariantCulture)} {minY.ToString(CultureInfo.InvariantCulture)} {width.ToString(CultureInfo.InvariantCulture)} {height.ToString(CultureInfo.InvariantCulture)}\" width=\"{size.ToString(CultureInfo.InvariantCulture)}\" height=\"{size.ToString(CultureInfo.InvariantCulture)}\">";
            string pathStyle = $"<path stroke-width=\"{strokeWidth.ToString(CultureInfo.InvariantCulture)}\" stroke=\"#{strokeColor}\" fill=\"#{fillColor}\" d=\"m";

            // Start path data
            double startingX = points[0].X;
            double startingY = points[0].Y;

            //Apply rounding if specified
            if (isRounded)
            {
                startingX = Math.Round(startingX, 3);
                startingY = Math.Round(startingY, 3);
            }

            string pathData = $"{startingX.ToString(CultureInfo.InvariantCulture)} {startingY.ToString(CultureInfo.InvariantCulture)}";
            double currentX = points[0].X;
            double currentY = points[0].Y;

            // Generate relative coordinates for the path
            for (int i = 1; i < points.Count; i++)
            {
                // Calculate deltas
                double deltaX = points[i].X - currentX;
                double deltaY = points[i].Y - currentY;

                // Apply rounding if specified
                if (isRounded)
                {
                    deltaX = Math.Round(deltaX, 3);
                    deltaY = Math.Round(deltaY, 3);
                }

                // Append relative coordinates
                pathData += $" {deltaX.ToString(CultureInfo.InvariantCulture)} {deltaY.ToString(CultureInfo.InvariantCulture)}";

                // Update current position
                currentX = points[i].X;
                currentY = points[i].Y;
            }

            // Close the path and complete the SVG
            string svg = $"{svgHeader}{pathStyle.Replace("stroke-width=\"0", "stroke-width=\"")}{pathData.Replace(" -", "-")}z\"/></svg>";

            return svg;
        }
    }
}