using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Timers;
using System.Windows.Forms;

namespace InfoGraphicProject
{
    public partial class InfoGraphic : UserControl
    {
        public class InfoGraphicObject
        {
            public byte Id;
            public Rectangle MainRect;
            public Rectangle CircleRect;
            public Point CircleOrigin;
            public string Caption;
            public bool IsChild;
            public Font CaptionFont;

            public InfoGraphicObject()
            {
                CaptionFont = new Font("Tahoma", 8.0f);
            }

            public void DrawObject(Graphics gr, bool foundInGraphicPath)
            {
                //gr.DrawRectangle(Pens.Blue, MainRect);
                //gr.DrawRectangle(Pens.Red, CircleRect);
                //return;

                LinearGradientBrush gradientBrush;

                if (foundInGraphicPath)
                {
                    gradientBrush = new LinearGradientBrush(CircleRect, Color.FromArgb(224, 237, 248), Color.FromArgb(94, 158, 219), LinearGradientMode.ForwardDiagonal);
                }
                else
                {
                    gradientBrush = new LinearGradientBrush(CircleRect, Color.FromArgb(224, 237, 248), Color.Gray, LinearGradientMode.ForwardDiagonal);
                }

                Rectangle circleRect = CircleRect;

                if (foundInGraphicPath)
                {
                    gr.FillEllipse(new SolidBrush(Color.FromArgb(245, 124, 45)), circleRect);
                }
                else
                {
                    gr.FillEllipse(new SolidBrush(Color.DarkGray), circleRect);
                }

                circleRect.Inflate(-3, -3);
                gr.FillEllipse(gradientBrush, circleRect);
                circleRect.Inflate(3, 3);

                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                gr.DrawString(Caption, CaptionFont, new SolidBrush(Color.Black), CircleRect, sf);
            }
        }

        public enum AnimationMethods { Default = 1, Nested = 2 };

        private const int _mainGraphicObjectRadius = 35;
        private const int _childGraphicObjectRadius = 30;
        private const int _offsetCurrectObj = 13;
        private System.Timers.Timer _timer = null;
        private Bitmap _bmpCurrentObject = null;
        private Rectangle _rectCurrentObject;
        private InfoGraphicObject _currentObject = null;
        private float _rotateAngle = 0.0f;

        private AnimationMethods _animationMethod = AnimationMethods.Default;
        private int _leftIndent = 0;
        private int _rightIndent = 0;
        private int _topIndent = 0;
        private int _bottomIndent = 0;
        private double _animationInterval = 50.0;
        private bool _isInInitializing = false;
        private bool _isVertical = false;

        [Browsable(false)]
        public List<InfoGraphicObject> GraphicObject { get; } = new List<InfoGraphicObject>();

        [Browsable(false)]
        public List<KeyValuePair<InfoGraphicObject, InfoGraphicObject>> GraphicRelation { get; } = new List<KeyValuePair<InfoGraphicObject, InfoGraphicObject>>();

        [Browsable(false)]
        public List<InfoGraphicObject> GraphicPath { get; } = new List<InfoGraphicObject>();

        [Browsable(false)]
        public InfoGraphicObject CurrentObject { get { return _currentObject; } }
        public int LeftIndent
        {
            get { return _leftIndent; }
            set
            {
                if (_leftIndent == value) return;
                _leftIndent = value;
                Invalidate();
            }
        }

        public int RightIndent
        {
            get { return _rightIndent; }
            set
            {
                if (_rightIndent == value) return;
                _rightIndent = value;
                Invalidate();
            }
        }

        public int TopIndent
        {
            get { return _topIndent; }
            set
            {
                if (_topIndent == value) return;
                _topIndent = value;
                Invalidate();
            }
        }

        public int BottomIndent
        {
            get { return _bottomIndent; }
            set
            {
                if (_bottomIndent == value) return;
                _bottomIndent = value;
                Invalidate();
            }
        }

        public double AnimationInterval
        {
            get { return _animationInterval; }
            set
            {
                _animationInterval = value;
                if (_timer != null)
                {
                    _timer.Interval = _animationInterval;
                }
            }
        }
        
        public bool IsVertical
        {
            get { return _isVertical; }
            set
            {
                if (_isVertical == value) return;
                _isVertical = value;
                Invalidate();
            }
        }

        public AnimationMethods AnimationMethod
        {
            get
            {
                return _animationMethod;
            }

            set
            {
                RemoveAnimationHelpers();
                _animationMethod = value;
            }
        }

        public InfoGraphic()
        {
            InitializeComponent();
        }

        // protected override void Dispose(bool disposing) metodundan RemoveAnimationHelpers() çağrılmalı
        private void RemoveAnimationHelpers()
        {
            if (_timer != null)
            {
                _timer.Enabled = false;
                _timer.Dispose();
                _timer = null;
            }

            if (_bmpCurrentObject != null)
            {
                _bmpCurrentObject.Dispose();
                _bmpCurrentObject = null;
            }
        }

        public void BeginInitalize()
        {
            _isInInitializing = true;
        }

        public void EndInitialize()
        {
            _isInInitializing = false;
            InitializeInfoGraphicObject();

            if (DesignMode == false)
            {
                Invalidate();
            }
        }

        private bool ControlInInitialize()
        {
            if (_isInInitializing == false)
            {
                MessageBox.Show("Call BeginInsertGraphicObject method first, later call EndInsertGraphicObject method!");
                return false;
            }

            return true;
        }

        public InfoGraphicObject AddGraphicObject(string caption, bool isChild)
        {
            if (ControlInInitialize() == false) return null;

            InfoGraphicObject item = new InfoGraphicObject() { Caption = caption, IsChild = isChild, Id = (byte)(GraphicObject.Count + 1) };
            GraphicObject.Add(item);
            return item;
        }

        public void AddGraphicObject(InfoGraphicObject[] objectArray)
        {
            if (ControlInInitialize() == false) return;

            GraphicObject.Clear();
            GraphicObject.AddRange(objectArray);
        }

        public void AddGraphicRelation(InfoGraphicObject key, InfoGraphicObject value)
        {
            if (ControlInInitialize() == false) return;

            GraphicRelation.Add(new KeyValuePair<InfoGraphicObject, InfoGraphicObject>(key, value));
        }

        public void AddGraphicRelation(KeyValuePair<InfoGraphicObject, InfoGraphicObject> [] objectArray)
        {
            if (ControlInInitialize() == false) return;

            GraphicRelation.Clear();
            GraphicRelation.AddRange(objectArray);
        }

        public void AddGraphicPath(InfoGraphicObject currentObject, params InfoGraphicObject[] graphicObjects)
        {
            RemoveAnimationHelpers();
            _currentObject = currentObject;
            GraphicPath.Clear();
            GraphicPath.AddRange(graphicObjects);
            Invalidate();
        }

        public void ClearObjects()
        {
            RemoveAnimationHelpers();

            _currentObject = null;
            GraphicObject.Clear();
            GraphicRelation.Clear();
            GraphicPath.Clear();
        }

        private void InitializeInfoGraphicObject()
        {
            RemoveAnimationHelpers();

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
            while (relChild.Count > 0)
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
            RemoveAnimationHelpers();

            if (GraphicObject.Count == 0 || _isInInitializing == true)
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

                if (_currentObject != null)
                {
                    _bmpCurrentObject = new Bitmap(_currentObject.CircleRect.Width + 2 * _offsetCurrectObj, _currentObject.CircleRect.Height + 2 * _offsetCurrectObj);
                    _rectCurrentObject = new Rectangle(_currentObject.CircleRect.X - _offsetCurrectObj, _currentObject.CircleRect.Y - _offsetCurrectObj, _bmpCurrentObject.Width, _bmpCurrentObject.Height);

                    using (Graphics gCurObj = Graphics.FromImage(_bmpCurrentObject))
                    {
                        gCurObj.DrawImage(bmp, 0, 0, _rectCurrentObject, GraphicsUnit.Pixel);
                    }

                    _timer = new System.Timers.Timer(_animationInterval);
                    _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                    _timer.Enabled = true;
                }
            }
        }

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

                using (Graphics gOwner = CreateGraphics())
                {
                    gOwner.DrawImage(bmp, _rectCurrentObject);
                }

                if (_rotateAngle == 360.0f)
                {
                    _rotateAngle = 0.0f;
                }

                _rotateAngle += 2.0f;
            }
        }

        private void Demo()
        {
            InfoGraphicObject main1;
            InfoGraphicObject child1;
            InfoGraphicObject main2;
            InfoGraphicObject child2;
            InfoGraphicObject child3;
            InfoGraphicObject main3;

            ClearObjects();

            BeginInitalize();
            main1 = AddGraphicObject("Main 1", false);
            main2 = AddGraphicObject("Main 2", false);
            main3 = AddGraphicObject("Main 3", false);
            child1 = AddGraphicObject("Child 1", true);
            child2 = AddGraphicObject("Child 2", true);
            child3 = AddGraphicObject("Child 3", true);

            AddGraphicRelation(main1, main2);
            AddGraphicRelation(main2, main3);
            AddGraphicRelation(main1, child1);
            AddGraphicRelation(child1, main2);
            AddGraphicRelation(main2, child2);
            AddGraphicRelation(main2, child3);
            EndInitialize();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
            {
                Demo();
            }

            DrawInfoGraphic(e.Graphics);

            if (DesignMode)
            {
                e.Graphics.DrawString("Demo", GraphicObject[0].CaptionFont, Brushes.Black, Point.Empty);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            RemoveAnimationHelpers();
            base.OnResize(e);
            InitializeInfoGraphicObject();

            //Invalidate();
        }
    }
}