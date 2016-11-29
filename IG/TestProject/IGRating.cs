using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TestProject
{
    public partial class IGRating : Control
    {
        private const short DefaultRowLineWidth = 1;
        private const short DefaultLeftIndent = 3;
        private const short DefaultRightIndent = 3;
        private const short DefaultNoteBarWidth = 25;

        public class RatingNote // ==> shortly note
        {
            public string Name;
            public float FromValue;
            public float ToValue;

            public bool Equals(string noteName, float noteValue)
            {
                return (noteName != string.Empty && Name == noteName)
                       ||
                       (noteValue != 0.0 && noteValue >= FromValue && noteValue <= ToValue);
            }
        }

        #region Brush Info
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class BrushInfo
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private Color _fromColor;
            private Color _toColor;
            private LinearGradientMode _gradientMode;
            private float _angle;

            public BrushInfo(Color fromColor, Color toColor, LinearGradientMode gradientMode, float angle)
            {
                _fromColor = fromColor;
                _toColor = ToColor;
                _gradientMode = gradientMode;
                _angle = angle;
            }

            // !!!!!     this Attributes doesn't work (or I could not do it)      !!!!!
            //[NotifyParentProperty(true)]
            //[RefreshProperties(RefreshProperties.All)]
            public Color FromColor
            {
                get { return _fromColor; }
                set
                {
                    _fromColor = value;
                    OnChanged("FromColor");
                }
            }

            public Color ToColor
            {
                get { return _toColor; }
                set
                {
                    _toColor = value;
                    OnChanged("ToColor");
                }
            }

            public LinearGradientMode GradientMode
            {
                get { return _gradientMode; }
                set
                {
                    _gradientMode = value;
                    OnChanged("GradientMode");
                }
            }

            public float Angle
            {
                get { return _angle; }
                set
                {
                    _angle = value;
                    OnChanged("Angle");
                }
            }

            protected void OnChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public Brush GetBrush(Rectangle rect)
            {
                if (_toColor == Color.Empty)
                {
                    return new SolidBrush(_fromColor);
                }

                if (_angle == 0)
                {
                    return new LinearGradientBrush(rect, _fromColor, _toColor, _gradientMode);
                }
                else
                {
                    return new LinearGradientBrush(rect, _fromColor, _toColor, _angle);
                }
            }
        }
        #endregion

        public enum NoteBarShapeType
        {
            Rectangle,
            RoundRectangle,
            Ellipse
        }

        private List<RatingNote> _ratings = new List<RatingNote>();
        private float _rowHeight = 0;
        private BrushInfo _backGroundBrush = new BrushInfo(Color.White, Color.Empty, LinearGradientMode.Horizontal, 0);
        private short _rowLineWidth = DefaultRowLineWidth;
        private BrushInfo _rowLineBrush = new BrushInfo(Color.DarkGray, Color.Empty, LinearGradientMode.Horizontal, 0);
        private short _leftIndent = DefaultLeftIndent;
        private short _rightIndent = DefaultRightIndent;
        private Color _frameColor = Color.DarkGray;
        private NoteBarShapeType _noteBarShape = NoteBarShapeType.Rectangle;
        private short _noteBarWidth = DefaultNoteBarWidth;
        private BrushInfo _inactiveNoteBarBrush = new BrushInfo(Color.Gainsboro, Color.Empty, LinearGradientMode.Horizontal, 0);
        private Color _inactiveNoteBarFrameColor = Color.Empty;
        private BrushInfo _activeNoteBarBrush = new BrushInfo(Color.DodgerBlue, Color.Empty, LinearGradientMode.Horizontal, 0);
        private Color _activeNoteBarFrameColor = Color.Empty;
        private StringAlignment _noteTextAlignment = StringAlignment.Near;
        private bool _showNoteName = true;
        private bool _showNoteValue = false;
        private string _currentNoteName = string.Empty;
        private float _currentNoteValue = 0.0f;
        private Color _herePointerColor = Color.Green;
        private bool _showHerePointer = true;
        private bool _noteBarWidthAccordingToRowHeight = false;
        private string _cutOffNoteName = string.Empty;
        private float _cutOffNoteValue = 0.0f;
        private Color _cutOffPointerColor = Color.Red;
        private string _calculatedNoteName = string.Empty;
        private float _calculatedNoteValue = 0.0f;
        private Color _calculatedNoteColor = Color.White;

        // to do:
        // bar shape roundrectangle

        #region Published Properties
        [Browsable(false)]
        public new Color BackColor
        {
            get;
        }

        public new Rectangle ClientRectangle
        {
            get
            {
                if (_frameColor == Color.Empty)
                {
                    return base.ClientRectangle;
                }

                Rectangle clientRect = base.ClientRectangle;
                clientRect.Inflate(-1, -1);
                return clientRect;
            }
        }

        [Browsable(false)]
        public RatingNote[] Ratings
        {
            get { return _ratings.ToArray(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         Description("Arkaplan boyama stili ve renkleri")]
        public BrushInfo BackGroundBrush
        {
            get { return _backGroundBrush; }
            set
            {
                if (value == null)
                {
                    return;
                }

                _backGroundBrush = value;
                Invalidate();
            }
        }

        [Description("Satırlar arası çizginin kalınlığı"),
         Category("RowLine")]
        public short RowLineWidth
        {
            get { return _rowLineWidth; }
            set
            {
                _rowLineWidth = value;
                ArrangeRowHeight();
                Invalidate();
            }
        }

        [Description("Solda bırakılan boşluk")]
        public short LeftIndent
        {
            get { return _leftIndent; }
            set { _leftIndent = value; Invalidate(); }
        }

        [Description("Sağda bırakılan boşluk")]
        public short RightIndent
        {
            get { return _rightIndent; }
            set { _rightIndent = value; Invalidate(); }
        }

        [Description("Grafik barın şekli"),
         Category("RatingNoteBar")]
        public NoteBarShapeType NoteBarShape
        {
            get { return _noteBarShape; }
            set { _noteBarShape = value; Invalidate(); }
        }

        [Description("Grafik barın genişliği"),
         Category("RatingNoteBar")]
        public short NoteBarWidth
        {
            get { return _noteBarWidth; }
            set { _noteBarWidth = value;  Invalidate(); }
        }

        [Description("Dış çerçeve rengi")]
        public Color FrameColor
        {
            get { return _frameColor; }
            set { _frameColor = value; Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         Description("Satır çizgisi boyama stili ve renkleri"),
         Category("RowLine")]
        public BrushInfo RowLineBrush
        {
            get { return _rowLineBrush; }
            set
            {
                if (value == null)
                {
                    return;
                }

                _rowLineBrush = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         Description("Pasif rating notu barının boyama stili ve renkleri"),
         Category("RatingNoteBar")]
        public BrushInfo InactiveNoteBarBrush
        {
            get { return _inactiveNoteBarBrush; }
            set
            {
                if (value == null)
                {
                    return;
                }

                _inactiveNoteBarBrush = value;
                Invalidate();
            }
        }

        [Description("Pasif rating notu barının çerçeve rengi"),
         Category("RatingNoteBar")]
        public Color InactiveNoteBarFrameColor
        {
            get { return _inactiveNoteBarFrameColor; }
            set { _inactiveNoteBarFrameColor = value; Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         Description("Aktif rating notu barının boyama stili ve renkleri"),
         Category("RatingNoteBar")]
        public BrushInfo ActiveNoteBarBrush
        {
            get { return _activeNoteBarBrush; }
            set
            {
                if (value == null)
                {
                    return;
                }

                _activeNoteBarBrush = value;
                Invalidate();
            }
        }

        [Description("Aktif rating notu barının çerçeve rengi"),
         Category("RatingNoteBar")]
        public Color ActiveNoteBarFrameColor
        {
            get { return _activeNoteBarFrameColor; }
            set { _activeNoteBarFrameColor = value; Invalidate(); }
        }

        [Description("Rating notu yazısının hizalanması"),
         Category("RatingNoteText")]
        public StringAlignment NoteTextAlignment
        {
            get { return _noteTextAlignment; }
            set { _noteTextAlignment = value; Invalidate(); }
        }

        [Description("Rating notu yazısının gösterilip gösterilmeyeceğini belirtir"),
         Category("RatingNoteText")]
        public bool ShowNoteName
        {
            get { return _showNoteName; }
            set { _showNoteName = value; Invalidate(); }
        }

        [Description("Rating notu değerlerinin gösterilip gösterilmeyeceğini belirtir"),
         Category("RatingNoteText")]
        public bool ShowNoteValue
        {
            get { return _showNoteValue; }
            set { _showNoteValue = value; Invalidate(); }
        }

        [Description("Rating notu")]
        public string CurrentNoteName
        {
            get { return _currentNoteName; }
            set { _currentNoteName = value; Invalidate(); }
        }

        [Description("Rating notu")]
        public float CurrentNoteValue
        {
            get { return _currentNoteValue; }
            set { _currentNoteValue = value; Invalidate(); }
        }

        [Description("Rating notu barı genişliği ve satır yüksekliğini dikkate alarak eşit bar çizer"),
         Category("RatingNoteBar")]
        public bool NoteBarWidthAccordingToRowHeight
        {
            get { return _noteBarWidthAccordingToRowHeight; }
            set { _noteBarWidthAccordingToRowHeight = value; Invalidate(); }
        }

        [Description("Aktif rating notunu gösteren işaretçinin rengi")]
        public Color HerePointerColor
        {
            get { return _herePointerColor; }
            set { _herePointerColor = value; Invalidate(); }
        }

        [Description("Aktif rating notunu gösteren işaretçinin çizilip çizilmeyeceğini belirtir")]
        public bool ShowHerePointer
        {
            get { return _showHerePointer; }
            set { _showHerePointer = value; Invalidate(); }
        }

        [Description("Rating notu için Cut off not ismi"),
        Category("CutOff")]
        public string CutOffNoteName
        {
            get { return _cutOffNoteName; }
            set { _cutOffNoteName = value; Invalidate(); }
        }

        [Description("Rating notu için Cut off not değeri"),
        Category("CutOff")]
        public float CutOffNoteValue
        {
            get { return _cutOffNoteValue; }
            set { _cutOffNoteValue = value; Invalidate(); }
        }

        [Description("Cut off işaretçisinin rengi"),
         Category("CutOff")]
        public Color CutOffPointerColor
        {
            get { return _cutOffPointerColor; }
            set { _cutOffPointerColor = value; Invalidate(); }
        }

        [Description("Rating notunun kullanıcı tarafından ezilmeden önceki halinin ismini belirtir"),
         Category("CalculatedNote")]
        public string CalculatedNoteName
        {
            get { return _calculatedNoteName; }
            set { _calculatedNoteName = value; Invalidate(); }
        }

        [Description("Rating notunun kullanıcı tarafından ezilmeden önceki halinin değerini belirtir"),
         Category("CalculatedNote")]
        public float CalculatedNoteValue
        {
            get { return _calculatedNoteValue; }
            set { _calculatedNoteValue = value; Invalidate(); }
        }

        [Description("Rating notunun kullanıcı tarafından ezilmeden önceki halini gösteren işaretçi rengi"),
         Category("CalculatedNote")]
        public Color CalculatedNoteColor
        {
            get { return _calculatedNoteColor; }
            set { _calculatedNoteColor = value; Invalidate(); }
        }
        
        #endregion

        public IGRating()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            _backGroundBrush.PropertyChanged += PropertyChanged;
            _rowLineBrush.PropertyChanged += PropertyChanged;
            _inactiveNoteBarBrush.PropertyChanged += PropertyChanged;
            _activeNoteBarBrush.PropertyChanged += PropertyChanged;
            Font = new Font("Tahoma", 8f);
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Invalidate();
        }

        private void ArrangeRowHeight()
        {
            short diff = (short)(_frameColor == Color.Empty ? 0 : 2);

            if (_ratings.Count == 0)
            {
                _rowHeight = Height - diff;
                return;
            }

            _rowHeight = (Height - diff - _rowLineWidth * (_ratings.Count - 1)) / (float)_ratings.Count;
            _rowHeight = (float)Math.Round(_rowHeight, 0);
        }

        public RatingNote AddRating(string ratingName, float fromValue, float toValue)
        {
            RatingNote rating = new RatingNote { Name = ratingName, FromValue = fromValue, ToValue = toValue };
            _ratings.Add(rating);
            _ratings.Sort((x, y) => y.ToValue.CompareTo(x.ToValue));
            ArrangeRowHeight();
            return rating;
        }

        public void AddRating(RatingNote[] ratings)
        {
            _ratings.Clear();
            _ratings.Sort((x, y) => y.ToValue.CompareTo(x.ToValue));
            ArrangeRowHeight();
        }
        
        public void AddDefaultRatingNotes()
        {
            string[] noteName = { "CC+ to C", "CCC-", "CCC", "CCC+", "B-", "B2", "B1", "B+", "BB-", "BB2", "BB1", "BB+", "BBB-", "BBB", "BBB+", "AAA to A-" };
            float[] noteFromValue = { 0, 5, 10, 15, 20, 25, 30, 35, 55, 60, 65, 70, 75, 80, 85, 90 };
            float[] noteToValue = { 5, 10, 15, 20, 25, 30, 35, 55, 60, 65, 70, 75, 80, 85, 90, 100 };

            _ratings.Clear();

            for (int i = 0; i < noteName.Length; ++i)
            {
                _ratings.Add(new RatingNote { Name = noteName[i], FromValue = noteFromValue[i], ToValue = noteToValue[i] });
            }

            _ratings.Sort((x, y) => y.ToValue.CompareTo(x.ToValue));
            ArrangeRowHeight();
        }

        private void DrawRowLines(Graphics gr, Rectangle clientRect)
        {
            if (_rowLineWidth <= 0)
            {
                return;
            }

            for (int i = 1; i < _ratings.Count; ++i)
            {
                RectangleF rect = new RectangleF(clientRect.Left, clientRect.Top + i * _rowHeight + (i - 1) * _rowLineWidth, clientRect.Width, _rowLineWidth);
                gr.FillRectangle(_rowLineBrush.GetBrush(Rectangle.Round(rect)), rect);
            }
        }

        private void DrawShape(Graphics gr, Brush brush, RectangleF rect)
        {
            switch (_noteBarShape)
            {
                case NoteBarShapeType.Rectangle:
                    gr.FillRectangle(brush, rect);
                    break;
                case NoteBarShapeType.Ellipse:
                    gr.FillEllipse(brush, rect);
                    break;
                case NoteBarShapeType.RoundRectangle:
                    //gr.FillEllipse(frameBrush, rect);
                    break;
            }
        }

        private void DrawNoteBar(Graphics gr, BrushInfo brushInfo, Color frameColor, RectangleF rect)
        {
            rect.Inflate(-1, -1);

            if (frameColor != Color.Empty)
            {
                SolidBrush frameBrush = new SolidBrush(frameColor);
                DrawShape(gr, frameBrush, rect);
                rect.Inflate(-1, -1);
            }

            DrawShape(gr, brushInfo.GetBrush(Rectangle.Round(rect)), rect);
        }

        private void DrawHerePointer(Graphics gr, RectangleF rect)
        {
            rect.Inflate(-2, -2);

            float span = rect.Height / 3;
            float startX = rect.X - 2 * span - 5;
            float startY = rect.Y + span;
            float holeRadius = 3 * span / 4;

            PointF[] points = new PointF[]
            {
                        new PointF(startX, startY),
                        //new PointF(startX + span / 2, startY + span),
                        new PointF(startX + span, startY + 2 * span),
                        //new PointF(startX + 1.5f * span , startY + span),
                        new PointF(startX + 2 * span, startY)
            };

            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            path.AddArc(new RectangleF(startX, startY - span, 2 * span, 2 * span), 0, -180);
            path.AddEllipse(new RectangleF(startX + span - holeRadius / 2, startY - holeRadius / 2, holeRadius, holeRadius));
            path.CloseFigure();
            gr.FillPath(new SolidBrush(_herePointerColor), path);
            path.Dispose();
        }

        private void DrawNoteNameOrValue(Graphics gr, RatingNote note, RectangleF rect)
        {
            if ((_showNoteName || _showNoteValue) == false)
            {
                return;
            }

            string noteNameValueStr = string.Empty;

            if (_showNoteValue)
            {
                noteNameValueStr = (_showNoteName ? "   (" : string.Empty)
                                   + note.FromValue.ToString()
                                   + " - "
                                   + note.ToValue.ToString()
                                   + (_showNoteName ? ")" : string.Empty);
            }

            if (_showNoteName)
            {
                noteNameValueStr = note.Name + noteNameValueStr;
            }

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = _noteTextAlignment;

            gr.DrawString(noteNameValueStr, Font, new SolidBrush(ForeColor), rect, sf);
        }

        private void DrawCalculatedNotePointer(Graphics gr, RectangleF rect)
        {
            float radius = 6;
            RectangleF calculatedNoteRect = new RectangleF(rect.Left + rect.Width / 2 - radius / 2, rect.Top + rect.Height / 2 - radius / 2, radius, radius);
            gr.FillEllipse(new SolidBrush(_calculatedNoteColor), calculatedNoteRect);
        }

        public void ConfigureGraphics(Graphics gr)
        {
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gr.InterpolationMode = InterpolationMode.High;
            //gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        }

        private void DrawCutOff(Graphics gr, int cutOffIndex)
        {
            Rectangle clRect = ClientRectangle;
            SolidBrush cutOffbrush = new SolidBrush(_cutOffPointerColor);

            RectangleF cutOffRect = new RectangleF(clRect.Left, clRect.Top + cutOffIndex * (_rowHeight + _rowLineWidth) - _rowLineWidth, clRect.Width, _rowLineWidth);
            gr.FillRectangle(cutOffbrush, cutOffRect);

            cutOffRect.Y += _rowLineWidth;
            cutOffRect.Height = clRect.Bottom - cutOffRect.Top;
            gr.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.LightGray)), cutOffRect);

            cutOffRect.Height = _rowHeight;

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Center;
            gr.DrawString("Cut Off", new Font("Tahoma", 12f, GraphicsUnit.Pixel), cutOffbrush, cutOffRect, sf);
        }

        private void DrawFrame(Graphics gr)
        {
            if (_frameColor == Color.Empty)
            {
                return;
            }

            Rectangle clientRect = base.ClientRectangle;
            using (SolidBrush brush = new SolidBrush(_frameColor))
            {
                gr.FillRectangle(brush, clientRect.Left, clientRect.Top, clientRect.Width, 1);
                gr.FillRectangle(brush, clientRect.Right - 1, clientRect.Top, 1, clientRect.Height);
                gr.FillRectangle(brush, clientRect.Left, clientRect.Bottom - 1, clientRect.Width, 1);
                gr.FillRectangle(brush, clientRect.Left, clientRect.Top, 1, clientRect.Height);
            }
        }

        private void DrawBackGround(Graphics gr)
        {
            if (BackgroundImage != null)
            {
                return;
            }

            gr.FillRectangle(_backGroundBrush.GetBrush(ClientRectangle), ClientRectangle);
        }

        private void DrawForeGround(Graphics gr)
        {
            Rectangle clRect = ClientRectangle;
            DrawRowLines(gr, clRect);

            int currentNoteIdx = -1;
            int cutOffNoteIdx = -1;
            int calculatedNoteIdx = -1;
            short tmpNoteBarWidth = (_noteBarWidthAccordingToRowHeight ? (short)(_rowHeight - 2) : _noteBarWidth);

            for (int i = 0; i < _ratings.Count; ++i)
            {
                if (cutOffNoteIdx == -1 && _ratings[i].Equals(_cutOffNoteName, _cutOffNoteValue))
                {
                    cutOffNoteIdx = i;
                }

                if (currentNoteIdx == -1 && _ratings[i].Equals(_currentNoteName, _currentNoteValue))
                {
                    currentNoteIdx = i;
                }

                if (calculatedNoteIdx == -1 && _ratings[i].Equals(_calculatedNoteName, _calculatedNoteValue))
                {
                    calculatedNoteIdx = i;
                }

                RectangleF rect = new RectangleF
                (
                    clRect.Left + _leftIndent,
                    clRect.Top + i * (_rowHeight + _rowLineWidth),
                    clRect.Width - _leftIndent - _rightIndent - tmpNoteBarWidth,
                    _rowHeight
                );

                DrawNoteNameOrValue(gr, _ratings[i], rect);

                rect.X = rect.Right;
                rect.Width = tmpNoteBarWidth;

                if (currentNoteIdx == -1)
                {
                    DrawNoteBar(gr, _inactiveNoteBarBrush, _inactiveNoteBarFrameColor, rect);
                }
                else
                {
                    DrawNoteBar(gr, _activeNoteBarBrush, _activeNoteBarFrameColor, rect);
                }

                if (_showHerePointer && i == currentNoteIdx)
                {
                    DrawHerePointer(gr, rect);
                }

                if (i == calculatedNoteIdx)
                {
                    DrawCalculatedNotePointer(gr, rect);
                }
            }

            if (cutOffNoteIdx != -1)
            {
                DrawCutOff(gr, cutOffNoteIdx);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
            {
                AddDefaultRatingNotes();
            }

            if (_ratings.Count == 0)
            {
                return;
            }

            ConfigureGraphics(e.Graphics);
            DrawBackGround(e.Graphics);
            DrawForeGround(e.Graphics);
            DrawFrame(e.Graphics);
        }
    }
}