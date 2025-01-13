using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace OwnProject
{
    class SVGConverter
    {
        /// <summary>
        /// Generates an SVG string with a path defined by the given points.
        /// The path is constructed using relative coordinates for optimization.
        /// </summary>
        /// <param name="stroke_width">The stroke width of the path.</param>
        /// <param name="stroke">The stroke color in hexadecimal format (e.g., "ff0000").</param>
        /// <param name="fill">The fill color in hexadecimal format (e.g., "00ff00").</param>
        /// <param name="size">The width and height of the SVG viewport.</param>
        /// <param name="points">A list of points defining the path.</param>
        /// <returns>A complete SVG string with the specified path and styling.</returns>
        /// <exception cref="ArgumentException">Thrown when the points list is null or empty.</exception>
        public static string SVGGen(double stroke_width, string stroke, string fill, double size, List<PointF> points)
        {
            if (points == null || points.Count == 0)
                throw new ArgumentException("The points list must contain at least one point.");

            // Calculate the viewBox dimensions
            string svg = $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"{points.Select(p => p.X).Min()} {points.Select(p => p.Y).Min()} {points.Select(p => p.X).Max() - points.Select(p => p.X).Min()} {points.Select(p => p.Y).Max() - points.Select(p => p.Y).Min()}\" width=\"{size}\" height=\"{size}\"><path stroke-width=\"{stroke_width.ToString().Replace(',','.')}\" stroke=\"#{stroke}\" fill=\"#{fill}\" d=\"m{points[0].X} {points[0].Y}";

            // Initialize the current position
            double currentX = points[0].X;
            double currentY = points[0].Y;

            // Build the path with relative coordinates
            for (int i = 1; i < points.Count; i++)
            {
                double dX = points[i].X - currentX;
                double dY = points[i].Y - currentY;

                // Append the deltas to the SVG string with proper formatting
                svg += $" {dX.ToString(CultureInfo.InvariantCulture)} {dY.ToString(CultureInfo.InvariantCulture)}";

                // Update the current position
                currentX = points[i].X;
                currentY = points[i].Y;
            }

            // Close the path and SVG
            svg += "z\"/></svg>";
            svg = svg.Replace(" -", "-"); // Ensure no unnecessary spaces before the 'z'

            return svg;
        }
    }
}