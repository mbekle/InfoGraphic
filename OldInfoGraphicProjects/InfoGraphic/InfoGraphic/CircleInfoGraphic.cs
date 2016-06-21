using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace InfoGraphic
{
    public class CircleInfoGraphic
    {
        private const int _circleWidth = 40;
        private const float _circleStartAngle = -90F;

        private class CircleInfoGraphicObject
        {
            public string Caption;
            public double Value;
            public float StartAngle;
            public float SweepAngle;
            public Point Origin;
            public Point OutPointOne;
            public Point OutPointTwo;
            public Rectangle CaptionRect;
            public bool IsEast;
        }

        public enum DrawingMethodEnum { Arc, Pie };

        private Control _owner;
        private List<CircleInfoGraphicObject> _graphicObject;
        private Point _refPoint;
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle OuterRect { get; set; }
        public Rectangle InnerRect { get; set; }
        public Point Origin { get; set; }
        public Font TextFont { get; set; }
        public int SpaceBetweenRingParts { get; set; }
        public bool UseGradient { get; set; }
        public string CircleCaption { get; set; }
        public DrawingMethodEnum DrawingMethod { get; set; }

        public CircleInfoGraphic(Control owner)
        {
            _owner = owner;
            _graphicObject = new List<CircleInfoGraphicObject>();
            TextFont = new Font("Tahoma", 8.0f);
            SpaceBetweenRingParts = 3;
            UseGradient = false;
            CircleCaption = string.Empty;
            DrawingMethod = DrawingMethodEnum.Arc;
        }
        public void AddGraphicObject(string caption, double value)
        {
            _graphicObject.Add(new CircleInfoGraphicObject { Caption = caption, Value = value });
        }

        private Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegree)
        {
            double angleInRadian = angleInDegree * (Math.PI / 180.0);
            double cosTheta = Math.Cos(angleInRadian);
            double sinTheta = Math.Sin(angleInRadian);

            return new Point
            {
                X = (int)(cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y = (int)(sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        public void InitializeInfoGraphicObject()
        {
            Width = _owner.Width;
            Height = _owner.Height;

            int empty = 70;
            int minHalfSize = (Math.Min(Width, Height) - empty) / 2;
            int radius = (3 * (Width / 10)) > minHalfSize ? minHalfSize : (3 * (Width / 10));

            Origin = new Point(Width / 2, Height / 2);
            OuterRect = new Rectangle(Origin.X - radius, Origin.Y - radius, 2 * radius, 2 * radius);
            Rectangle tmpR = OuterRect;
            tmpR.Inflate(-_circleWidth, -_circleWidth);
            InnerRect = tmpR;

            _refPoint = new Point(Origin.X, Origin.Y - InnerRect.Height / 2 - _circleWidth / 2);

            double sumValue = _graphicObject.Sum(ob => ob.Value);
            float startAngle = _circleStartAngle;
            float sweepAngle;
            float rotateAngle;
            int lineCaptionSpace = 5;
            Point outP = new Point(_refPoint.X, _refPoint.Y - _circleWidth / 2 - empty / 3);

            foreach (CircleInfoGraphicObject ob in _graphicObject)
            {
                sweepAngle = (float)Math.Round((ob.Value / sumValue) * 360.0, 2);

                ob.StartAngle = startAngle;
                ob.SweepAngle = sweepAngle;

                startAngle += sweepAngle;

                rotateAngle = ob.StartAngle + (-_circleStartAngle) + ob.SweepAngle / 2;
                ob.Origin = RotatePoint(_refPoint, Origin, rotateAngle);
                ob.IsEast = (ob.Origin.X > Origin.X);
                ob.OutPointOne = RotatePoint(outP, Origin, rotateAngle);
                ob.OutPointTwo = new Point(Origin.X + (ob.IsEast ? 1 : -1) * (radius + 40), ob.OutPointOne.Y);
                ob.CaptionRect = new Rectangle((ob.IsEast ? ob.OutPointTwo.X + lineCaptionSpace : lineCaptionSpace),
                                               ob.OutPointTwo.Y - empty / 3,
                                               (ob.IsEast ? (Width - ob.OutPointTwo.X) : ob.OutPointTwo.X) - 2 * lineCaptionSpace,
                                               2 * empty / 3);
            }
        }

        public void DrawInfoGraphic(Graphics gr)
        {
            if (_graphicObject.Count == 0)
            {
                return;
            }

            //gr.DrawRectangle(Pens.Black, new Rectangle(1, 1, Width-2, Height-2));
            //gr.DrawRectangle(Pens.Red, OuterRect);
            //gr.DrawRectangle(Pens.Blue, InnerRect);


            Color[] colors =
            {
                Color.FromArgb(245, 126, 47),
                Color.FromArgb(90, 157, 219),
                Color.FromArgb(255, 198, 0),
                Color.FromArgb(165, 165, 165),
                Color.LightPink,
                Color.LightBlue,
                Color.Yellow,
                Color.Tomato,
                Color.Teal
            };

            using (Bitmap bmp = new Bitmap(Width, Height))
            using (Graphics bmpGraph = Graphics.FromImage(bmp))
            {
                bmpGraph.SmoothingMode = SmoothingMode.AntiAlias;
                bmpGraph.PixelOffsetMode = PixelOffsetMode.HighQuality;
                bmpGraph.Clear(Color.FloralWhite);

                for (int i = 0; i < _graphicObject.Count; ++i)
                {
                    CircleInfoGraphicObject ob = _graphicObject[i];

                    #region halka gösterge çizgilerini çiz
                    bmpGraph.DrawLines(new Pen(colors[i]), new Point[] { ob.Origin, ob.OutPointOne, ob.OutPointTwo });
                    bmpGraph.FillEllipse(Brushes.Gray, ob.OutPointTwo.X - 3, ob.OutPointTwo.Y - 3, 6, 6);
                    bmpGraph.FillEllipse(new SolidBrush(colors[i]), ob.OutPointTwo.X - 2, ob.OutPointTwo.Y - 2, 4, 4);
                    #endregion


                    #region halka parçalarını çiz
                    Brush brush;
                    if (UseGradient)
                    {
                        brush = new LinearGradientBrush(OuterRect, colors[i], Color.White, LinearGradientMode.Vertical);
                    }
                    else
                    {
                        brush = new SolidBrush(colors[i]);
                    }

                    if (DrawingMethod == DrawingMethodEnum.Arc)
                    {
                        using (GraphicsPath grPath = new GraphicsPath())
                        {
                            grPath.Reset();
                            grPath.AddArc(OuterRect, ob.StartAngle, ob.SweepAngle - SpaceBetweenRingParts);
                            grPath.AddArc(InnerRect, ob.StartAngle + ob.SweepAngle - SpaceBetweenRingParts, -(ob.SweepAngle - SpaceBetweenRingParts));
                            grPath.CloseFigure();

                            bmpGraph.FillPath(brush, grPath);
                            bmpGraph.DrawPath((UseGradient ? Pens.Gray : Pens.LightGray), grPath);
                        }
                    }
                    else // pie drawing method
                    {
                        bmpGraph.FillPie(brush, OuterRect, ob.StartAngle, ob.SweepAngle - SpaceBetweenRingParts);
                        bmpGraph.DrawPie((UseGradient ? Pens.Gray : Pens.LightGray), OuterRect, ob.StartAngle, ob.SweepAngle - SpaceBetweenRingParts);
                    }
                    #endregion

                    #region yüzde oranlarını ve halka textlerini çiz
                    bmpGraph.DrawString("%" + ((int)(ob.SweepAngle / 3.6f)).ToString(), TextFont, new SolidBrush(Color.Black), new Point(ob.Origin.X - 10, ob.Origin.Y - 5));

                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = (ob.IsEast ? StringAlignment.Near : StringAlignment.Far);

                    bmpGraph.DrawString(ob.Caption, TextFont, new SolidBrush(Color.Black), ob.CaptionRect, sf);
                    #endregion
                }

                if (CircleCaption != string.Empty)
                {
                    Font f = new Font("Arial Black", 12.0f, FontStyle.Bold);
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    LinearGradientBrush gradientBrush = new LinearGradientBrush(InnerRect, Color.LightGreen, Color.Black, LinearGradientMode.ForwardDiagonal);
                    bmpGraph.DrawString(CircleCaption, f, gradientBrush, InnerRect, sf);
                }


                

                gr.DrawImage(bmp, Point.Empty);
            }
        }



    }
}