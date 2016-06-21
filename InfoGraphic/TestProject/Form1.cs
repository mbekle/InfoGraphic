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
        private ProcessInfoGraphic.Shape sube;
        private ProcessInfoGraphic.Shape hazine;
        private ProcessInfoGraphic.Shape tahm1;

        public Form1()
        {
            InitializeComponent();

            processInfoGraphic1.BeginInitalize();
            sube = processInfoGraphic1.AddShape("şube", 30, 30, new Point(20, 50));
            hazine = processInfoGraphic1.AddShape("hazine", 25, 25, new Point(70, 10));
            tahm1 = processInfoGraphic1.AddShape("tahm1", 30, 30, new Point(120, 50));
            processInfoGraphic1.EndInitialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            processInfoGraphic1.RotateToShape(sube, 30);
        }
    }
}