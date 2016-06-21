using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TestProject
{
    public partial class ProcessInfoGraphic : UserControl
    {
        public class Shape
        {
            private ProcessInfoGraphic _owner;
            private string _text;
            private int _width;
            private int _height;
            private Point _location;
            private Point _origin;
            private Bitmap _bmp;
            private Rectangle _clientRectangle;

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
                set { _location = value; Prepare(); }
            }

            public int Width
            {
                get { return _width; }
                set { _width = value; Prepare(); }
            }

            public int Height
            {
                get { return _height; }
                set { _height = value; Prepare(); }    
            }

            public Rectangle ClientRectangle { get { return _clientRectangle; } }
            public Point Origin { get { return _origin; } }

            public Shape(ProcessInfoGraphic owner, Point location, int width, int height)
            {
                _owner = owner;
                _location = location;
                _width = width;
                _height = height;
                Prepare();
            }

            private void Prepare()
            {
                _clientRectangle = new Rectangle(_location, new Size(_width, _height));
                _origin = new Point(_location.X + _width / 2, _location.Y + _height / 2);
            }
        }

        public class Relation
        {
            public Shape FromShape;
            public Shape ToShape;
            public DashStyle LineDashSytle;
            public bool IsActive;
        }

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] // designer ekranı yazıldığında vs designerı için gerekecek
        //public List<Shape> Shapes { get; } = new List<Shape>();

        public enum ShapeAlignmentStyle { None, Left, Right, Top, Bottom, Center }

        private bool _isInInitializing = false;
        private List<Shape> _shapes = new List<Shape>();
        private List<Relation> _shapeRelation = new List<Relation>();
        private List<Relation> _shapePath = new List<Relation>();
        private ShapeAlignmentStyle _shapeAlignment = ShapeAlignmentStyle.None;
        private Point _leftMostShapeLocation;

        public ShapeAlignmentStyle ShapeAlignment
        {
            get { return _shapeAlignment; }
            set
            {
                _shapeAlignment = value;
                AlignShapes(_shapeAlignment);
                Invalidate();
            }
        }

        public void BeginInitalize()
        {
            _isInInitializing = true;
        }

        public void EndInitialize()
        {
            _isInInitializing = false;
            _leftMostShapeLocation = GetLeftMostShapeLocation();
            Invalidate();
        }

        private Point GetLeftMostShapeLocation()
        {
            if (_shapes.Count == 0) return Point.Empty;

            Point minLocation = _shapes[0].Location;

            foreach (Shape ob in _shapes)
            {
                if (ob.Location.X < minLocation.X)
                {
                    minLocation = ob.Location;
                }
            }

            return minLocation;
        }

        private bool ControlInInitialize()
        {
            if (_isInInitializing == false)
            {
                MessageBox.Show("Call BeginInitialize method first, later call EndInitialize method!");
                return false;
            }

            return true;
        }

        public Shape AddShape(string text, int width, int height, Point location)
        {
            if (ControlInInitialize() == false) return null;

            Shape shape = new Shape(this, location, width, height);
            _shapes.Add(shape);
            return shape;
        }

        public void AddShape(Shape[] shapeArray)
        {
            if (ControlInInitialize() == false) return;

            _shapes.Clear();
            _shapes.AddRange(shapeArray);
        }

        public void RotateToShape(Shape referenceShape, float angle)
        {
            if (_shapes.FindIndex(ob => ob == referenceShape) == -1)
            {
                MessageBox.Show("Nesne bulunamadı!");
                return;
            }

            foreach (Shape ob in _shapes)
            {
                Point rotatedOrijin = GraphicHelper.RotatePoint(ob.Origin, referenceShape.Origin, angle);
                ob.Location = new Point(rotatedOrijin.X - ob.Width / 2, rotatedOrijin.Y - ob.Height / 2);
            }

            _leftMostShapeLocation = GetLeftMostShapeLocation();

            Invalidate();
        }

        private void AlignShapes(ShapeAlignmentStyle alignment)
        {
            if (_shapes.Count == 0) return;

            Point tmpLocation = _shapes[0].Location;

            foreach (Shape ob in _shapes)
            {
                bool exc = false;

                switch (alignment)
                {
                    case ShapeAlignmentStyle.Left:
                        if (ob.Location.X < tmpLocation.X) exc = true;
                        break;

                    case ShapeAlignmentStyle.Right:
                        if (ob.Location.X > tmpLocation.X) exc = true;
                        break;

                    case ShapeAlignmentStyle.Top:
                        if (ob.Location.Y < tmpLocation.Y) exc = true;
                        break;

                    case ShapeAlignmentStyle.Bottom:
                        if (ob.Location.Y > tmpLocation.Y) exc = true;
                        break;
                }

                if (exc)
                {
                    tmpLocation = ob.Location;
                }

            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;
            rect.Width = rect.Width - 1;
            rect.Height = rect.Height - 1;
            e.Graphics.DrawRectangle(Pens.Red, rect);

            if (_shapes.Count == 0) return;

            foreach (Shape ob in _shapes)
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