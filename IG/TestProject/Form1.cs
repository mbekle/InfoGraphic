using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form1 : Form
    {
        private IGProcess.IGProcessItem _piAnaliz;
        private IGProcess.IGProcessItem _piDosyaHazirlik;

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = trackBar1.Value.ToString();

            Font font = new Font("Tahoma", 8f);
            Color fromColor = Color.FromArgb(224, 237, 248);
            Color toColor = Color.FromArgb(94, 158, 219);
            _piDosyaHazirlik = igProcess1.AddItem("Dosya Hazırlık", 0, font, Color.Black, StringAlignment.Center, 0, fromColor, toColor, LinearGradientMode.Horizontal, Color.DarkGray, 1.0f, null);
            igProcess1.AddItem("Dosya Kabul", 0, font, Color.Red, StringAlignment.Center, 0, fromColor, toColor, LinearGradientMode.BackwardDiagonal, Color.DarkGray, 1.0f, null);
            _piAnaliz = igProcess1.AddItem("Analiz", 0, font, Color.White, StringAlignment.Center, 0, fromColor, toColor, LinearGradientMode.ForwardDiagonal, Color.DarkGray, 1.0f, null);
            igProcess1.AddItem("İstihbarat", 0, font, Color.Blue, StringAlignment.Center, 0, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.DarkGray, 1.0f, null);
            igProcess1.AddItem("Analiz Kontrol", 0, font, Color.Blue, StringAlignment.Center, 0, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.DarkGray, 1.0f, null);
            igProcess1.AddItem("Kesinleştirildi", 0, font, Color.Blue, StringAlignment.Center, 0, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.Empty, 1.0f, null);
            igProcess1.PrepareItems();


            Font font2 = new Font("Tahoma", 8f, FontStyle.Bold);
            Color fromColor2 = Color.FromArgb(91, 155, 213);
            Color toColor2 = Color.FromArgb(91, 200, 255);
            igProcess2.AddItem("Dosya Hazırlık", 0, font2, Color.White, StringAlignment.Center, 0,  fromColor2, toColor2, LinearGradientMode.Horizontal, Color.DarkGray, 1.0f, null);
            igProcess2.AddItem("Dosya Kabul", 0, font2, Color.White, StringAlignment.Center, 0,  fromColor2, toColor2, LinearGradientMode.BackwardDiagonal, Color.DarkGray, 1.0f, null);
            igProcess2.AddItem("Analiz", 0, font2, Color.White, StringAlignment.Center, 0,  fromColor2, toColor2, LinearGradientMode.ForwardDiagonal, Color.DarkGray, 1.0f, null);
            igProcess2.AddItem("İstihbarat", 0, font2, Color.White, StringAlignment.Center, 0,  fromColor2, toColor2, LinearGradientMode.Vertical, Color.DarkGray, 1.0f, null);
            igProcess2.AddItem("Analiz Kontrol", 0, font2, Color.White, StringAlignment.Center, 0,  fromColor2, toColor2, LinearGradientMode.Vertical, Color.DarkGray, 1.0f, null);
            igProcess2.AddItem("Kesinleştirildi", 0, font2, Color.White, StringAlignment.Center, 0,  fromColor2, toColor2, LinearGradientMode.Vertical, Color.Black, 1.0f, null);
            igProcess2.PrepareItems();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _piAnaliz.BackColorFrom = Color.White;
            _piAnaliz.BackColorTo = Color.DarkGreen;
            _piAnaliz.BackColorGradientMode = LinearGradientMode.ForwardDiagonal;
            _piAnaliz.LineColor = Color.Red;
            _piAnaliz.Text = "Analiz yapıldı";
            _piAnaliz.Refresh();
            //igProcess1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _piDosyaHazirlik.TextAlignment = (StringAlignment)(((int)_piDosyaHazirlik.TextAlignment + 1) % 3);
            _piDosyaHazirlik.Refresh();
            //igProcess1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            igProcess1.ItemAlignment = (StringAlignment)(((int)igProcess1.ItemAlignment + 1) % 3);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int track;
            if (int.TryParse(textBox1.Text, out track))
            {
                trackBar1.Value = track;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();

            foreach (IGProcess.IGProcessItem item in igProcess2.Items)
            {
                item.TextAngle = trackBar1.Value;
            }

            igProcess2.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            igProcess2.Items[0].TextAlignment = (StringAlignment)(((int)igProcess2.Items[0].TextAlignment + 1) % 3);
            igProcess2.Items[0].Refresh();
        }
    }
}