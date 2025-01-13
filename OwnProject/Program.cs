using System.Collections.Generic;
using System.Drawing;
using System.IO;
using static System.Console;

namespace OwnProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PointF> points = new List<PointF>
            {
                new PointF(-2, 2),
                new PointF(-1, 4),
                new PointF(0, 7),
                new PointF(1, 4),
                new PointF(2, 2)
            };
            List<PointF> rotatedPoints = PointRotator.RotatePoints(points, 10, new PointF(0, 0), true);
            WriteLine(string.Join(",\n", rotatedPoints));

            using(StreamWriter sW = new StreamWriter("test1.svg"))
            {
                sW.Write(SVGConverter.SVGGen(.5, "ddd", "444", 200, rotatedPoints));
            }

            ReadKey(true);
        }
    }
}