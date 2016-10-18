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
    public partial class IGProcess : UserControl
    {
        private const int DefaultItemWidht = 50;
        private const int DefaultItemHeight = 30;
        private const int ItemTextLeftOrRightEmptyWidth = 15;
        private const int ItemSeperatorWidth = 5;
        private const int ItemBeginOrEndEmptyWidth = 10;

        public class IGProcessItem
        {
            public string Text { get; set; }
            public int Width { get; set; }
            public Font TextFont { get; set; }
            public Rectangle BoundedRect { get; set; }
            public Point Origin { get; set; }

            public IGProcessItem(string text, int width, Font font)
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
            }

            public void Draw(Graphics gr)
            {
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
            }
        }

        private List<IGProcessItem> _items = new List<IGProcessItem>();
        public int ItemHeight { get; set; }

        public IGProcess()
        {
            ItemHeight = DefaultItemHeight;

            InitializeComponent();
        }

        public IGProcessItem AddItem(string text, int width, Font font)
        {
            IGProcessItem item = new IGProcessItem(text, width, font);
            _items.Add(item);
            return item;
        }

        public void AddItem(IGProcessItem[] itemArray)
        {
            _items.Clear();
            _items.AddRange(itemArray);
        }

        public void InitializeItems()
        {
            if (_items.Count == 0)
            {
                return;
            }

            int itemsWidht = _items.Sum(ob => ob.Width);
            itemsWidht += ((_items.Count - 1) * ItemSeperatorWidth) + (2 * ItemBeginOrEndEmptyWidth);
            int start

            for (int i = 0; i < _items.Count; ++i)
            {
                IGProcessItem pItem = _items[i];

                pItem.BoundedRect = new Rectangle()
            }


                foreach (IGProcessItem ob in _items)
                {
                    //ob.BoundedRect = 

                    //e.Graphics.DrawRectangle(Pens.Blue, ob.ClientRectangle);
                }
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

            foreach (IGProcessItem ob in _items)
            {
                ob.Draw(e.Graphics);

                //e.Graphics.DrawRectangle(Pens.Blue, ob.ClientRectangle);
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