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
            public int Width { get; }
            public string Text { get; set; }
            public Font TextFont { get; set; }
            public Color TextColor { get; set; }
            public StringAlignment TextAlignment { get; set; }
            public float TextAngle { get; set; }
            public Color BackColorFrom { get; set; }
            public Color BackColorTo { get; set; }
            public LinearGradientMode BackColorGradientMode { get; set; }
            public Color LineColor { get; set; }
            public float LineWidth { get; set; }
            public Image Icon { get; set; }

            public void Refresh()
            {
                IGProcessItemInner item = (IGProcessItemInner)this;
                using (Graphics gr = item.Owner.CreateGraphics())
                {
                    item.Owner.ConfigureGraphics(gr);
                    item.Draw(gr);
                }
            }
        }

        private class IGProcessItemInner : IGProcessItem
        {
            public IGProcess Owner { get; set; }
            public new int Width { get; set; }
            public PointF Origin { get; set; }
            public RectangleF BoundRect { get; set; }
            public GraphicsPath GrPath { get; set; }

            public void Draw(Graphics gr)
            {
                Brush brush;

                if (BackColorTo == Color.Empty)
                {
                    brush = new SolidBrush(BackColorFrom);
                }
                else
                {
                    brush = new LinearGradientBrush(BoundRect, BackColorFrom, BackColorTo, BackColorGradientMode);
                }

                gr.FillPath(brush, GrPath);

                if (LineColor != Color.Empty)
                {
                    gr.DrawPath(new Pen(LineColor, LineWidth), GrPath);
                }

                if (Icon != null)
                {
                    gr.DrawImage(Icon, new PointF(BoundRect.X + Owner.ItemTriangleWidth, BoundRect.Y + (BoundRect.Height - Icon.Height) / 2));
                }

                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = TextAlignment;
                RectangleF textRect = BoundRect;

                if (Owner.IsVertical)
                {
                    textRect.Y += (Owner.ItemTriangleWidth + 3) + (Icon != null ? Icon.Width : 0);
                    textRect.Height -= (2 * Owner.ItemTriangleWidth + 3);
                }
                else
                {
                    textRect.X += (Owner.ItemTriangleWidth + 3) + (Icon != null ? Icon.Width : 0);
                    textRect.Width -= (2 * Owner.ItemTriangleWidth + 3);
                }

                textRect.Y += 1;

                if (TextAngle == 0.0f)
                {
                    gr.DrawString(Text, TextFont, new SolidBrush(TextColor), textRect, sf);
                }
                else
                {
                    gr.TranslateTransform(textRect.Left + textRect.Width / 2, textRect.Top + textRect.Height / 2);
                    gr.RotateTransform(TextAngle);
                    RectangleF newTextRect = new RectangleF(-textRect.Width / 2, -textRect.Height / 2, textRect.Width, textRect.Height);
                    gr.DrawString(Text, TextFont, new SolidBrush(TextColor), newTextRect, sf);
                    gr.ResetTransform();
                }
            }
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
        private bool _isVertical = false;

        [Browsable(false)]
        public IGProcessItem[] Items
        {
            get { return _items.ToArray(); }
        }

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
                PrepareItemsGraphicsPath();
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
                PrepareItemsGraphicsPath();
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
                PrepareItemsGraphicsPath();
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

        [Description("Süreç bileşeninin dikey olarak çizilip çizilmeyeceğini gösterir")]
        public bool IsVertical
        {
            get { return _isVertical; }
            set
            {
                _isVertical = value;
                PrepareItems();
                Invalidate();
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
            float textAngle,
            Color backColorFrom,
            Color backColorTo,
            LinearGradientMode backColorGradientMode,
            Color lineColor,
            float lineWidth,
            Image icon)
        {
            IGProcessItemInner item = new IGProcessItemInner()
            {
                Owner = this,
                Text = text,
                Width = width,
                TextFont = textFont,
                TextColor = textColor,
                TextAlignment = textAlignment,
                TextAngle = textAngle,
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
                    Size textSize = TextRenderer.MeasureText(pi.Text, pi.TextFont);
                    pi.Width = (_isVertical ? textSize.Height : textSize.Width) + (_itemTextLeftRightEmptyWidth * 2);
                }
            }

            int itemsTotalWidth = GetItemsTotalWidth();
            float itemStartX_Y = 0;
            int w_h = (_isVertical ? Height : Width);

            switch (_itemAlignment)
            {
                case StringAlignment.Center: itemStartX_Y = (w_h - itemsTotalWidth) / 2; break;
                case StringAlignment.Far: itemStartX_Y = w_h - itemsTotalWidth - DefaultNearFarEmptyWidth; break;
                case StringAlignment.Near: itemStartX_Y = DefaultNearFarEmptyWidth; break;
            }

            float x = (_isVertical ? (Width - _itemHeight) / 2 : itemStartX_Y);
            float y = (_isVertical ? itemStartX_Y : (Height - _itemHeight) / 2);
            PointF location = new PointF(x, y);

            for (int i = 0; i < _items.Count; ++i)
            {
                IGProcessItemInner pItem = _items[i];
                pItem.BoundRect = new RectangleF(location, new Size((_isVertical ? _itemHeight : pItem.Width), (_isVertical ? pItem.Width : _itemHeight)));
                pItem.Origin = new PointF(pItem.BoundRect.X + pItem.BoundRect.Width / 2, pItem.BoundRect.Y + pItem.BoundRect.Height / 2);

                if (_isVertical)
                {
                    location.Y += pItem.Width + ItemSeperatorWidth;
                }
                else
                {
                    location.X += pItem.Width + ItemSeperatorWidth;
                }
            }

            PrepareItemsGraphicsPath();
        }

        private void PrepareItemsGraphicsPath()
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
                    if (_isVertical)
                    {
                        points[0] = new PointF(rect.Right, rect.Bottom - itemRoundWidth);
                        points[1] = new PointF(rect.Right, rect.Y);
                        points[2] = new PointF(rect.X + rect.Width / 2, rect.Y + _itemTriangleWidth);
                        points[3] = new PointF(rect.X, rect.Y);
                        points[4] = new PointF(rect.X, rect.Bottom - itemRoundWidth);
                    }
                    else
                    {
                        points[0] = new PointF(rect.Right - itemRoundWidth, rect.Y);
                        points[1] = new PointF(rect.X, rect.Y);
                        points[2] = new PointF(rect.X + _itemTriangleWidth, rect.Y + rect.Height / 2);
                        points[3] = new PointF(rect.X, rect.Bottom);
                        points[4] = new PointF(rect.Right - itemRoundWidth, rect.Bottom);
                    }
                }
                else
                {
                    if(_isVertical)
                    {
                        points[0] = new PointF(rect.Right, rect.Y + (isFirst ? itemRoundWidth : 0));
                        points[1] = new PointF(rect.Right, rect.Bottom - _itemTriangleWidth);
                        points[2] = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height);
                        points[3] = new PointF(rect.X, rect.Bottom - _itemTriangleWidth);
                        points[4] = new PointF(rect.X, rect.Y + (isFirst ? itemRoundWidth : 0));
                    }
                    else
                    {
                        points[0] = new PointF(rect.X + (isFirst ? itemRoundWidth : 0), rect.Y);
                        points[1] = new PointF(rect.Right - _itemTriangleWidth, rect.Y);
                        points[2] = new PointF(rect.Right, rect.Y + rect.Height / 2);
                        points[3] = new PointF(rect.Right - _itemTriangleWidth, rect.Bottom);
                        points[4] = new PointF(rect.X + (isFirst ? itemRoundWidth : 0), rect.Bottom);
                    }
                }

                if (isFirst == false && isLast == false)
                {
                    if (_isVertical)
                    {
                        points[5] = new PointF(rect.X + rect.Width / 2, rect.Y + _itemTriangleWidth);
                    }
                    else
                    {
                        points[5] = new PointF(rect.X + _itemTriangleWidth, rect.Y + rect.Height / 2);
                    }
                }

                grPath.AddLines(points);

                if ((isFirst || isLast) && _itemRoundingShape == ItemRoundingShapeType.Circular)
                {
                    int roundRectWidth = 2 * _itemRoundWidth;

                    if (_isVertical)
                    {
                        RectangleF r = new RectangleF(rect.X, (isLast ? rect.Bottom - roundRectWidth : rect.Y), rect.Width, roundRectWidth);
                        grPath.AddArc(r, 180, (isLast ? -1 : 1) * 180);
                    }
                    else
                    {
                        RectangleF r = new RectangleF((isLast ? rect.Right - roundRectWidth : rect.X), rect.Y, roundRectWidth, rect.Height);
                        grPath.AddArc(r, 90, (isLast ? -1 : 1) * 180);
                    }
                }

                grPath.CloseFigure();

                if (pItem.GrPath != null)
                {
                    pItem.GrPath.Dispose();
                }

                pItem.GrPath = grPath;
            }
        }

        public void ConfigureGraphics(Graphics gr)
        {
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        private void DrawBackground(Graphics gr)
        {
            if (BackgroundImage != null)
            {
                return;
            }

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
        private void Draw(Graphics gr)
        {
            if (_items.Count == 0)
            {
                return;
            }

            ConfigureGraphics(gr);
            DrawBackground(gr);

            foreach (IGProcessItemInner item in _items)
            {
                item.Draw(gr);
            }

            /*
            if (_items.Count > 0 && _isVertical)
            {
                IGProcessItemInner it = _items[1];
                PointF[] p = new PointF[8];
                RectangleF rect = it.BoundRect;
                p[0] = new PointF(rect.Right + 1, rect.Top);
                p[1] = new PointF(ClientRectangle.Right - 5, rect.Top - 5);
                p[2] = new PointF(rect.Right + 3 * (ClientRectangle.Right - rect.Right) / 4, 3 * ClientRectangle.Top + (rect.Top - ClientRectangle.Top) / 4);
                p[3] = new PointF(ClientRectangle.Right, ClientRectangle.Top);
                p[4] = new PointF(ClientRectangle.Right, ClientRectangle.Bottom);
                p[5] = new PointF(rect.Right + 3 * (ClientRectangle.Right - rect.Right) / 4, ClientRectangle.Bottom - 3 * (ClientRectangle.Bottom - rect.Bottom) / 4);
                p[6] = new PointF(ClientRectangle.Right - 5, rect.Bottom + 5);
                p[7] = new PointF(rect.Right + 1, rect.Bottom - _itemTriangleWidth);
                
                GraphicsPath path = new GraphicsPath();
                path.AddBezier(p[0], p[1], p[2], p[3]);
                path.AddBezier(p[4], p[5], p[6], p[7]);
                path.CloseFigure();

                LinearGradientBrush gBrush = new LinearGradientBrush(new RectangleF(rect.Right, ClientRectangle.Top, ClientRectangle.Right - rect.Right, ClientRectangle.Height),
                    Color.White,
                    //Color.FromArgb(91, 155, 213),
                    Color.FromArgb(133, 181, 234),

                    //Color.LightGray,
                LinearGradientMode.Horizontal);
                //gr.FillPath(new SolidBrush(Color.FromArgb(91, 155, 213)), path);
                gr.FillPath(gBrush, path);
                path.Dispose();

                //foreach(PointF po in p)
                //{
                //    gr.DrawEllipse(Pens.Red, new RectangleF(po, new Size(6, 6)));
                //}
            }
            */
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

        private void ShrinkItems()
        {
            int totalWidth = GetItemsTotalWidth() + (_itemAlignment != StringAlignment.Center ? DefaultNearFarEmptyWidth : 0);
            int diff = totalWidth - (_isVertical ? Height : Width);

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
            AddItem("Process Item1", 0, font, Color.Black, StringAlignment.Center, 0, fromColor, toColor, LinearGradientMode.Horizontal, Color.DarkGray, 1.0f, null);
            AddItem("Process Item...", 0, font, Color.Red, StringAlignment.Center, 0, fromColor, toColor, LinearGradientMode.BackwardDiagonal, Color.DarkGray, 1.0f, null);
            AddItem("Process ItemN", 0, font, Color.White, StringAlignment.Center, 0, fromColor, toColor, LinearGradientMode.ForwardDiagonal, Color.DarkGray, 1.0f, null);
            PrepareItems();
        }
    }
}