using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoGraphic
{
    public class InfoGraphicObject
    {
        public byte Id { get; set; }
        public Rectangle MainRect { get; set; }
        public Rectangle CircleRect { get; set; }
        public Point CircleOrigin { get; set; }
        public string Caption { get; set; }
        public bool IsChild { get; set; }
        public Font CaptionFont { get; set; }

        public InfoGraphicObject()
        {
            CaptionFont = new Font("Tahoma", 8.0f);
        }

        public void DrawObject(Graphics gr, bool foundInGraphicPath)
        {
            //gr.DrawRectangle(Pens.Blue, MainRect);
            //gr.DrawRectangle(Pens.Red, CircleRect);
            //return;

            LinearGradientBrush blueGradientBrush;

            if (foundInGraphicPath)
            {
                blueGradientBrush = new LinearGradientBrush(CircleRect, Color.FromArgb(224, 237, 248), Color.FromArgb(94, 158, 219), LinearGradientMode.ForwardDiagonal);
            }
            else
            {
                blueGradientBrush = new LinearGradientBrush(CircleRect, Color.FromArgb(224, 237, 248), Color.Gray, LinearGradientMode.ForwardDiagonal);
            }

            //LinearGradientBrush orangeGradientBrush = new LinearGradientBrush(circleRect, Color.FromArgb(224, 237, 248), Color.FromArgb(245, 124, 45), LinearGradientMode.Vertical);
            /*Blend blend1 = new Blend(9);
            blend1.Factors = new float[]{0.0F, 0.2F, 0.5F, 0.7F, 1.0F, 0.7F, 0.5F, 0.2F, 0.0F};
            blend1.Positions = new float[]{0.0F, 0.1F, 0.3F, 0.4F, 0.5F, 0.6F, 0.7F, 0.8F, 1.0F};
            gradientBrush.Blend = blend1;*/

            Rectangle circleRect = CircleRect;

            if (foundInGraphicPath)
            {
                gr.FillEllipse(new SolidBrush(Color.FromArgb(245, 124, 45)), circleRect);
            }
            else
            {
                gr.FillEllipse(new SolidBrush(Color.DarkGray), circleRect);
            }

            circleRect.Inflate(-3, -3);
            gr.FillEllipse(blueGradientBrush, circleRect);
            circleRect.Inflate(3, 3);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            gr.DrawString(Caption, CaptionFont, new SolidBrush(Color.Black), CircleRect, sf);
        }
    }
}