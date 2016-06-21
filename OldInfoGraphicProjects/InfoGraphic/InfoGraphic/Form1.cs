using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoGraphic
{
    public partial class Form1 : Form
    {
        private InfoGraphic infoGrH = null;
        private InfoGraphicObject subeH;
        private InfoGraphicObject hazineH;
        private InfoGraphicObject tahm1H;
        private InfoGraphicObject pazarlamaH;
        private InfoGraphicObject tahsisH;
        private InfoGraphicObject kop1H;
        private InfoGraphicObject kop2H;
        private InfoGraphicObject tahm2H;

        private CircleInfoGraphic circleInfoGr = null;

        public Form1()
        {
            InitializeComponent();
            InitializeInfoGraphicH();
            InitializeCircleInfoGraphic();
        }

        private void InitializeInfoGraphicH()
        {
            infoGrH = new InfoGraphic(pictureBox1);

            subeH = infoGrH.AddGraphicObject("Şube", false);
            hazineH = infoGrH.AddGraphicObject("Hazine Müd.", true);
            tahm1H = infoGrH.AddGraphicObject("Tahsilatlar Müd.1", false);
            pazarlamaH = infoGrH.AddGraphicObject("Pazarlama Görüşü", true);
            tahsisH = infoGrH.AddGraphicObject("Tahsis Görüşü", true);
            kop1H = infoGrH.AddGraphicObject("Kredi Opr.1", false);
            kop2H = infoGrH.AddGraphicObject("Kredi Opr.2", false);
            tahm2H = infoGrH.AddGraphicObject("Tahsilatlar Müd.2", false);

            infoGrH.AddGraphicRelation(subeH, tahm1H);
            infoGrH.AddGraphicRelation(tahm1H, kop1H);
            infoGrH.AddGraphicRelation(kop1H, kop2H);
            infoGrH.AddGraphicRelation(kop2H, tahm2H);

            infoGrH.AddGraphicRelation(subeH, hazineH);
            infoGrH.AddGraphicRelation(hazineH, tahm1H);
            infoGrH.AddGraphicRelation(tahm1H, pazarlamaH);
            infoGrH.AddGraphicRelation(tahm1H, tahsisH);

            infoGrH.InitializeInfoGraphicObject();
        }

        private void InitializeCircleInfoGraphic()
        {
            circleInfoGr = new CircleInfoGraphic(pictureBox2);

            circleInfoGr.AddGraphicObject("Şube (25sn)", 25);
            circleInfoGr.AddGraphicObject("Hazine Müd. (20sn)", 20);
            circleInfoGr.AddGraphicObject("Tahsilatlar Müd.1 (30sn)", 30);
            circleInfoGr.AddGraphicObject("Pazarlama Görüşü (25sn)", 25);
            circleInfoGr.AddGraphicObject("Tahsis Görüşü (55sn)", 55);
            circleInfoGr.AddGraphicObject("Kredi Opr.1 (35sn)", 35);
            circleInfoGr.AddGraphicObject("Kredi Opr.2 (15sn)", 15);
            circleInfoGr.AddGraphicObject("Tahsilatlar Müd.2 (70sn)", 70);

            circleInfoGr.InitializeInfoGraphicObject();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            infoGrH.DrawInfoGraphic(e.Graphics);
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (infoGrH != null)
            {
                //infoGrH.LeftIndent = (pictureBox1.Width - 736) / 2;
                //infoGrH.RightIndent = infoGrH.LeftIndent;
                infoGrH.InitializeInfoGraphicObject();
            }
            pictureBox1.Refresh();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            infoGrH.Dispose();
        }

        private void full_Click(object sender, EventArgs e)
        {
            infoGrH.AddGraphicPath(tahm2H, subeH, hazineH, tahm1H, tahsisH, tahm1H, pazarlamaH, tahm1H, kop1H, kop2H, tahm2H);
            pictureBox1.Refresh();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            infoGrH.AddGraphicPath(subeH, subeH);
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            infoGrH.AddGraphicPath(hazineH, subeH, hazineH);
            pictureBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            infoGrH.AddGraphicPath(tahm1H, subeH, tahm1H, pazarlamaH);
            pictureBox1.Refresh();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            InfoGraphicObject[] obj = { subeH, hazineH, tahm1H, tahsisH, tahm1H, pazarlamaH, tahm1H, kop1H, kop2H, tahm2H };

            for (int i = 0; i < obj.Length; ++i)
            {
                List<InfoGraphicObject> tmp = new List<InfoGraphicObject>();
                for (int j = 0; j <= i; ++j)
                {
                    tmp.Add(obj[j]);
                }

                infoGrH.AddGraphicPath(tmp[tmp.Count - 1], tmp.ToArray());
                pictureBox1.Refresh();
                Thread.Sleep(3000);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InfoGraphicObject[] obj = { subeH, tahm1H, tahsisH, tahm1H, kop1H, kop2H, tahm2H };

            for (int i = 0; i < obj.Length; ++i)
            {
                List<InfoGraphicObject> tmp = new List<InfoGraphicObject>();
                for (int j = 0; j <= i; ++j)
                {
                    tmp.Add(obj[j]);
                }

                infoGrH.AddGraphicPath(tmp[tmp.Count - 1], tmp.ToArray());
                pictureBox1.Refresh();
                Thread.Sleep(3000);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (infoGrH.AnimationMethod == InfoGraphic.AnimationMethods.Default)
            {
                infoGrH.AnimationMethod = InfoGraphic.AnimationMethods.Nested;
                button6.Text = "current object basic method";
            }
            else
            {
                infoGrH.AnimationMethod = InfoGraphic.AnimationMethods.Default;
                button6.Text = "current object nested method";
            }

            pictureBox1.Refresh();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (infoGrH.IsVertical)
            {
                button8.Text = "Vertical";
                infoGrH.IsVertical = false;
                pictureBox1.Dock = DockStyle.Top;
                pictureBox1.Height = 200;
            }
            else
            {
                button8.Text = "Horizantal";
                infoGrH.IsVertical = true;
                pictureBox1.Dock = DockStyle.Left;
                pictureBox1.Width = 200;
            }

            infoGrH.InitializeInfoGraphicObject();
            pictureBox1.Refresh();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            circleInfoGr.DrawInfoGraphic(e.Graphics);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (circleInfoGr.SpaceBetweenRingParts == 0)
            {
                button7.Text = "halka arası boşluk yok";
                circleInfoGr.SpaceBetweenRingParts = 3;
            }
            else
            {
                button7.Text = "halka arası boşluk var";
                circleInfoGr.SpaceBetweenRingParts = 0;
            }

            pictureBox2.Invalidate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (circleInfoGr.UseGradient)
            {
                button9.Text = "Gradient";
                circleInfoGr.UseGradient = false;
            }
            else
            {
                button9.Text = "Solid";
                circleInfoGr.UseGradient = true;
            }

            pictureBox2.Invalidate();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (circleInfoGr.DrawingMethod == CircleInfoGraphic.DrawingMethodEnum.Arc)
            {
                button10.Text = "Arc Method";
                circleInfoGr.DrawingMethod = CircleInfoGraphic.DrawingMethodEnum.Pie;
            }
            else
            {
                button10.Text = "Pie Method";
                circleInfoGr.DrawingMethod = CircleInfoGraphic.DrawingMethodEnum.Arc;
            }

            pictureBox2.Invalidate();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            circleInfoGr.CircleCaption = textBox1.Text;
            pictureBox2.Invalidate();
        }
    }
}