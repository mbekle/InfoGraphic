using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TestProject
{
    public partial class IGProcess : UserControl
    {
        private const int DefaultItemWidht = 50;
        private const int DefaultItemHeight = 30;
        private const int ItemTextLeftOrRightEmptyWidth = 15;
        private const int ItemSeperatorWidth = 5;

        public class IGProcessItem
        {
            public string Text { get; set; }
            public int Width { get; set; }
            public Font TextFont { get; set; }
            public Rectangle BoundedRect { get; set; }
            public Point Origin { get; set; }
            public GraphicsPath GrPath { get; set; }
            public Color BackColorFrom { get; set; }
            public Color BackColorTo { get; set; }

            public IGProcessItem(string text, int width, Font font, Color backColorFrom, Color backColorTo)
            {
                Text = text;
                TextFont = font;

                if (width > 0)
                {
                    Width = width;
                }
                else
                {
                    if(string.IsNullOrEmpty(text) || font == null)
                    {
                        Width = DefaultItemWidht;
                    }
                    else
                    {
                        Width = TextRenderer.MeasureText(text, font).Width + (ItemTextLeftOrRightEmptyWidth * 2);
                    }
                }

                BackColorFrom = (backColorFrom == Color.Empty ? Color.Blue : backColorFrom);
                BackColorTo = backColorTo;
            }
        }

        public enum FirstLastItemShapeType
        {
            Rectangle,
            RoundedRectangle,
            Round
        }

        private List<IGProcessItem> _items = new List<IGProcessItem>();
        private int _itemHeight = DefaultItemHeight;
        private bool _isInitializing = false;
        private FirstLastItemShapeType _firstLastItemShapeType = FirstLastItemShapeType.Round;
        
        public int ItemHeight
        {
            get { return _itemHeight; }
            set
            {
                _itemHeight = value;
                PrepareItems();
                Invalidate();
            }
        }

        public IGProcess()
        {
            _isInitializing = true;
            try
            {
                InitializeComponent();
            }
            finally
            {
                _isInitializing = false;
            }
        }

        public IGProcessItem AddItem(string text, int width, Font font, Color backColorFrom, Color backColorTo)
        {
            IGProcessItem item = new IGProcessItem(text, width, font, backColorFrom, backColorTo);
            _items.Add(item);
            return item;
        }

        public void AddItem(IGProcessItem[] itemArray)
        {
            _items.Clear();
            _items.AddRange(itemArray);
        }

        public void PrepareItems()
        {
            if (_items.Count == 0 || _isInitializing)
            {
                return;
            }

            int itemsTotalWidht = _items.Sum(ob => ob.Width);
            itemsTotalWidht += ((_items.Count - 1) * ItemSeperatorWidth);

            Point location = new Point((Width - itemsTotalWidht) / 2, (Height - ItemHeight) / 2);

            for (int i = 0; i < _items.Count; ++i)
            {
                IGProcessItem pItem = _items[i];
                pItem.BoundedRect = new Rectangle(location, new Size(pItem.Width, ItemHeight));
                pItem.Origin = new Point(pItem.BoundedRect.X + pItem.BoundedRect.Width / 2, pItem.BoundedRect.Y + pItem.BoundedRect.Height / 2);

                if (pItem.GrPath != null)
                {
                    pItem.GrPath.Dispose();
                }
                pItem.GrPath = new GraphicsPath();
                pItem.GrPath.AddRectangle(pItem.BoundedRect);

                location.X = location.X + pItem.Width + ItemSeperatorWidth;
            }
        }

        public void DrawProcessItem(Graphics gr, IGProcessItem item)
        {
            Brush brush;

            if (item.BackColorTo == Color.Empty)
            {
                brush = new SolidBrush(item.BackColorFrom);
            }
            else
            {
                brush = new LinearGradientBrush(item.BoundedRect, item.BackColorFrom, item.BackColorTo, LinearGradientMode.Horizontal);
            }

            gr.FillPath(brush, item.GrPath);


            //LinearGradientBrush gradientBrush;

            //if (isActive)
            //{
            //    gr.FillPath(new SolidBrush(Color.FromArgb(245, 124, 45)), _shapePath);
            //    gradientBrush = new LinearGradientBrush(_clientRectangle, Color.FromArgb(224, 237, 248), Color.FromArgb(94, 158, 219), LinearGradientMode.ForwardDiagonal);
            //}
            //else
            //{
            //    gr.FillEllipse(new SolidBrush(Color.DarkGray), _clientRectangle);
            //    gradientBrush = new LinearGradientBrush(_clientRectangle, Color.FromArgb(224, 237, 248), Color.Gray, LinearGradientMode.ForwardDiagonal);
            //}

            //Rectangle tmpRect = _clientRectangle;
            //tmpRect.Inflate(-3, -3);
            //gr.FillEllipse(gradientBrush, tmpRect);

            //StringFormat sf = new StringFormat();
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;
            //gr.DrawString(_text, TextFont, new SolidBrush(Color.Black), _clientRectangle, sf);

            //gr.FillRectangle(Brushes.Blue, BoundedRect);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_items.Count == 0)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.Clear(Color.LightGray);

            foreach (IGProcessItem item in _items)
            {
                DrawProcessItem(e.Graphics, item);
            }


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