using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;

namespace TestProject
{
    public partial class IGProcessUC : UserControl
    {
        private const int DefaultItemWidht = 50;
        private const int DefaultItemHeight = 35;
        private const int ItemTextLeftOrRightEmptyWidth = 50;
        private const int DefaultItemSeperatorWidth = -17;
        private const int DefaultItemTriangleWidth = 20;
        private const int DefaultItemRoundWidth = 15;

        public class IGProcessItem
        {
            public string Text { get; set; }
            public int Width { get; set; }
            public Font TextFont { get; set; }
            public Color TextColor { get; set; }
            public StringAlignment TextAlignment { get; set; }
            public RectangleF BoundRect { get; set; }
            public PointF Origin { get; set; }
            public GraphicsPath GrPath { get; set; }
            public Color BackColorFrom { get; set; }
            public Color BackColorTo { get; set; }
            public LinearGradientMode BackColorGradientMode { get; set; }
            public Color LineColor { get; set; }
            public float LineWidth { get; set; }
            public Image Icon { get; set; }

            public IGProcessItem(
                string text,
                int width,
                Font textFont,
                Color textColor,
                StringAlignment textAlignment,
                Color backColorFrom,
                Color backColorTo,
                LinearGradientMode backColorGradientMode,
                Color lineColor,
                float lineWidth,
                Image icon)
            {
                Text = text;
                TextFont = textFont;
                TextColor = textColor;
                TextAlignment = textAlignment;

                if (width > 0)
                {
                    Width = width;
                }
                else
                {
                    if (string.IsNullOrEmpty(text) || textFont == null)
                    {
                        Width = DefaultItemWidht;
                    }
                    else
                    {
                        Width = TextRenderer.MeasureText(text, textFont).Width + (ItemTextLeftOrRightEmptyWidth * 2);
                    }
                }

                BackColorFrom = (backColorFrom == Color.Empty ? Color.Blue : backColorFrom);
                BackColorTo = backColorTo;
                BackColorGradientMode = backColorGradientMode;
                LineColor = lineColor;
                LineWidth = lineWidth;
                Icon = icon;
            }
        }

        private List<IGProcessItem> _items = new List<IGProcessItem>();

        private short _itemHeight = DefaultItemHeight;
        private short _itemSeperatorWidth = DefaultItemSeperatorWidth;
        private Color _backColorTo = Color.Empty;
        private LinearGradientMode _backColorGradientMode = LinearGradientMode.Horizontal;
        private ItemRoundingShapeType _itemRoundingShape = ItemRoundingShapeType.Circular;
        private short _itemTriangleWidth = DefaultItemTriangleWidth;
        private short _itemRoundWidth = DefaultItemRoundWidth;
        private StringAlignment _itemAlignment = StringAlignment.Center;

        public short ItemHeight
        {
            get { return _itemHeight; }
            set
            {
                _itemHeight = value;
                PrepareItems();
                Invalidate();
            }
        }
        public short ItemSeperatorWidth
        {
            get { return _itemSeperatorWidth; }
            set
            {
                _itemSeperatorWidth = value;
                PrepareItems();
                Invalidate();
            }
        }

        public ItemRoundingShapeType ItemRoundingShape
        {
            get { return _itemRoundingShape; }
            set
            {
                _itemRoundingShape = value;
                PrepareGraphicsPath();
                Invalidate();
            }
        }

        public Color BackColorTo
        {
            get { return _backColorTo; }
            set { _backColorTo = value; Invalidate(); }
        }

        public LinearGradientMode BackColorGradientMode
        {
            get { return _backColorGradientMode; }
            set { _backColorGradientMode = value; Invalidate(); }
        }

        public short ItemTriangleWidth
        {
            get { return _itemTriangleWidth; }
            set
            {
                _itemTriangleWidth = value;
                PrepareGraphicsPath();
                Invalidate();
            }
        }

        public short ItemRoundWidth
        {
            get { return _itemRoundWidth; }
            set
            {
                _itemRoundWidth = value;
                PrepareGraphicsPath();
                Invalidate();
            }
        }

        public StringAlignment ItemAlignment
        {
            get { return _itemAlignment; }
            set
            {
                _itemAlignment = value;
                PrepareItems();
                Invalidate();
            }
        }

        public IGProcessUC()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
        }

        public IGProcessItem AddItem(
            string text,
            int width,
            Font textFont,
            Color textColor,
            StringAlignment textAlignment,
            Color backColorFrom,
            Color backColorTo,
            LinearGradientMode backColorGradientMode,
            Color lineColor,
            float lineWidth,
            Image icon)
        {
            IGProcessItem item = new IGProcessItem
            (
                text,
                width, 
                textFont, 
                textColor, 
                textAlignment, 
                backColorFrom, 
                backColorTo, 
                backColorGradientMode, 
                lineColor,
                lineWidth,
                icon
            );
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
            if (_items.Count == 0)
            {
                return;
            }

            int itemsTotalWidht = _items.Sum(ob => ob.Width);
            itemsTotalWidht += ((_items.Count - 1) * _itemSeperatorWidth);

            int itemStartX = 0;
            switch (_itemAlignment)
            {
                case StringAlignment.Center: itemStartX = (Width - itemsTotalWidht) / 2; break;
                case StringAlignment.Far: itemStartX = Width - itemsTotalWidht - 5; break;
                case StringAlignment.Near: itemStartX = 5; break;
            }
            PointF location = new PointF(itemStartX, (Height - _itemHeight) / 2);

            for (int i = 0; i < _items.Count; ++i)
            {
                IGProcessItem pItem = _items[i];
                pItem.BoundRect = new RectangleF(location, new Size(pItem.Width, ItemHeight));
                pItem.Origin = new PointF(pItem.BoundRect.X + pItem.BoundRect.Width / 2, pItem.BoundRect.Y + pItem.BoundRect.Height / 2);

                location.X = location.X + pItem.Width + ItemSeperatorWidth;
            }

            PrepareGraphicsPath();
        }

         private void PrepareGraphicsPath()
        {
            for (int i = 0; i < _items.Count; ++i)
            {
                IGProcessItem pItem = _items[i];
                RectangleF rect = pItem.BoundRect;
                GraphicsPath grPath = new GraphicsPath();
                bool isFirst = (i == 0);
                bool isLast = (i == _items.Count - 1);
                int itemRoundWidth = (_itemRoundingShape == ItemRoundingShapeType.Circular ? _itemRoundWidth : 0);

                PointF[] points = new PointF[isFirst || isLast ? 5 : 6];

                if (isLast)
                {
                    points[0] = new PointF(rect.X + rect.Width - itemRoundWidth, rect.Y);
                    points[1] = new PointF(rect.X, rect.Y);
                    points[2] = new PointF(rect.X + _itemTriangleWidth, rect.Y + rect.Height / 2);
                    points[3] = new PointF(rect.X, rect.Y + rect.Height);
                    points[4] = new PointF(rect.X + rect.Width - itemRoundWidth, rect.Y + rect.Height);
                }
                else
                {
                    points[0] = new PointF(rect.X + (isFirst ? itemRoundWidth : 0), rect.Y);
                    points[1] = new PointF(rect.X + rect.Width - _itemTriangleWidth, rect.Y);
                    points[2] = new PointF(rect.X + rect.Width, rect.Y + rect.Height / 2);
                    points[3] = new PointF(rect.X + rect.Width - _itemTriangleWidth, rect.Y + rect.Height);
                    points[4] = new PointF(rect.X + (isFirst ? itemRoundWidth : 0), rect.Y + rect.Height);
                }

                if (isFirst == false && isLast == false)
                {
                    points[5] = new PointF(rect.X + _itemTriangleWidth, rect.Y + rect.Height / 2);
                }

                grPath.AddLines(points);

                if ((isFirst || isLast) && _itemRoundingShape == ItemRoundingShapeType.Circular)
                {
                    int roundRectWidth = 2 * _itemRoundWidth;
                    RectangleF r = new RectangleF(rect.X + (isLast ? rect.Width - roundRectWidth : 0), rect.Y, roundRectWidth, rect.Height);
                    grPath.AddArc(r, 90, (isLast ? -1 : 1) * 180);
                }

                grPath.CloseFigure();

                if (pItem.GrPath != null)
                {
                    pItem.GrPath.Dispose();
                }
                pItem.GrPath = grPath;
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
                brush = new LinearGradientBrush(item.BoundRect, item.BackColorFrom, item.BackColorTo, item.BackColorGradientMode);
            }

            gr.FillPath(brush, item.GrPath);

            if (item.LineColor != Color.Empty)
            {
                gr.DrawPath(new Pen(item.LineColor, item.LineWidth), item.GrPath);
            }

            if (item.Icon != null)
            {
                gr.DrawImage(item.Icon, new PointF(item.BoundRect.X + _itemTriangleWidth, item.BoundRect.Y + (item.BoundRect.Height - item.Icon.Height) / 2));
            }

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = item.TextAlignment; ;
            RectangleF textRect = item.BoundRect;
            textRect.X += (_itemTriangleWidth + 3) + (item.Icon != null ? item.Icon.Width : 0);
            textRect.Width -= (2 * _itemTriangleWidth + 3);
            textRect.Y += 1;

            gr.DrawString(item.Text, item.TextFont, new SolidBrush(item.TextColor), textRect, sf);
        }

        private void Draw(Graphics gr)
        {
            if (_items.Count == 0)
            {
                return;
            }

            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (BackgroundImage == null)
            {
                if (_backColorTo == Color.Empty)
                {
                    gr.Clear(BackColor);
                }
                else
                {
                    LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, BackColor, _backColorTo, _backColorGradientMode);
                    gr.FillRectangle(brush, ClientRectangle);
                }
            }

            foreach (IGProcessItem item in _items)
            {
                DrawProcessItem(gr, item);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
            {
                PrepareDemoItems();
                Draw(e.Graphics);
                e.Graphics.DrawString("Demo", new Font("Tahoma", 8), Brushes.Black, Point.Empty);
                return;
            }

            Draw(e.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PrepareItems();
            Invalidate();
        }

        private void PrepareDemoItems()
        {
            _items.Clear();
            Font font = new Font("Tahoma", 8f);
            Color fromColor = Color.FromArgb(224, 237, 248);
            Color toColor = Color.FromArgb(94, 158, 219);
            AddItem("Dosya Hazırlık", 0, font, Color.Black, StringAlignment.Center, fromColor, toColor, LinearGradientMode.Horizontal, Color.DarkGray, 1.0f, null);
            AddItem("   ...   ", 0, font, Color.Red, StringAlignment.Center, fromColor, toColor, LinearGradientMode.BackwardDiagonal, Color.DarkGray, 1.0f, null);
            AddItem("Analiz Kontrol", 0, font, Color.White, StringAlignment.Center, fromColor, toColor, LinearGradientMode.ForwardDiagonal, Color.DarkGray, 1.0f, null);
            PrepareItems();
        }
    }
}