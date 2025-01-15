using System.Collections.Generic;
using System.IO;
using static OwnProject.MathUtilities;
using static OwnProject.PointRotator;
using static System.Console;

namespace OwnProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PointD> points = new List<PointD>();
            using (StreamReader sR = new StreamReader("pointstest.txt"))
            {
                while (!sR.EndOfStream)
                {
                    var lineParts = sR.ReadLine().Split(';');
                    points.Add(new PointD(double.Parse(lineParts[0]), double.Parse(lineParts[1])));
                }
            }
            //Test point list
            //List<PointF> points = new List<PointF>
            //{
            //    new PointF(-2, 2),
            //    new PointF(-1, 4),
            //    new PointF(0, 7),
            //    new PointF(1, 4),
            //    new PointF(2, 2)
            //};
            List<PointD> rotatedPoints = new List<PointD>();
            PointD center = new PointD(74.17495355, 0);
            double angleInDegrees = 24.092136856491987 + 180;
            rotatedPoints = RotatePoints(points, angleInDegrees, center, false);
            WriteLine(string.Join(",\n", rotatedPoints));

            using (StreamWriter sW = new StreamWriter("test.svg"))
            {
                sW.Write(SVGConverter.GenerateSVG(.5,"444", "ddd", 800, rotatedPoints, true));
            }

            ReadKey(true);
        }
    }
}