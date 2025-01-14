using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using static OwnProject.PointRotator;
using static System.Console;

namespace OwnProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<PointF> points = new List<PointF>();
            //using (StreamReader sR = new StreamReader("pointstest.txt"))
            //{
            //    while (!sR.EndOfStream)
            //    {
            //        var lineParts = sR.ReadLine().Split(',');
            //        points.Add(new PointF(float.Parse(lineParts[0]), float.Parse(lineParts[1])));
            //    }
            //}
            //List<PointF> points = new List<PointF>
            //{
            //    new PointF(-2, 2),
            //    new PointF(-1, 4),
            //    new PointF(0, 7),
            //    new PointF(1, 4),
            //    new PointF(2, 2)
            //};
            PointD rotatedPoint = PointRotator.RotatePoint(new PointD(-28.33632505,74.17495355 - 28.33632505), 24.092136856491987, new PointD(74.17495355, 0), false);
            //WriteLine(string.Join(",\n", rotatedPoints));

            //using (StreamWriter sW = new StreamWriter("test1.svg"))
            //{
            //    sW.Write(SVGConverter.SVGGen(.5, "ddd", "444", 200, rotatedPoints));
            //}
            WriteLine(rotatedPoint);

            ReadKey(true);
        }
    }
}