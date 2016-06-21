using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Timers;
using System.Windows.Forms;

namespace InfoGraphic
{
    public class InfoGraphic
    {
        public enum AnimationMethods { Default = 1, Nested = 2 };

        private const int _mainGraphicObjectRadius = 35;
        private const int _childGraphicObjectRadius = 30;
        private const int _offsetCurrectObj = 13;
        private Control _owner = null;
        private System.Timers.Timer _timer = null;
        private Bitmap _bmpCurrentObject = null;
        private Rectangle _rectCurrentObject;

        private AnimationMethods _animationMethod = AnimationMethods.Default;

        public int Width { get; set; }
        public int Height { get; set; }
        public List<InfoGraphicObject> GraphicObject { get; set; }
        public List<KeyValuePair<InfoGraphicObject, InfoGraphicObject>> GraphicRelation { get; set; }
        public List<InfoGraphicObject> GraphicPath { get; set; }
        public InfoGraphicObject CurrentObject { get; set; }
        public int LeftIndent { get; set; }
        public int RightIndent { get; set; }
        public int TopIndent { get; set; }
        public int BottomIndent { get; set; }
        public bool IsVertical { get; set; }

        public AnimationMethods AnimationMethod
        {
            get
            {
                return _animationMethod;
            }

            set
            {
                RemoveCurrentObjectHelpers(false);
                _animationMethod = value;
            }
        }

        public InfoGraphic(Control owner)
        {
            _owner = owner;

            Width = _owner.Width;
            Height = _owner.Height;

            GraphicObject = new List<InfoGraphicObject>();
            GraphicRelation = new List<KeyValuePair<InfoGraphicObject, InfoGraphicObject>>();
            GraphicPath = new List<InfoGraphicObject>();
            IsVertical = false;
            LeftIndent = 0;
            TopIndent = 0;
            BottomIndent = 0;
            RightIndent = 0;
        }

        ~InfoGraphic()
        {
            Dispose();
        }

        private void RemoveCurrentObjectHelpers(bool ownerSetNull)
        {
            if (_timer != null)
            {
                _timer.Enabled = false;
                _timer.Dispose();
                _timer = null;
            }

            if (ownerSetNull)
            {
                _owner = null;
            }

            if (_bmpCurrentObject != null)
            {
                _bmpCurrentObject.Dispose();
                _bmpCurrentObject = null;
            }
        }
        public void Dispose()
        {
            RemoveCurrentObjectHelpers(true);
        }
        public InfoGraphicObject AddGraphicObject(string caption, bool isChild)
        {
            InfoGraphicObject item = new InfoGraphicObject() { Caption = caption, IsChild = isChild, Id = (byte)(GraphicObject.Count + 1) };
            GraphicObject.Add(item);
            return item;
        }

        public void AddGraphicRelation(InfoGraphicObject key, InfoGraphicObject value)
        {
            GraphicRelation.Add(new KeyValuePair<InfoGraphicObject, InfoGraphicObject>(key, value));
        }

        public void AddGraphicPath(InfoGraphicObject currentObject, params InfoGraphicObject[] graphicObjects)
        {
            RemoveCurrentObjectHelpers(false);
            CurrentObject = currentObject;
            GraphicPath.Clear();
            GraphicPath.AddRange(graphicObjects);
        }

        public void InitializeInfoGraphicObject()
        {
            RemoveCurrentObjectHelpers(false);

            Width = _owner.Width;
            Height = _owner.Height;

            if (GraphicObject.Count == 0)
            {
                return;
            }

            #region ana grafik nesnelerinin rect'leri
            List<InfoGraphicObject> mainGraphObj = (from ob in GraphicObject
                                                    where ob.IsChild == false
                                                    select ob).ToList();
            int rectWH = (IsVertical ? (Height - TopIndent - BottomIndent) : (Width - LeftIndent - RightIndent)) / mainGraphObj.Count;

            for (int i = 0; i < mainGraphObj.Count; ++i)
            {
                if (IsVertical)
                {
                    mainGraphObj[i].MainRect = new Rectangle(0, TopIndent + i * rectWH + 1, Width - 1, rectWH);
                    mainGraphObj[i].CircleRect = new Rectangle(mainGraphObj[i].MainRect.Width / 2 - _mainGraphicObjectRadius,
                                                               mainGraphObj[i].MainRect.Y + mainGraphObj[i].MainRect.Height / 2 - _mainGraphicObjectRadius,
                                                               _mainGraphicObjectRadius * 2,
                                                               _mainGraphicObjectRadius * 2);
                }
                else
                {
                    mainGraphObj[i].MainRect = new Rectangle(LeftIndent + i * rectWH + 1, 0, rectWH, Height - 1);
                    mainGraphObj[i].CircleRect = new Rectangle(mainGraphObj[i].MainRect.X + mainGraphObj[i].MainRect.Width / 2 - _mainGraphicObjectRadius,
                                                               mainGraphObj[i].MainRect.Height / 2 - _mainGraphicObjectRadius,
                                                               _mainGraphicObjectRadius * 2,
                                                               _mainGraphicObjectRadius * 2);
                }

                Point leftCorner = mainGraphObj[i].CircleRect.Location;
                leftCorner.Offset(mainGraphObj[i].CircleRect.Width / 2, mainGraphObj[i].CircleRect.Height / 2);
                mainGraphObj[i].CircleOrigin = leftCorner;
            }
            #endregion

            #region çocuk grafik nesnelerinin rect'leri
            List<KeyValuePair<InfoGraphicObject, InfoGraphicObject>> relChild = (from ob in GraphicRelation
                                                                                 where ob.Value.IsChild == true
                                                                                 orderby ob.Key.Id 
                                                                                 select ob).ToList();
            while(relChild.Count > 0)
            {
                List<KeyValuePair<InfoGraphicObject, InfoGraphicObject>> keyRel = (from ob in relChild
                                                                                   where ob.Key.Id == relChild[0].Key.Id
                                                                                   orderby ob.Key.Id
                                                                                   select ob).ToList();
                for (int j = 0; j < keyRel.Count; ++j)
                {
                    Point priorObjOrigin = keyRel[j].Key.CircleOrigin;

                    if (j == 0)
                    {
                        if (IsVertical)
                        {
                            priorObjOrigin.X = 0;
                        }
                        else
                        {
                            priorObjOrigin.Y = 0;
                        }
                    }

                    if (IsVertical)
                    {
                        keyRel[j].Value.MainRect = new Rectangle(priorObjOrigin, new Size(keyRel[j].Key.MainRect.Width / 2, keyRel[j].Key.MainRect.Height));
                        keyRel[j].Value.CircleRect = new Rectangle(keyRel[j].Value.MainRect.X + keyRel[j].Value.MainRect.Width / 2 - _childGraphicObjectRadius,
                                                                   keyRel[j].Value.MainRect.Y + keyRel[j].Value.MainRect.Height / 2 - _childGraphicObjectRadius,
                                                                   _childGraphicObjectRadius * 2,
                                                                   _childGraphicObjectRadius * 2);
                    }
                    else
                    {
                        keyRel[j].Value.MainRect = new Rectangle(priorObjOrigin, new Size(keyRel[j].Key.MainRect.Width, keyRel[j].Key.MainRect.Height / 2));
                        keyRel[j].Value.CircleRect = new Rectangle(keyRel[j].Value.MainRect.X + keyRel[j].Value.MainRect.Width / 2 - _childGraphicObjectRadius,
                                                                   keyRel[j].Value.MainRect.Y + keyRel[j].Value.MainRect.Height / 2 - _childGraphicObjectRadius,
                                                                   _childGraphicObjectRadius * 2,
                                                                   _childGraphicObjectRadius * 2);
                    }

                    Point leftCorner = keyRel[j].Value.CircleRect.Location;
                    leftCorner.Offset(keyRel[j].Value.CircleRect.Width / 2, keyRel[j].Value.CircleRect.Height / 2);
                    keyRel[j].Value.CircleOrigin = leftCorner;

                    relChild.Remove(keyRel[j]);
                }
            }
            #endregion
        }

        public void DrawInfoGraphic(Graphics gr)
        {
            RemoveCurrentObjectHelpers(false);

            if (GraphicObject.Count == 0)
            {
                return;
            }

            using (Bitmap bmp = new Bitmap(Width, Height))
            using (Graphics bmpGraph = Graphics.FromImage(bmp))
            {
                bmpGraph.SmoothingMode = SmoothingMode.AntiAlias;
                bmpGraph.PixelOffsetMode = PixelOffsetMode.HighQuality;
                bmpGraph.Clear(Color.LightGray);

                #region draw relation lines
                if (GraphicRelation.Count > 0)
                {
                    using (Pen dotPen = new Pen(Color.Gray))
                    {
                        dotPen.Width = 1;
                        dotPen.DashStyle = DashStyle.Dot;

                        for (int i = 0; i < GraphicRelation.Count; ++i)
                        {
                            bool foundPath = false;

                            if (GraphicPath.Count > 1)
                            {
                                for (int k = 0; k < GraphicPath.Count; ++k)
                                {
                                    if (GraphicPath[k] == GraphicRelation[i].Key && (k + 1) < GraphicPath.Count && GraphicPath[k + 1] == GraphicRelation[i].Value)
                                    {
                                        foundPath = true;
                                        break;
                                    }
                                }

                                if (foundPath)
                                {
                                    foundPath = true;
                                    dotPen.Width = 5;
                                    dotPen.DashStyle = DashStyle.Solid;
                                    dotPen.Color = Color.FromArgb(188, 81, 10);
                                }
                                else
                                {
                                    dotPen.Width = 1;
                                    dotPen.DashStyle = DashStyle.Dot;
                                    dotPen.Color = Color.Gray;
                                }
                            }

                            bmpGraph.DrawLine(dotPen, GraphicRelation[i].Key.CircleOrigin, GraphicRelation[i].Value.CircleOrigin);
                            if (foundPath)
                            {
                                dotPen.Width = 3;
                                dotPen.Color = Color.FromArgb(245, 124, 45);
                                bmpGraph.DrawLine(dotPen, GraphicRelation[i].Key.CircleOrigin, GraphicRelation[i].Value.CircleOrigin);
                            }
                        }
                    }
                }
                #endregion

                #region draw info graphic objects
                for (int i = 0; i < GraphicObject.Count; ++i)
                {
                    GraphicObject[i].DrawObject(bmpGraph, GraphicPath.Exists(ob => ob == GraphicObject[i]));
                }
                #endregion

                gr.DrawImage(bmp, Point.Empty);

                if (CurrentObject != null)
                {
                    _bmpCurrentObject = new Bitmap(CurrentObject.CircleRect.Width + 2 * _offsetCurrectObj, CurrentObject.CircleRect.Height + 2 * _offsetCurrectObj);
                    _rectCurrentObject = new Rectangle(CurrentObject.CircleRect.X - _offsetCurrectObj, CurrentObject.CircleRect.Y - _offsetCurrectObj, _bmpCurrentObject.Width, _bmpCurrentObject.Height);

                    using (Graphics gCurObj = Graphics.FromImage(_bmpCurrentObject))
                    {
                        gCurObj.DrawImage(bmp, 0, 0, _rectCurrentObject, GraphicsUnit.Pixel);
                    }

                    _timer = new System.Timers.Timer(50);
                    _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                    _timer.Enabled = true;
                }
            }
        }

        private float _rotateAngle = 0.0f;
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            using (Bitmap bmp = new Bitmap(_bmpCurrentObject.Width, _bmpCurrentObject.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(_bmpCurrentObject, Point.Empty);

                float startAngle;
                float sweepAngle = (_animationMethod == AnimationMethods.Default ? 44.0F : 11.0F);
                int offs = (_offsetCurrectObj - 1) / 2 + 1;

                using (GraphicsPath grPath = new GraphicsPath())
                {
                    if (_animationMethod == AnimationMethods.Default)
                    {
                        Rectangle outerRectSmall = new Rectangle(offs, offs, bmp.Width - 2 * offs, bmp.Height - 2 * offs);
                        Rectangle innerRectSmall = new Rectangle(_offsetCurrectObj, _offsetCurrectObj, bmp.Width - 2 * _offsetCurrectObj, bmp.Height - 2 * _offsetCurrectObj);

                        for (int i = 0; i < 8; ++i)
                        {
                            startAngle = (45.0f * i) + _rotateAngle;
                            using (SolidBrush brush = new SolidBrush(Color.FromArgb(22, 148, 22)))
                            {
                                grPath.Reset();
                                grPath.AddArc(outerRectSmall, startAngle, sweepAngle);
                                grPath.AddArc(innerRectSmall, startAngle + sweepAngle, -sweepAngle);
                                grPath.CloseFigure();

                                g.FillPath(brush, grPath);
                                g.DrawPath(new Pen(Color.FromArgb(92, 231, 95)), grPath);
                            }
                        }
                    }
                    else
                    {
                        Rectangle outerRectBig = new Rectangle(1, 1, bmp.Width - 2, bmp.Height - 2);
                        Rectangle innerRectBig = new Rectangle(offs, offs, bmp.Width - 2 * offs, bmp.Height - 2 * offs);
                        Rectangle outerRectSmall = new Rectangle(offs, offs, bmp.Width - 2 * offs, bmp.Height - 2 * offs);
                        Rectangle innerRectSmall = new Rectangle(_offsetCurrectObj, _offsetCurrectObj, bmp.Width - 2 * _offsetCurrectObj, bmp.Height - 2 * _offsetCurrectObj);

                        for (int i = 0; i < 27; ++i)
                        {
                            startAngle = (10.0f * i) - _rotateAngle;
                            grPath.Reset();
                            grPath.AddArc(outerRectBig, startAngle, sweepAngle);
                            grPath.AddArc(innerRectBig, startAngle + sweepAngle, -sweepAngle);
                            using (SolidBrush brush = new SolidBrush(Color.FromArgb(21 + i, 130 + i * 3, 21 + i)))
                            {
                                g.FillPath(brush, grPath);
                            }

                            startAngle = (10.0f * i) + _rotateAngle;
                            grPath.Reset();
                            grPath.AddArc(outerRectSmall, startAngle, sweepAngle);
                            grPath.AddArc(innerRectSmall, startAngle + sweepAngle, -sweepAngle);
                            using (SolidBrush brush = new SolidBrush(Color.FromArgb(21 + i, 130 + i * 3, 21 + i)))
                            {
                                g.FillPath(brush, grPath);
                            }
                        }
                    }
                }

                if (_owner != null)
                {
                    using (Graphics gOwner = _owner.CreateGraphics())
                    {
                        gOwner.DrawImage(bmp, _rectCurrentObject);
                    }
                }

                if (_rotateAngle == 360.0f)
                {
                    _rotateAngle = 0.0f;
                }

                _rotateAngle += 2.0f;
            }
        }
    }
}


//for (int i = 0; i < count; ++i)
//{
//    /*
//    startAngle = (10.0f * i) - _rotateAngle;
//    grPath.Reset();
//    grPath.AddArc(outerRectBig, startAngle, sweepAngle);
//    grPath.AddArc(innerRectBig, startAngle + sweepAngle, -sweepAngle);
//    using (SolidBrush brush = new SolidBrush(Color.FromArgb(21 + i, 130 + i * 3, 21 + i)))
//    {
//        g.FillPath(brush, grPath);
//    }

//    startAngle = (10.0f * i) + _rotateAngle;
//    grPath.Reset();
//    grPath.AddArc(outerRectSmall, startAngle, sweepAngle);
//    grPath.AddArc(innerRectSmall, startAngle + sweepAngle, -sweepAngle);
//    using (SolidBrush brush = new SolidBrush(Color.FromArgb(21 + i, 130 + i * 3, 21 + i)))
//    {
//        g.FillPath(brush, grPath);
//    }

//    */

//    startAngle = (45.0f * i) + _rotateAngle;
//    using (SolidBrush brush = new SolidBrush(Color.FromArgb(22, 148, 22)))
//    {
//        grPath.Reset();
//        grPath.AddArc(outerRectSmall, startAngle, sweepAngle);
//        grPath.AddArc(innerRectSmall, startAngle + sweepAngle, -sweepAngle);
//        grPath.CloseFigure();

//        g.FillPath(brush, grPath);
//        g.DrawPath(new Pen(Color.FromArgb(92, 231, 95)), grPath);
//    }



//    /*
//    startAngle = (float)(45 * i) - _rotateAngle;
//    using (SolidBrush brush = new SolidBrush(Color.FromArgb(47, 131, 206)))
//    {
//        offs = (_offsetCurrectObj - 3) / 2 + 1;
//        Rectangle outerRect = new Rectangle(1, 1, bmp.Width - 2, bmp.Height - 2);
//        Rectangle innerRect = new Rectangle(offs, offs, bmp.Width - 2 * offs, bmp.Height - 2 * offs);

//        using (GraphicsPath p = new GraphicsPath())
//        {
//            p.AddArc(outerRect, startAngle, sweepAngle);
//            p.AddArc(innerRect, startAngle + sweepAngle, -sweepAngle);
//            p.CloseFigure();

//            g.FillPath(brush, p);
//            g.DrawPath(new Pen(Color.FromArgb(96, 160, 219)), p);
//        }
//    }
//    */

//    //offs = (_offsetCurrectObj - 3) / 2 + 1;

//    //startAngle = (float)(45 * i) + _rotateAngle;
//    //using (SolidBrush brush = new SolidBrush(Color.FromArgb(22, 148, 22)))
//    //{
//    //    offs += 2;

//    //    Rectangle outerRect = new Rectangle(offs, offs, bmp.Width - 2 * offs, bmp.Height - 2 * offs);
//    //    Rectangle innerRect = new Rectangle(_offsetCurrectObj, _offsetCurrectObj, bmp.Width - 2 * _offsetCurrectObj, bmp.Height - 2 * _offsetCurrectObj);

//    //    using (GraphicsPath p = new GraphicsPath())
//    //    {
//    //        p.AddArc(outerRect, startAngle, sweepAngle);
//    //        p.AddArc(innerRect, startAngle + sweepAngle, -sweepAngle);
//    //        p.CloseFigure();

//    //        g.FillPath(brush, p);
//    //        g.DrawPath(new Pen(Color.FromArgb(92, 231, 95)), p);
//    //    }
//    //}
