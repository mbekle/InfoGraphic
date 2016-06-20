using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public static class GraphicHelper
    {
        public static Point RotatePoint(Point pointToRotate, Point referencePoint, double angleInDegree)
        {
            double angleInRadian = angleInDegree * (Math.PI / 180.0);
            double cosTheta = Math.Cos(angleInRadian);
            double sinTheta = Math.Sin(angleInRadian);

            return new Point
            {
                X = (int)(cosTheta * (pointToRotate.X - referencePoint.X) - sinTheta * (pointToRotate.Y - referencePoint.Y) + referencePoint.X),
                Y = (int)(sinTheta * (pointToRotate.X - referencePoint.X) + cosTheta * (pointToRotate.Y - referencePoint.Y) + referencePoint.Y)
            };
        }
    }
}