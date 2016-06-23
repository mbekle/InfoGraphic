using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form1 : Form
    {
        private ProcessInfoGraphic.ProcessItem sube;
        private ProcessInfoGraphic.ProcessItem hazine;
        private ProcessInfoGraphic.ProcessItem tahm1;

        public Form1()
        {
            InitializeComponent();

            //processInfoGraphic1.BeginInitalize();
            sube = processInfoGraphic1.AddItem("şube", 60, 60, new Point(20, 70));
            hazine = processInfoGraphic1.AddItem("hazine", 50, 50, new Point(120, 15));
            tahm1 = processInfoGraphic1.AddItem("tahm1", 60, 60, new Point(220, 50));
            //processInfoGraphic1.EndInitialize();
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
    }
}