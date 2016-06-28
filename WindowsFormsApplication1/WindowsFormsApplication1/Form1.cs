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
        PointF org2 ;
        
        public Form1()
        {
            InitializeComponent();
            InitializePaths(new PointF(200, 10), new PointF(20, 150));
        }

        private void InitializePaths(PointF location1, PointF location2)
        {
            RectangleF rect1 = new RectangleF(location1.X, location1.Y, 100, 100);
            org1 = new PointF(rect1.Left + rect1.Width / 2, rect1.Top + rect1.Height / 2);
            path1.Reset();
            path1.AddRectangle(rect1);
            path1.CloseFigure();

            RectangleF rect2 = new RectangleF(location2.X, location2.Y, 70, 70);
            org2 = new PointF(rect2.Left + rect2.Width / 2, rect2.Top + rect2.Height / 2);
            path2.Reset();
            path2.AddEllipse(rect2);
            path2.CloseFigure();
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

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (path1.IsVisible(e.Location))
            {
                if (path1In == false)
                {
                    path1In = true;

                    using (Graphics g = CreateGraphics())
                        g.DrawPath(Pens.Red, path1);
                }
            }
            else
            {
                if (path1In == true)
                {
                    path1In = false;

                    using (Graphics g = CreateGraphics())
                        g.DrawPath(Pens.Black, path1);
                }
            }

            if (path2.IsVisible(e.Location))
            {
                if (path2In == false)
                {
                    path2In = true;

                    using (Graphics g = CreateGraphics())
                        g.DrawPath(Pens.Red, path2);
                }
            }
            else
            {
                if (path2In == true)
                {
                    path2In = false;

                    using (Graphics g = CreateGraphics())
                        g.DrawPath(Pens.Black, path2);
                }
            }

            if (e.Button == MouseButtons.Left && path1In)
            {
                //InitializePaths(new PointF())

                //e.X - mouseDX

                Text = "mouseDX " + (e.X - mouseDX).ToString();
                mouseDX = e.X;
            }
            else
            {
                Text = "";
            }

            //Text = path1.IsVisible(e.Location).ToString();
            //Text = path2.IsVisible(e.Location).ToString();
        }

        private void GetNearestPoints(PointF orgPoint1, PointF orgPoint2, out PointF nearestPoint1, out PointF nearestPoint2)
        {
            bool nearestPoint1Found = false;
            bool nearestPoint2Found = false;
            float opposite = orgPoint2.Y - orgPoint1.Y;
            float adjacent = orgPoint2.X - orgPoint1.X;
            float angleRad = (float)Math.Atan(opposite / adjacent);
            float sign = (adjacent < 0 ? -1 : 1);
            
            nearestPoint1 = orgPoint1;
            nearestPoint2 = orgPoint2;
            adjacent = Math.Abs(adjacent);

            for (float i = 1; i < adjacent; ++i)
            {
                if (nearestPoint1Found == false)
                {
                    nearestPoint1 = new PointF(orgPoint1.X + i * sign, orgPoint1.Y + (float)Math.Tan(angleRad) * i * sign);

                    if (path1.IsVisible(nearestPoint1) == false)
                    {
                        nearestPoint1Found = true;
                    }
                }

                if (nearestPoint2Found == false)
                {
                    nearestPoint2 = new PointF(orgPoint2.X - i * sign, orgPoint2.Y - (float)Math.Tan(angleRad) * i * sign);

                    if (path2.IsVisible(nearestPoint2) == false)
                    {
                        nearestPoint2Found = true;
                    }
                }

                if (nearestPoint1Found && nearestPoint2Found)
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawPath(Pens.Black, path1);
            e.Graphics.DrawPath(Pens.Black, path2);

            PointF nearestPoint1;
            PointF nearestPoint2;
            GetNearestPoints(org1, org2, out nearestPoint1, out nearestPoint2);
            e.Graphics.DrawLine(Pens.Gray, nearestPoint1, nearestPoint2);
        }

        private int mouseDX;
        private int mouseDY;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && path1In)
            {
                mouseDX = e.X;
                mouseDY = e.Y;
            }
        }
    }
}
