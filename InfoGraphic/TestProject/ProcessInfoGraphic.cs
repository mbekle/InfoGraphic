using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProject
{
    public partial class ProcessInfoGraphic : UserControl
    {
        public class Shape
        {
            private ProcessInfoGraphic _owner;
            private string _text;
            private Bitmap _bmp;
            private Point _location = Point.Empty;
            private int _width;
            private int _height;
            private Rectangle _clientRectangle;
            private Point _origin;

            public short Id;

            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }

            public Bitmap Bmp
            {
                get { return _bmp; }
                set
                {
                    _bmp = (Bitmap)value.Clone();
                }
            }

            public Point Location
            {
                get { return _location; }
                set
                {
                    _location = value;
                    SetClientRectangle();
                }
            }

            public int Width
            {
                get { return _width; }
                set
                {
                    _width = value;
                    SetClientRectangle();
                }
            }

            public int Height
            {
                get { return _height; }
                set
                {
                    _height = value;
                    SetClientRectangle();
                }
            }

            public Rectangle ClientRectangle { get { return _clientRectangle; } }

            public Shape(ProcessInfoGraphic owner)
            {
                _owner = owner;
                SetClientRectangle();
            }

            private void SetClientRectangle()
            {
                _clientRectangle = new Rectangle(_location, new Size(_width, _height));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] // designer ekranı yazıldığında vs designerı için gerekecek
        public List<Shape> Shapes { get; } = new List<Shape>();

        [Browsable(false)]
        public List<KeyValuePair<Shape, Shape>> ShapeRelation { get; } = new List<KeyValuePair<Shape, Shape>>();

        [Browsable(false)]
        public List<KeyValuePair<Shape, Shape>> ShapePath { get; } = new List<KeyValuePair<Shape, Shape>>();

        public void RotateAllToShape(Shape referenceShape, float angle)
        {
            if (Shapes.FindIndex(ob => ob == referenceShape) == -1)
            {
                MessageBox.Show("Nesne bulunamadı!");
                return;
            }

            foreach (Shape ob in Shapes)
            {
                ob.Location = GraphicHelper.RotatePoint(ob.Location, referenceShape.Location, angle);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;
            rect.Width = rect.Width - 1;
            rect.Height = rect.Height - 1;
            e.Graphics.DrawRectangle(Pens.Red, rect);

            if (Shapes.Count == 0) return;

            foreach (Shape ob in Shapes)
            {
                e.Graphics.DrawRectangle(Pens.Blue, ob.ClientRectangle);
            }
        }

        public ProcessInfoGraphic()
        {
            InitializeComponent();
        }
    }
}