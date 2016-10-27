using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace TestProject
{
    public enum ItemRoundingShapeType
    {
        Rectangle,
        Circular
    }

    public partial class IGProcess : Control
    {
        private const short DefaultItemWidht = 50;
        private const short DefaultItemHeight = 35;
        private const short DefaultItemTextLeftRightEmptyWidth = 50;
        private const short DefaultItemSeperatorWidth = -17;
        private const short DefaultItemTriangleWidth = 20;
        private const short DefaultItemRoundWidth = 15;
        private const short DefaultNearFarEmptyWidth = 5;

        public class IGProcessItem
        {
            public string Text { get; set; }
            public int Width { get; set; }
            public Font TextFont { get; set; }
            public Color TextColor { get; set; }
            public StringAlignment TextAlignment { get; set; }
            public Color BackColorFrom { get; set; }
            public Color BackColorTo { get; set; }
            public LinearGradientMode BackColorGradientMode { get; set; }
            public Color LineColor { get; set; }
            public float LineWidth { get; set; }
            public Image Icon { get; set; }
        }

        private class IGProcessItemInner : IGProcessItem
        {
            public PointF Origin { get; set; }
            public RectangleF BoundRect { get; set; }
            public GraphicsPath GrPath { get; set; }
        }

        private List<IGProcessItemInner> _items = new List<IGProcessItemInner>();
        private short _itemHeight = DefaultItemHeight;
        private short _itemSeperatorWidth = DefaultItemSeperatorWidth;
        private Color _backColorTo = Color.Empty;
        private LinearGradientMode _backColorGradientMode = LinearGradientMode.Horizontal;
        private ItemRoundingShapeType _itemRoundingShape = ItemRoundingShapeType.Circular;
        private short _itemTriangleWidth = DefaultItemTriangleWidth;
        private short _itemRoundWidth = DefaultItemRoundWidth;
        private StringAlignment _itemAlignment = StringAlignment.Center;
        private short _itemTextLeftRightEmptyWidth = DefaultItemTextLeftRightEmptyWidth;
        public bool _shrinker = false;
        private int _widthBeforeShrink = 0;


        [Category("Process Items"),Description("Herbir süreç elemanının yüksekliği")]
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

        [Category("Process Items"), Description("Süreç elemanları arasındaki boşluk miktarı")]
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

        [Category("Process Items"), Description("İlk ve son süreç elemanının çizim şekli")]
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

        [Description("Süreç bileşeninin arkaplan ikinci rengi, bir renk verilirse renk geçişi çizilir")]
        public Color BackColorTo
        {
            get { return _backColorTo; }
            set { _backColorTo = value; Invalidate(); }
        }

        [Description("Süreç bileşeninin arkaplan renginin renk geçişi yöntemini gösterir")]
        public LinearGradientMode BackColorGradientMode
        {
            get { return _backColorGradientMode; }
            set { _backColorGradientMode = value; Invalidate(); }
        }

        [Category("Process Items"), Description("Ara süreç elemanlarının ok gösterim genişliği")]
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

        [Category("Process Items"), Description("İlk ve son süreç elemanının çizim şekli yuvarlak olma ölçüsü")]
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

        [Category("Process Items"), Description("Süreç elemanlarının sağ, sol ve merkez olarak hizalanması")]
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

        [Category("Process Items"), Description("Her bir süreç elemanının adının sağında ve solunda bırakılacak boşluk")]
        public short ItemTextLeftRightEmptyWidth
        {
            get { return _itemTextLeftRightEmptyWidth; }
            set
            {
                _itemTextLeftRightEmptyWidth = value;
                PrepareItems();
                Invalidate();
            }
        }

        [Description("Süreç bileşeni yeniden boyutlanınca elemanlarının ölçeklenip ölçeklenmeyeceğini gösterir")]
        public bool Shrinker
        {
            get { return _shrinker; }
            set
            {
                _shrinker = value;

                if (DesignMode == false)
                {
                    Invalidate();
                }
            }
        }

        public IGProcess()
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
            IGProcessItemInner item = new IGProcessItemInner()
            {
                Text = text,
                Width = width,
                TextFont = textFont,
                TextColor = textColor,
                TextAlignment = textAlignment,
                BackColorFrom = (backColorFrom == Color.Empty ? Color.Blue : backColorFrom),
                BackColorTo = backColorTo,
                BackColorGradientMode = backColorGradientMode,
                LineColor = lineColor,
                LineWidth = lineWidth,
                Icon = icon
            };

            _items.Add(item);
            return item;
        }

        public void AddItem(IGProcessItem item)
        {
            _items.Add((IGProcessItemInner)item);
        }

        public void ClearItems()
        {
            _items.Clear();
        }

        private int GetItemsTotalWidth()
        {
            int itemsTotalWidth = _items.Sum(ob => ob.Width);
            itemsTotalWidth += ((_items.Count - 1) * _itemSeperatorWidth);
            return itemsTotalWidth;
        }

        public void PrepareItems()
        {
            if (_items.Count == 0)
            {
                return;
            }

            foreach (IGProcessItemInner pi in _items)
            {
                if (pi.Width != 0)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(pi.Text) || pi.TextFont == null)
                {
                    pi.Width = DefaultItemWidht;
                }
                else
                {
                    pi.Width = TextRenderer.MeasureText(pi.Text, pi.TextFont).Width + (_itemTextLeftRightEmptyWidth * 2);
                }
            }

            int itemsTotalWidth = GetItemsTotalWidth();
            int itemStartX = 0;

            switch (_itemAlignment)
            {
                case StringAlignment.Center: itemStartX = (Width - itemsTotalWidth) / 2; break;
                case StringAlignment.Far: itemStartX = Width - itemsTotalWidth - DefaultNearFarEmptyWidth; break;
                case StringAlignment.Near: itemStartX = DefaultNearFarEmptyWidth; break;
            }
            PointF location = new PointF(itemStartX, (Height - _itemHeight) / 2);

            for (int i = 0; i < _items.Count; ++i)
            {
                IGProcessItemInner pItem = _items[i];
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
                IGProcessItemInner pItem = _items[i];
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

        private void DrawProcessItem(Graphics gr, IGProcessItemInner item)
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

            foreach (IGProcessItemInner item in _items)
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

        public int TotalW()
        {
            return GetItemsTotalWidth();
        }

        private void ShrinkItems()
        {
            int totalWidth = GetItemsTotalWidth() + (_itemAlignment != StringAlignment.Center ? DefaultNearFarEmptyWidth : 0);
            int diff = totalWidth - Width;

            if (diff <= 0)
            {
                if (_widthBeforeShrink == 0 || Math.Abs(diff) < (_items.Count - 1))
                {
                    return;
                }

                if (totalWidth >= _widthBeforeShrink)
                {
                    _widthBeforeShrink = 0;
                }
                else
                {
                    for (int i = 0; i < _items.Count; ++i)
                    {
                        ++_items[i].Width;
                    }
                }

                return;
            }

            if (_widthBeforeShrink == 0)
            {
                _widthBeforeShrink = GetItemsTotalWidth();
            }

            diff = _items.Count;

            while (diff > 0)
            {
                for (int i = 0; i < _items.Count && diff > 0; ++i, diff--)
                {
                    --_items[i].Width;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            if (_shrinker)
            {
                ShrinkItems();
            }

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