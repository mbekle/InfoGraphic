using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        GraphicsPath path1 = new GraphicsPath();
        GraphicsPath path2 = new GraphicsPath();
        PointF org1;
        PointF org2;
        //RectangleF rect1 = new RectangleF(200, 10, 100, 100);
        //RectangleF rect2 = new RectangleF(20, 150, 70, 70);

        RectangleF rect1 = new RectangleF(158, 64, 100, 100);
        RectangleF rect2 = new RectangleF(183, 283, 70, 70);


        public Form1()
        {
            InitializeComponent();

            //DoubleBuffered = true;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);


            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);


            InitializePath(path1, 0, 0);
            InitializePath(path2, 0, 0);
        }

        private void InitializePath(GraphicsPath path, float deltaX, float deltaY)
        {
            path.Reset();

            if (path == path1)
            {
                rect1.Offset(deltaX, deltaY);
                org1 = new PointF(rect1.Left + rect1.Width / 2, rect1.Top + rect1.Height / 2);
                path1.AddRectangle(rect1);
            }
            else
            {
                rect2.Offset(deltaX, deltaY);
                org2 = new PointF(rect2.Left + rect2.Width / 2, rect2.Top + rect2.Height / 2);
                path2.AddEllipse(rect2);
            }

            path.CloseFigure();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //float opposite = org2.Y - org1.Y;
            //float adjacent = org2.X - org1.X;
            //float angleRad = (float)Math.Atan(opposite / adjacent);

            //PointF nearestPoint1 = org1;
            //bool nearestPoint1Found = false;
            //PointF nearestPoint2 = org2;
            //bool nearestPoint2Found = false;
            ////List<PointF> plist = new List<PointF>();

            //float sign = (adjacent < 0 ? -1 : 1);
            //adjacent = Math.Abs(adjacent);
            //for (float i = 1; i < adjacent; ++i)
            //{
            //    if (nearestPoint1Found == false)
            //    {
            //        nearestPoint1 = new PointF(org1.X + i * sign, org1.Y + (float)Math.Tan(angleRad) * i * sign);

            //        if (path1.IsVisible(nearestPoint1) == false)
            //        {
            //            nearestPoint1Found = true;
            //        }
            //    }

            //    if (nearestPoint2Found == false)
            //    {
            //        nearestPoint2 = new PointF(org2.X - i * sign, org2.Y - (float)Math.Tan(angleRad) * i * sign);

            //        if (path2.IsVisible(nearestPoint2) == false)
            //        {
            //            nearestPoint2Found = true;
            //        }
            //    }

            //    if (nearestPoint1Found && nearestPoint2Found)
            //        break;
            //}


            //g.DrawLine(Pens.Blue, nearestPoint1, nearestPoint2);












            /*
            Graphics g = this.CreateGraphics();

            PointF p1 = new PointF(20, 100);
            PointF p2 = new PointF(100, 80);

            g.DrawLine(Pens.Blue, p1, p2);


            ////
            float a = p2.Y - p1.Y;
            float b = p2.X - p1.X;
            float angleRad = (float)Math.Atan(a / b);


            List<PointF> points = new List<PointF>();

            for (float i = 1f; i < b; ++i)
            {
                points.Add(new PointF(p1.X + i, p1.Y + (float)Math.Tan(angleRad) * i));
            }

            g.DrawLines(Pens.Red, points.ToArray());
            ///
            */



            //g.Dispose();




            //Text = Math.Sin(30 * Math.PI/180).ToString();
            //Text = (Math.Asin(Math.Sin(30 * Math.PI / 180)) * (180 / Math.PI) ).ToString();

            /*
d = sqrt((x2-x1)^2 + (y2 - y1)^2) #distance
r = n / d #segment ratio

x3 = r * x2 + (1 - r) * x1 #find point that divides the segment
y3 = r * y2 + (1 - r) * y1 #into the ratio (1-r):r             
             */
        }

        private bool path1In = false;
        private bool path2In = false;
        private int mouseDX;
        private int mouseDY;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (path1In || path2In))
            {
                mouseDX = e.X;
                mouseDY = e.Y;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (path1.IsVisible(e.Location))
            {
                if (path1In == false)
                {
                    path1In = true;

                    //using (Graphics g = CreateGraphics())
                    //    g.DrawPath(Pens.Red, path1);
                }

                if (e.Button == MouseButtons.Left)
                {
                    InitializePath(path1, e.X - mouseDX, e.Y - mouseDY);
                    mouseDX = e.X;
                    mouseDY = e.Y;
                    textBox1.Text = "org1 --> " + org1.ToString() + "     rect1 --> " + rect1.ToString();
                    this.Invalidate();
                }
            }
            else
            {
                if (path1In == true)
                {
                    path1In = false;

                    //using (Graphics g = CreateGraphics())
                    //    g.DrawPath(Pens.Black, path1);
                }
            }

            // path2
            if (path2.IsVisible(e.Location))
            {
                if (path2In == false)
                {
                    path2In = true;

                    //using (Graphics g = CreateGraphics())
                    //    g.DrawPath(Pens.Red, path2);
                }

                if (e.Button == MouseButtons.Left)
                {
                    InitializePath(path2, e.X - mouseDX, e.Y - mouseDY);
                    mouseDX = e.X;
                    mouseDY = e.Y;
                    textBox2.Text = "org2 --> " + org2.ToString() + "     rect2 --> " + rect2.ToString();
                    this.Invalidate();
                }
            }
            else
            {
                if (path2In == true)
                {
                    path2In = false;

                    //using (Graphics g = CreateGraphics())
                    //    g.DrawPath(Pens.Black, path2);
                }
            }
        }

        private void GetNearestPoints(PointF orgPoint1, PointF orgPoint2, out PointF nearestPoint1, out PointF nearestPoint2)
        {
            bool nearestPoint1Found = false;
            bool nearestPoint2Found = false;
            float opposite = orgPoint2.Y - orgPoint1.Y;
            float adjacent = orgPoint2.X - orgPoint1.X;
            float angleRad = (float)Math.Atan(opposite / adjacent);

            float sign;
            float limit;
            bool isAdjacent = (Math.Abs(adjacent) > Math.Abs(opposite));
            if (isAdjacent)
            {
                limit = Math.Abs(adjacent);
                sign = (adjacent < 0 ? -1 : 1);
            }
            else
            {
                limit = Math.Abs(opposite);
                sign = (opposite < 0 ? -1 : 1);
            }

            nearestPoint1 = orgPoint1;
            nearestPoint2 = orgPoint2;

            for (float i = 1; i < limit; ++i)
            {
                if (nearestPoint1Found == false)
                {
                    nearestPoint1 = isAdjacent ? new PointF(orgPoint1.X + i * sign, orgPoint1.Y + (float)Math.Tan(angleRad) * i * sign)
                                               : new PointF(orgPoint1.X + (i / (float)Math.Tan(angleRad)) * sign, orgPoint1.Y + i * sign);

                    if (path1.IsVisible(nearestPoint1) == false)
                    {
                        nearestPoint1Found = true;
                    }
                }

                if (nearestPoint2Found == false)
                {
                    nearestPoint2 = isAdjacent ? new PointF(orgPoint2.X - i * sign, orgPoint2.Y - (float)Math.Tan(angleRad) * i * sign)
                                               : new PointF(orgPoint2.X - (i / (float)Math.Tan(angleRad)) * sign, orgPoint2.Y - i * sign);

                    if (path2.IsVisible(nearestPoint2) == false)
                    {
                        nearestPoint2Found = true;
                    }
                }

                if (nearestPoint1Found && nearestPoint2Found)
                    break;
            }
        }

        public enum IGArrowCapStyle
        {
            None,
            Rectangle,
            RoundedRectangle,
            Ellipse,
            Arrow,
            Diamond
        }

        public class IGLineProp
        {
            public Color Color;
            public DashStyle Dash;
            public float Width;
        }

        public class IGArrowProp
        {
            public PointF StartPoint;
            public PointF EndPoint;
            public IGArrowCapStyle StartCap;
            public IGArrowCapStyle EndCap;

            public float Lenght;
            public float Width;
            public float BackPuffyLenght;
            public Color FromColor;
            public Color ToColor = Color.Empty;
            public Color OutlineColor;
            public DashStyle OutlineDash;
            public float OutlineWidth;

            public IGLineProp LineProp;
        }

        private GraphicsPath CalculateArrowPath
        (
            IGArrowCapStyle arrowCap,
            PointF arrowPoint,
            PointF otherPoint,
            float length,
            float width,
            float backPuffyLenght)
        {
            if (arrowCap == IGArrowCapStyle.None)
                return null;

            float tmpW;
            GraphicsPath arrowPath = new GraphicsPath();

            switch (arrowCap)
            {
                case IGArrowCapStyle.Arrow :
                    double lineAngleRad = Math.Atan2(arrowPoint.Y - otherPoint.Y, arrowPoint.X - otherPoint.X);
                    double pointY = arrowPoint.Y - Math.Sin(lineAngleRad) * (length + backPuffyLenght);
                    double pointX = arrowPoint.X - Math.Cos(lineAngleRad) * (length + backPuffyLenght);

                    PointF arrowpPointFront = arrowPoint;
                    PointF arrowpPointBack = new PointF((float)pointX, (float)pointY);

                    double arrowCapAngleRad = Math.Atan2(width, length);
                    double tmpRad = Math.PI - arrowCapAngleRad + lineAngleRad;
                    double capLength = Math.Sqrt(Math.Pow(length, 2) + Math.Pow(width, 2));
                    pointY = arrowPoint.Y + Math.Sin(tmpRad) * capLength;
                    pointX = arrowPoint.X + Math.Cos(tmpRad) * capLength;
                    PointF arrowpPointLeft = new PointF((float)pointX, (float)pointY);

                    tmpRad = Math.PI + arrowCapAngleRad + lineAngleRad;
                    pointY = arrowPoint.Y + Math.Sin(tmpRad) * capLength;
                    pointX = arrowPoint.X + Math.Cos(tmpRad) * capLength;
                    PointF arrowpPointRight = new PointF((float)pointX, (float)pointY);

                    arrowPath.AddPolygon(new PointF[] { arrowpPointFront, arrowpPointLeft, arrowpPointBack, arrowpPointRight });

                    break;

                case IGArrowCapStyle.Ellipse:
                    arrowPath.AddEllipse(arrowPoint.X - (width / 2), arrowPoint.Y - (width / 2), width, width);
                    break;

                case IGArrowCapStyle.Rectangle:
                    arrowPath.AddRectangle(new RectangleF(arrowPoint.X - (width / 2), arrowPoint.Y - (width / 2), width, width));
                    break;

                case IGArrowCapStyle.RoundedRectangle:
                    tmpW = width / 1.5f;
                    float radius = width / 2;
                    arrowPath.AddArc(arrowPoint.X + tmpW - radius, arrowPoint.Y - tmpW, radius, radius, -90, 90);
                    arrowPath.AddArc(arrowPoint.X + tmpW - radius, arrowPoint.Y + tmpW - radius, radius, radius, 0, 90);
                    arrowPath.AddArc(arrowPoint.X - tmpW, arrowPoint.Y + tmpW - radius, radius, radius, 90, 90);
                    arrowPath.AddArc(arrowPoint.X - tmpW, arrowPoint.Y - tmpW, radius, radius, 180, 90);
                    break;

                case IGArrowCapStyle.Diamond:
                    tmpW = width / 1.5f;
                    arrowPath.AddPolygon(new PointF[]
                    {
                        new PointF(arrowPoint.X, arrowPoint.Y - tmpW),
                        new PointF(arrowPoint.X + tmpW, arrowPoint.Y),
                        new PointF(arrowPoint.X, arrowPoint.Y + tmpW),
                        new PointF(arrowPoint.X - tmpW, arrowPoint.Y)
                    });
                    break;
            }

            arrowPath.CloseFigure();
            return arrowPath;
        }

        private void PaintPath(Graphics gr, GraphicsPath path, IGArrowProp prop)
        {
            Pen pen = new Pen(prop.OutlineColor);
            pen.DashStyle = prop.OutlineDash;
            pen.Width = prop.OutlineWidth;

            Brush brush;
            if (prop.ToColor == Color.Empty)
            {
                brush = new SolidBrush(prop.FromColor);
            }
            else
            {
                brush = new LinearGradientBrush(path.GetBounds(), prop.FromColor, prop.ToColor, LinearGradientMode.ForwardDiagonal);
            }

            gr.FillPath(brush, path);
            gr.DrawPath(pen, path);
            pen.Dispose();
            brush.Dispose();
        }

        private void DrawArrow(Graphics gr, IGArrowProp prop)
        {
            GraphicsPath startPath = CalculateArrowPath
                (
                    prop.StartCap,
                    prop.StartPoint,
                    prop.EndPoint,
                    prop.Lenght,
                    prop.Width,
                    prop.BackPuffyLenght
                );
            GraphicsPath endPath = CalculateArrowPath
                (
                    prop.EndCap,
                    prop.EndPoint,
                    prop.StartPoint,
                    prop.Lenght,
                    prop.Width,
                    prop.BackPuffyLenght
                );

            Pen pen = new Pen(prop.LineProp.Color);
            pen.DashStyle = prop.LineProp.Dash;
            pen.Width = prop.LineProp.Width;

            gr.DrawLine(pen, prop.StartPoint, prop.EndPoint);
            pen.Dispose();

            if (startPath != null)
            {
                PaintPath(gr, startPath, prop);
                startPath.Dispose();
            }

            if (endPath != null)
            {
                PaintPath(gr, endPath, prop);
                endPath.Dispose();
            }

            //g.FillEllipse(Brushes.Green, arrowpPointBack.X - 2.5f, arrowpPointBack.Y - 2.5f, 5, 5);
            //g.FillEllipse(Brushes.Yellow, arrowpPointFront.X - 2.5f, arrowpPointFront.Y - 2.5f, 5, 5);
        }

        private void DrawArrow(Graphics g, PointF ArrowStart, PointF ArrowEnd, Color ArrowColor, int LineWidth, int ArrowMultiplier)
        {
            //create the pen
            Pen p = new Pen(ArrowColor, LineWidth);

            //draw the line
            g.DrawLine(p, ArrowStart, ArrowEnd);

            //determine the coords for the arrow point

            //tip of the arrow
            PointF arrowPoint = ArrowEnd;

            //determine arrow length
            double arrowLength = Math.Sqrt(Math.Pow(Math.Abs(ArrowStart.X - ArrowEnd.X), 2) +
                                           Math.Pow(Math.Abs(ArrowStart.Y - ArrowEnd.Y), 2));

            //determine arrow angle
            double arrowAngle = Math.Atan2(Math.Abs(ArrowStart.Y - ArrowEnd.Y), Math.Abs(ArrowStart.X - ArrowEnd.X));

            //get the x,y of the back of the point

            //to change from an arrow to a diamond, change the 3
            //in the next if/else blocks to 6

            double pointX, pointY;
            if (ArrowStart.X > ArrowEnd.X)
            {
                pointX = ArrowStart.X - (Math.Cos(arrowAngle) * (arrowLength - (3 * ArrowMultiplier)));
            }
            else
            {
                pointX = Math.Cos(arrowAngle) * (arrowLength - (3 * ArrowMultiplier)) + ArrowStart.X;
            }

            if (ArrowStart.Y > ArrowEnd.Y)
            {
                pointY = ArrowStart.Y - (Math.Sin(arrowAngle) * (arrowLength - (3 * ArrowMultiplier)));
            }
            else
            {
                pointY = (Math.Sin(arrowAngle) * (arrowLength - (3 * ArrowMultiplier))) + ArrowStart.Y;
            }

            PointF arrowPointBack = new PointF((float)pointX, (float)pointY);

            //get the secondary angle of the left tip
            double angleB = Math.Atan2((3 * ArrowMultiplier), (arrowLength - (3 * ArrowMultiplier)));

            double angleC = Math.PI * (90 - (arrowAngle * (180 / Math.PI)) - (angleB * (180 / Math.PI))) / 180;

            //get the secondary length
            double secondaryLength = (3 * ArrowMultiplier) / Math.Sin(angleB);

            if (ArrowStart.X > ArrowEnd.X)
            {
                pointX = ArrowStart.X - (Math.Sin(angleC) * secondaryLength);
            }
            else
            {
                pointX = (Math.Sin(angleC) * secondaryLength) + ArrowStart.X;
            }

            if (ArrowStart.Y > ArrowEnd.Y)
            {
                pointY = ArrowStart.Y - (Math.Cos(angleC) * secondaryLength);
            }
            else
            {
                pointY = (Math.Cos(angleC) * secondaryLength) + ArrowStart.Y;
            }

            //get the left point
            PointF arrowPointLeft = new PointF((float)pointX, (float)pointY);

            //move to the right point
            angleC = arrowAngle - angleB;

            if (ArrowStart.X > ArrowEnd.X)
            {
                pointX = ArrowStart.X - (Math.Cos(angleC) * secondaryLength);
            }
            else
            {
                pointX = (Math.Cos(angleC) * secondaryLength) + ArrowStart.X;
            }

            if (ArrowStart.Y > ArrowEnd.Y)
            {
                pointY = ArrowStart.Y - (Math.Sin(angleC) * secondaryLength);
            }
            else
            {
                pointY = (Math.Sin(angleC) * secondaryLength) + ArrowStart.Y;
            }

            PointF arrowPointRight = new PointF((float)pointX, (float)pointY);

            //create the point list
            PointF[] arrowPoints = new PointF[4];
            arrowPoints[0] = arrowPoint;
            arrowPoints[1] = arrowPointLeft;
            arrowPoints[2] = arrowPointBack;
            arrowPoints[3] = arrowPointRight;

            //draw the outline
            g.DrawPolygon(p, arrowPoints);

            //fill the polygon
            g.FillPolygon(new SolidBrush(ArrowColor), arrowPoints);

            //for (int i = 0; i < arrowPoints.Length; ++i)
            //g.FillEllipse(Brushes.Red, arrowPoints[i].X - 2.5f, arrowPoints[i].Y - 2.5f, 5, 5);

            //g.FillEllipse(Brushes.Red, arrowPoints[1].X - 2.5f, arrowPoints[1].Y - 2.5f, 5, 5);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;// AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.Clear(Color.LightGray);

            e.Graphics.DrawPath(Pens.Black, path1);
            e.Graphics.DrawPath(Pens.Black, path2);

            PointF nearestPoint1;
            PointF nearestPoint2;
            GetNearestPoints(org1, org2, out nearestPoint1, out nearestPoint2);

            IGArrowProp prop = new IGArrowProp()
            {
                StartPoint = nearestPoint1,
                EndPoint = nearestPoint2,
                StartCap = IGArrowCapStyle.RoundedRectangle,
                EndCap = IGArrowCapStyle.Arrow,
                Lenght = 20,
                Width = 10,
                BackPuffyLenght = -3,
                FromColor = Color.Blue,
                ToColor = Color.White,
                OutlineColor = Color.Red,
                OutlineDash = DashStyle.Solid,
                OutlineWidth = 1,

                LineProp = new IGLineProp() { Color = Color.Green, Dash = DashStyle.DashDot, Width = 2 }
            };

            DrawArrow(e.Graphics, prop);
        }


        public static PointF RotatePoint(PointF pointToRotate, PointF referencePoint, double angleInRadian)
        {
            //double angleInRadian = angleInDegree * (Math.PI / 180.0);
            double cosTheta = Math.Cos(angleInRadian);
            double sinTheta = Math.Sin(angleInRadian);

            return new PointF
            {
                X = (int)(cosTheta * (pointToRotate.X - referencePoint.X) - sinTheta * (pointToRotate.Y - referencePoint.Y) + referencePoint.X),
                Y = (int)(sinTheta * (pointToRotate.X - referencePoint.X) + cosTheta * (pointToRotate.Y - referencePoint.Y) + referencePoint.Y)
            };
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDX = e.X;
                mouseDY = e.Y;
            }

        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                button1.Left = button1.Left + (e.X - mouseDX);
                button1.Top = button1.Top + (e.Y - mouseDY);
            }
        }
    }
}