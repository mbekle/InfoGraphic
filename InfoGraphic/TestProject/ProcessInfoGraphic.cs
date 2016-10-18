using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TestProject
{
    public partial class ProcessInfoGraphic : UserControl
    {
        public enum ShapeType
        {
            Rectangle,
            RoundedRectangle,
            Ellipse,
            Triangle
        }

        public class ProcessItem
        {
            private ProcessInfoGraphic _owner;
            private string _text;
            private int _width;
            private int _height;
            private Point _location;
            private Point _origin; // calculated
            private Bitmap _bmp;
            private Rectangle _clientRectangle; // calculated
            private ShapeType _shape = ShapeType.Ellipse;
            private GraphicsPath _shapePath;

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
                    Prepare();
                }
            }

            public int Width
            {
                get { return _width; }
                set
                {
                    _width = value;
                    Prepare();
                }
            }

            public int Height
            {
                get { return _height; }
                set
                {
                    _height = value;
                    Prepare();
                }    
            }

            public Font TextFont { get; set; }

            public Rectangle ClientRectangle { get { return _clientRectangle; } }
            public Point Origin { get { return _origin; } }

            public ShapeType Shape
            {
                get { return _shape; }
                set
                {
                    _shape = value;
                    SetShapePath();
                }
            }

            public ProcessItem(ProcessInfoGraphic owner, string text, Point location, int width, int height)
            {
                _owner = owner;
                _text = text;
                _location = location;
                _width = width;
                _height = height;

                TextFont = new Font("Tahoma", 8f);

                Prepare();
            }

            private void Prepare()
            {
                _clientRectangle = new Rectangle(_location, new Size(_width, _height));
                _origin = new Point(_location.X + _width / 2, _location.Y + _height / 2);

                SetShapePath();
            }

            private void SetShapePath()
            {
                if (_shapePath != null)
                    _shapePath.Dispose();

                _shapePath = new GraphicsPath();

                switch (_shape)
                {
                    case ShapeType.Ellipse:
                        _shapePath.AddEllipse(_clientRectangle);
                        break;

                    case ShapeType.Rectangle:
                        _shapePath.AddRectangle(_clientRectangle);
                        break;

                    case ShapeType.RoundedRectangle:
                        break;

                    case ShapeType.Triangle:
                        break;
                }
            }

            public void Draw(Graphics gr, bool isActive)
            {
                LinearGradientBrush gradientBrush;

                if (isActive)
                {
                    gr.FillPath(new SolidBrush(Color.FromArgb(245, 124, 45)), _shapePath);
                    gradientBrush = new LinearGradientBrush(_clientRectangle, Color.FromArgb(224, 237, 248), Color.FromArgb(94, 158, 219), LinearGradientMode.ForwardDiagonal);
                }
                else
                {
                    gr.FillEllipse(new SolidBrush(Color.DarkGray), _clientRectangle);
                    gradientBrush = new LinearGradientBrush(_clientRectangle, Color.FromArgb(224, 237, 248), Color.Gray, LinearGradientMode.ForwardDiagonal);
                }

                Rectangle tmpRect = _clientRectangle;
                tmpRect.Inflate(-3, -3);
                gr.FillEllipse(gradientBrush, tmpRect);

                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                gr.DrawString(_text, TextFont, new SolidBrush(Color.Black), _clientRectangle, sf);
            }
        }

        public class Relation
        {
            public ProcessItem FromItem;
            public ProcessItem ToItem;
            public DashStyle LineDashSytle;
            public bool IsActive;
        }

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] // designer ekranı yazıldığında vs designerı için gerekecek
        //public List<ProcessItem> Shapes { get; } = new List<ProcessItem>();

        //private bool _isInInitializing = false;
        private List<ProcessItem> _items = new List<ProcessItem>();
        private List<Relation> _itemRelations = new List<Relation>();
        private List<Relation> _processPath = new List<Relation>();
        private bool _isVertical = false;

        public bool IsVertical
        {
            get { return _isVertical; }
            set
            {
                _isVertical = value;
                RotateToItem(null, (_isVertical ? 90 : -90));
            }
        }

        public ProcessInfoGraphic()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
        }

        public void ClearObjects()
        {
            _items.Clear();
            _itemRelations.Clear();
            _processPath.Clear();
        }

        //public void BeginInitalize()
        //{
        //    _isInInitializing = true;
        //}

        //public void EndInitialize()
        //{
        //    _isInInitializing = false;
        //}

        //private bool ControlInInitialize()
        //{
        //    if (_isInInitializing == false)
        //    {
        //        MessageBox.Show("Call BeginInitialize method first, later call EndInitialize method!");
        //        return false;
        //    }

        //    return true;
        //}

        public ProcessItem AddItem(string text, int width, int height, Point location)
        {
            //if (ControlInInitialize() == false) return null;

            ProcessItem item = new ProcessItem(this, text, location, width, height);
            _items.Add(item);
            return item;
        }

        public void AddItem(ProcessItem[] itemArray)
        {
            //if (ControlInInitialize() == false) return;

            _items.Clear();
            _items.AddRange(itemArray);
        }

        public void RotateToItem(ProcessItem referenceItem, float angle)
        {
            if (_items.Count == 0)
                return;

            if (referenceItem == null)
            {
                referenceItem = _items[0];
            }
            else
            {
                if (_items.FindIndex(ob => ob == referenceItem) == -1)
                {
                    MessageBox.Show("Nesne bulunamadı!");
                    return;
                }
            }

            foreach (ProcessItem ob in _items)
            {
                Point rotatedOrijin = GraphicHelper.RotatePoint(ob.Origin, referenceItem.Origin, angle);
                ob.Location = new Point(rotatedOrijin.X - ob.Width / 2, rotatedOrijin.Y - ob.Height / 2);
            }
        }

        public void CenterItems()
        {
            if (_items.Count == 0) return;

            int left = _items[0].Location.X;
            int right = _items[0].ClientRectangle.Right;
            int top = _items[0].Location.Y;
            int bottom = _items[0].ClientRectangle.Bottom;

            foreach (ProcessItem ob in _items)
            {
                if (ob.Location.X < left) left = ob.Location.X;
                if (ob.ClientRectangle.Right > right) right = ob.ClientRectangle.Right;
                if (ob.Location.Y < top) top = ob.Location.Y;
                if (ob.ClientRectangle.Bottom > bottom) bottom = ob.ClientRectangle.Bottom;
            }

            int deltaX = (Width - (right - left)) / 2 - left;
            int deltaY = (Height - (bottom - top)) / 2 - top;

            foreach (ProcessItem ob in _items)
            {
                ob.Location = new Point(ob.Location.X + deltaX, ob.Location.Y + deltaY);
            }
        }

        private void Demo()
        {
            ProcessItem item1;
            ProcessItem item2;
            ProcessItem item3;

            ClearObjects();

            //BeginInitalize();
            item1 = AddItem("Item 1", 30, 30, new Point(20, 50));
            item2 = AddItem("Item 2", 25, 25, new Point(70, 10));
            item3 = AddItem("Item 3", 30, 30, new Point(120, 50));
            //EndInitialize();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_items.Count == 0 /*|| _isInInitializing == true*/)
            {
                base.OnPaint(e);
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.Clear(Color.LightGray);

            bool a = false;
            foreach (ProcessItem ob in _items)
            {
                a = !a;
                ob.Draw(e.Graphics, a);

                //e.Graphics.DrawRectangle(Pens.Blue, ob.ClientRectangle);
            }

            base.OnPaint(e);

            //if (DesignMode)
            //{
            //    Demo();
            //}

            //if (DesignMode)
            //{
            //    e.Graphics.DrawString("Demo", new Font("Tahoma", 8), Brushes.Black, Point.Empty);
            //}
        }
    }
}