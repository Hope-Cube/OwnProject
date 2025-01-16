using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        /// Generates an SVG file with a path defined by the given points.
        /// </summary>
        /// <param name="name">The name of the SVG file to create (without extension).</param>
        /// <param name="strokeWidth">The stroke width of the path.</param>
        /// <param name="strokeColor">The stroke color in hexadecimal format (e.g., "ff0000").</param>
        /// <param name="fillColor">The fill color in hexadecimal format (e.g., "00ff00").</param>
        /// <param name="size">The width and height of the SVG viewport.</param>
        /// <param name="points">A list of points defining the path.</param>
        /// <param name="isRounded">Specifies whether the coordinates should be rounded to 3 decimal places.</param>
        /// <exception cref="ArgumentException">Thrown when the points list or filename is invalid.</exception>
        /// <exception cref="IOException">Thrown when an error occurs while writing the file.</exception>
        public static void GenerateSVG(string name, double strokeWidth, string strokeColor, string fillColor, double size, List<PointD> points, bool isRounded)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The file name must not be null or empty.", nameof(name));

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

            // Build the path data
            var pathDataBuilder = new System.Text.StringBuilder();
            double currentX = points[0].X;
            double currentY = points[0].Y;

            // Apply rounding to the starting point if required
            if (isRounded)
            {
                currentX = Math.Round(currentX, 3);
                currentY = Math.Round(currentY, 3);
            }

            pathDataBuilder.Append($"{currentX.ToString(CultureInfo.InvariantCulture)} {currentY.ToString(CultureInfo.InvariantCulture)}");

            for (int i = 1; i < points.Count; i++)
            {
                double deltaX = points[i].X - currentX;
                double deltaY = points[i].Y - currentY;

                if (isRounded)
                {
                    deltaX = Math.Round(deltaX, 3);
                    deltaY = Math.Round(deltaY, 3);
                }

                pathDataBuilder.Append($" {deltaX.ToString(CultureInfo.InvariantCulture)} {deltaY.ToString(CultureInfo.InvariantCulture)}");

                currentX = points[i].X;
                currentY = points[i].Y;
            }

            // Close the path and complete the SVG
            string svg = $"{svgHeader}{pathStyle}{pathDataBuilder}z\"/></svg>";

            // Write to file
            try
            {
                using (var writer = new StreamWriter($"{name}.svg"))
                {
                    writer.Write(svg);
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Failed to write the SVG file: {ex.Message}", ex);
            }
        }
    }
}