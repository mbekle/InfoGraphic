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

            Font font = new Font("Tahoma", 8f);
            Color fromColor = Color.FromArgb(224, 237, 248);
            Color toColor = Color.FromArgb(94, 158, 219);
            _piDosyaHazirlik = igProcess1.AddItem("Dosya Hazırlık", 0, font, Color.Black, StringAlignment.Center, fromColor, toColor, LinearGradientMode.Horizontal, Color.DarkGray, 1.0f, null);
            igProcess1.AddItem("Dosya Kabul", 0, font, Color.Red, StringAlignment.Center, fromColor, toColor, LinearGradientMode.BackwardDiagonal, Color.DarkGray, 1.0f, null);
            _piAnaliz = igProcess1.AddItem("Analiz", 0, font, Color.White, StringAlignment.Center, fromColor, toColor, LinearGradientMode.ForwardDiagonal, Color.DarkGray, 1.0f, null);
            igProcess1.AddItem("İstihbarat", 0, font, Color.Blue, StringAlignment.Center, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.DarkGray, 1.0f, null);
            igProcess1.AddItem("Analiz Kontrol", 0, font, Color.Blue, StringAlignment.Center, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.DarkGray, 1.0f, null);
            igProcess1.AddItem("Kesinleştirildi", 0, font, Color.Blue, StringAlignment.Center, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.Empty, 1.0f, null);
            igProcess1.PrepareItems();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _piAnaliz.BackColorFrom = Color.White;
            _piAnaliz.BackColorTo = Color.DarkGreen;
            _piAnaliz.BackColorGradientMode = LinearGradientMode.ForwardDiagonal;
            _piAnaliz.LineColor = Color.Red;
            igProcess1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _piDosyaHazirlik.TextAlignment = (StringAlignment)(((int)_piDosyaHazirlik.TextAlignment + 1) % 3);
            igProcess1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            igProcess1.ItemAlignment = (StringAlignment)(((int)igProcess1.ItemAlignment + 1) % 3);
        }
    }
}