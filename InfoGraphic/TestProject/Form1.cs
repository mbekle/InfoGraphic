﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form1 : Form
    {
        private ProcessInfoGraphic.ProcessItem sube;
        private ProcessInfoGraphic.ProcessItem hazine;
        private ProcessInfoGraphic.ProcessItem tahm1;

        private IGProcessUC.IGProcessItem _piAnaliz;
        private IGProcessUC.IGProcessItem _piDosyaHazirlik;

        public Form1()
        {
            InitializeComponent();

            //processInfoGraphic1.BeginInitalize();
            sube = processInfoGraphic1.AddItem("şube", 60, 60, new Point(20, 70));
            hazine = processInfoGraphic1.AddItem("hazine", 50, 50, new Point(120, 15));
            tahm1 = processInfoGraphic1.AddItem("tahm1", 60, 60, new Point(220, 50));
            //processInfoGraphic1.EndInitialize();



            Font font = new Font("Tahoma", 8f);
            Color fromColor = Color.FromArgb(224, 237, 248);
            Color toColor = Color.FromArgb(94, 158, 219);
            _piDosyaHazirlik = igProcess1UC.AddItem("Dosya Hazırlık", 0, font, Color.Black, StringAlignment.Center, fromColor, toColor, LinearGradientMode.Horizontal, Color.DarkGray, 1.0f, null);
            igProcess1UC.AddItem("Dosya Kabul", 0, font, Color.Red, StringAlignment.Center, fromColor, toColor, LinearGradientMode.BackwardDiagonal, Color.DarkGray, 1.0f, null);
            _piAnaliz = igProcess1UC.AddItem("Analiz", 0, font, Color.White, StringAlignment.Center, fromColor, toColor, LinearGradientMode.ForwardDiagonal, Color.DarkGray, 1.0f, null);
            igProcess1UC.AddItem("İstihbarat", 0, font, Color.Blue, StringAlignment.Center, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.DarkGray, 1.0f, null);
            igProcess1UC.AddItem("Analiz Kontrol", 0, font, Color.Blue, StringAlignment.Center, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.DarkGray, 1.0f, null);
            igProcess1UC.AddItem("Kesinleştirildi", 0, font, Color.Blue, StringAlignment.Center, fromColor, Color.Empty, LinearGradientMode.Vertical, Color.Empty, 1.0f, null);
            igProcess1UC.PrepareItems();
        }

        private void RotateButton_Click(object sender, EventArgs e)
        {
            processInfoGraphic1.RotateToItem(sube, 30);
            processInfoGraphic1.Invalidate();
        }

        private void CenterShapesButton_Click(object sender, EventArgs e)
        {
            processInfoGraphic1.CenterItems();
            processInfoGraphic1.Invalidate();
            CenterShapesButton.Tag = "1";
        }

        private void processInfoGraphic1_Resize(object sender, EventArgs e)
        {
            if ((string)CenterShapesButton.Tag == "1")
            {
                processInfoGraphic1.CenterItems();
                processInfoGraphic1.Invalidate();
            }
        }

        private void VerticalButton_Click(object sender, EventArgs e)
        {
            processInfoGraphic1.IsVertical = !processInfoGraphic1.IsVertical;
            VerticalButton.Text = processInfoGraphic1.IsVertical ? "Horizantal" : "Vertical";
            processInfoGraphic1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _piAnaliz.BackColorFrom = Color.White;
            _piAnaliz.BackColorTo = Color.DarkGreen;
            _piAnaliz.BackColorGradientMode = LinearGradientMode.ForwardDiagonal;
            _piAnaliz.LineColor = Color.Red;
            igProcess1UC.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //igProcess1.ItemAlignment = (StringAlignment)(((int)igProcess1.ItemAlignment + 1) % 3);
            _piDosyaHazirlik.TextAlignment = (StringAlignment)(((int)_piDosyaHazirlik.TextAlignment + 1) % 3);
            igProcess1UC.Refresh();
        }
    }
}