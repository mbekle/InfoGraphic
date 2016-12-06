namespace TestProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.igRating4 = new TestProject.IGRating();
            this.igRating3 = new TestProject.IGRating();
            this.igRating2 = new TestProject.IGRating();
            this.igRating1 = new TestProject.IGRating();
            this.igProcess2 = new TestProject.IGProcess();
            this.igProcess1 = new TestProject.IGProcess();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(158, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "agt_home.png");
            this.imageList1.Images.SetKeyName(1, "cache.png");
            this.imageList1.Images.SetKeyName(2, "kate.png");
            this.imageList1.Images.SetKeyName(3, "katomic.png");
            this.imageList1.Images.SetKeyName(4, "remote.png");
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(363, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 40);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(768, 74);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1388, 38);
            this.panel1.TabIndex = 9;
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 20;
            this.trackBar1.Location = new System.Drawing.Point(158, 120);
            this.trackBar1.Maximum = 360;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(711, 69);
            this.trackBar1.SmallChange = 5;
            this.trackBar1.TabIndex = 11;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(472, 20);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 40);
            this.button4.TabIndex = 12;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.igRating4);
            this.panel2.Controls.Add(this.igRating3);
            this.panel2.Controls.Add(this.igRating2);
            this.panel2.Controls.Add(this.igRating1);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.trackBar1);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(158, 186);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1230, 688);
            this.panel2.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(722, 410);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 178);
            this.panel3.TabIndex = 18;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(768, 278);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 65);
            this.button5.TabIndex = 14;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // igRating4
            // 
            this.igRating4.ActiveNoteBarBrush.Angle = 0F;
            this.igRating4.ActiveNoteBarBrush.FromColor = System.Drawing.Color.DodgerBlue;
            this.igRating4.ActiveNoteBarBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.igRating4.ActiveNoteBarBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating4.ActiveNoteBarFrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.igRating4.BackGroundBrush.Angle = 0F;
            this.igRating4.BackGroundBrush.FromColor = System.Drawing.Color.White;
            this.igRating4.BackGroundBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating4.BackGroundBrush.ToColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.igRating4.CalculatedNoteColor = System.Drawing.Color.White;
            this.igRating4.CalculatedNoteName = "BB2";
            this.igRating4.CalculatedNoteValue = 0F;
            this.igRating4.CurrentNoteName = "B+";
            this.igRating4.CurrentNoteValue = 40F;
            this.igRating4.CutOffNoteName = "CCC";
            this.igRating4.CutOffNoteValue = 0F;
            this.igRating4.CutOffPointerColor = System.Drawing.Color.Red;
            this.igRating4.CutOffTranslucentColor = System.Drawing.Color.LightGray;
            this.igRating4.CutOffTranslucentValue = ((byte)(125));
            this.igRating4.Font = new System.Drawing.Font("Tahoma", 8F);
            this.igRating4.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.igRating4.HerePointerColor = System.Drawing.Color.Green;
            this.igRating4.InactiveNoteBarBrush.Angle = 0F;
            this.igRating4.InactiveNoteBarBrush.FromColor = System.Drawing.Color.Gainsboro;
            this.igRating4.InactiveNoteBarBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating4.InactiveNoteBarBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating4.InactiveNoteBarFrameColor = System.Drawing.Color.White;
            this.igRating4.LeftIndent = ((short)(3));
            this.igRating4.Location = new System.Drawing.Point(930, 31);
            this.igRating4.Name = "igRating4";
            this.igRating4.NoteBarShape = TestProject.IGRating.NoteBarShapeType.Rectangle;
            this.igRating4.NoteBarShapeInflateValue = -1F;
            this.igRating4.NoteBarWidth = ((short)(25));
            this.igRating4.NoteBarWidthAccordingToRowHeight = true;
            this.igRating4.NoteTextAlignment = System.Drawing.StringAlignment.Near;
            this.igRating4.RightIndent = ((short)(3));
            this.igRating4.RowLineBrush.Angle = 0F;
            this.igRating4.RowLineBrush.FromColor = System.Drawing.Color.DimGray;
            this.igRating4.RowLineBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating4.RowLineBrush.ToColor = System.Drawing.Color.White;
            this.igRating4.RowLineWidth = ((short)(1));
            this.igRating4.ShowCutOffText = true;
            this.igRating4.ShowHerePointer = true;
            this.igRating4.ShowNoteName = true;
            this.igRating4.ShowNoteValue = false;
            this.igRating4.Size = new System.Drawing.Size(226, 626);
            this.igRating4.TabIndex = 17;
            this.igRating4.Text = "ıgRating1";
            // 
            // igRating3
            // 
            this.igRating3.ActiveNoteBarBrush.Angle = 0F;
            this.igRating3.ActiveNoteBarBrush.FromColor = System.Drawing.Color.DodgerBlue;
            this.igRating3.ActiveNoteBarBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating3.ActiveNoteBarBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating3.ActiveNoteBarFrameColor = System.Drawing.Color.White;
            this.igRating3.BackGroundBrush.Angle = 0F;
            this.igRating3.BackGroundBrush.FromColor = System.Drawing.Color.White;
            this.igRating3.BackGroundBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating3.BackGroundBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating3.CalculatedNoteColor = System.Drawing.Color.White;
            this.igRating3.CalculatedNoteName = "CCC+";
            this.igRating3.CalculatedNoteValue = 0F;
            this.igRating3.CurrentNoteName = "B-";
            this.igRating3.CurrentNoteValue = 0F;
            this.igRating3.CutOffNoteName = "";
            this.igRating3.CutOffNoteValue = 0F;
            this.igRating3.CutOffPointerColor = System.Drawing.Color.Empty;
            this.igRating3.CutOffTranslucentColor = System.Drawing.Color.LightGray;
            this.igRating3.CutOffTranslucentValue = ((byte)(125));
            this.igRating3.Font = new System.Drawing.Font("Tahoma", 8F);
            this.igRating3.FrameColor = System.Drawing.Color.DimGray;
            this.igRating3.HerePointerColor = System.Drawing.Color.Green;
            this.igRating3.InactiveNoteBarBrush.Angle = 0F;
            this.igRating3.InactiveNoteBarBrush.FromColor = System.Drawing.Color.Gainsboro;
            this.igRating3.InactiveNoteBarBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating3.InactiveNoteBarBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating3.InactiveNoteBarFrameColor = System.Drawing.Color.White;
            this.igRating3.LeftIndent = ((short)(3));
            this.igRating3.Location = new System.Drawing.Point(472, 211);
            this.igRating3.Name = "igRating3";
            this.igRating3.NoteBarShape = TestProject.IGRating.NoteBarShapeType.Rectangle;
            this.igRating3.NoteBarShapeInflateValue = -1F;
            this.igRating3.NoteBarWidth = ((short)(25));
            this.igRating3.NoteBarWidthAccordingToRowHeight = false;
            this.igRating3.NoteTextAlignment = System.Drawing.StringAlignment.Near;
            this.igRating3.RightIndent = ((short)(3));
            this.igRating3.RowLineBrush.Angle = 0F;
            this.igRating3.RowLineBrush.FromColor = System.Drawing.Color.DarkGray;
            this.igRating3.RowLineBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating3.RowLineBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating3.RowLineWidth = ((short)(1));
            this.igRating3.ShowCutOffText = true;
            this.igRating3.ShowHerePointer = true;
            this.igRating3.ShowNoteName = true;
            this.igRating3.ShowNoteValue = false;
            this.igRating3.Size = new System.Drawing.Size(187, 418);
            this.igRating3.TabIndex = 16;
            this.igRating3.Text = "ıgRating3";
            // 
            // igRating2
            // 
            this.igRating2.ActiveNoteBarBrush.Angle = 0F;
            this.igRating2.ActiveNoteBarBrush.FromColor = System.Drawing.Color.DodgerBlue;
            this.igRating2.ActiveNoteBarBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating2.ActiveNoteBarBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating2.ActiveNoteBarFrameColor = System.Drawing.Color.Empty;
            this.igRating2.BackGroundBrush.Angle = 0F;
            this.igRating2.BackGroundBrush.FromColor = System.Drawing.Color.White;
            this.igRating2.BackGroundBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating2.BackGroundBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating2.CalculatedNoteColor = System.Drawing.Color.Empty;
            this.igRating2.CalculatedNoteName = "";
            this.igRating2.CalculatedNoteValue = 0F;
            this.igRating2.CurrentNoteName = "B2";
            this.igRating2.CurrentNoteValue = 0F;
            this.igRating2.CutOffNoteName = "";
            this.igRating2.CutOffNoteValue = 0F;
            this.igRating2.CutOffPointerColor = System.Drawing.Color.Empty;
            this.igRating2.CutOffTranslucentColor = System.Drawing.Color.LightGray;
            this.igRating2.CutOffTranslucentValue = ((byte)(125));
            this.igRating2.Font = new System.Drawing.Font("Tahoma", 7F);
            this.igRating2.FrameColor = System.Drawing.Color.Empty;
            this.igRating2.HerePointerColor = System.Drawing.Color.Green;
            this.igRating2.InactiveNoteBarBrush.Angle = 0F;
            this.igRating2.InactiveNoteBarBrush.FromColor = System.Drawing.Color.Gainsboro;
            this.igRating2.InactiveNoteBarBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating2.InactiveNoteBarBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating2.InactiveNoteBarFrameColor = System.Drawing.Color.Empty;
            this.igRating2.LeftIndent = ((short)(3));
            this.igRating2.Location = new System.Drawing.Point(106, 211);
            this.igRating2.Name = "igRating2";
            this.igRating2.NoteBarShape = TestProject.IGRating.NoteBarShapeType.Rectangle;
            this.igRating2.NoteBarShapeInflateValue = -1F;
            this.igRating2.NoteBarWidth = ((short)(25));
            this.igRating2.NoteBarWidthAccordingToRowHeight = false;
            this.igRating2.NoteTextAlignment = System.Drawing.StringAlignment.Near;
            this.igRating2.RightIndent = ((short)(30));
            this.igRating2.RowLineBrush.Angle = 0F;
            this.igRating2.RowLineBrush.FromColor = System.Drawing.Color.LightGray;
            this.igRating2.RowLineBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.igRating2.RowLineBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating2.RowLineWidth = ((short)(1));
            this.igRating2.ShowCutOffText = true;
            this.igRating2.ShowHerePointer = true;
            this.igRating2.ShowNoteName = true;
            this.igRating2.ShowNoteValue = false;
            this.igRating2.Size = new System.Drawing.Size(177, 418);
            this.igRating2.TabIndex = 15;
            this.igRating2.Text = "ıgRating1";
            // 
            // igRating1
            // 
            this.igRating1.ActiveNoteBarBrush.Angle = 0F;
            this.igRating1.ActiveNoteBarBrush.FromColor = System.Drawing.Color.DodgerBlue;
            this.igRating1.ActiveNoteBarBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating1.ActiveNoteBarBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating1.ActiveNoteBarFrameColor = System.Drawing.Color.Empty;
            this.igRating1.BackGroundBrush.Angle = 0F;
            this.igRating1.BackGroundBrush.FromColor = System.Drawing.Color.White;
            this.igRating1.BackGroundBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating1.BackGroundBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating1.CalculatedNoteColor = System.Drawing.Color.Empty;
            this.igRating1.CalculatedNoteName = "";
            this.igRating1.CalculatedNoteValue = 0F;
            this.igRating1.CurrentNoteName = "BB-";
            this.igRating1.CurrentNoteValue = 0F;
            this.igRating1.CutOffNoteName = "";
            this.igRating1.CutOffNoteValue = 0F;
            this.igRating1.CutOffPointerColor = System.Drawing.Color.Empty;
            this.igRating1.CutOffTranslucentColor = System.Drawing.Color.LightGray;
            this.igRating1.CutOffTranslucentValue = ((byte)(125));
            this.igRating1.Font = new System.Drawing.Font("Tahoma", 7F);
            this.igRating1.FrameColor = System.Drawing.Color.Empty;
            this.igRating1.HerePointerColor = System.Drawing.Color.Green;
            this.igRating1.InactiveNoteBarBrush.Angle = 0F;
            this.igRating1.InactiveNoteBarBrush.FromColor = System.Drawing.Color.Gainsboro;
            this.igRating1.InactiveNoteBarBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igRating1.InactiveNoteBarBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating1.InactiveNoteBarFrameColor = System.Drawing.Color.Empty;
            this.igRating1.LeftIndent = ((short)(3));
            this.igRating1.Location = new System.Drawing.Point(282, 211);
            this.igRating1.Name = "igRating1";
            this.igRating1.NoteBarShape = TestProject.IGRating.NoteBarShapeType.Rectangle;
            this.igRating1.NoteBarShapeInflateValue = -1F;
            this.igRating1.NoteBarWidth = ((short)(25));
            this.igRating1.NoteBarWidthAccordingToRowHeight = false;
            this.igRating1.NoteTextAlignment = System.Drawing.StringAlignment.Near;
            this.igRating1.RightIndent = ((short)(20));
            this.igRating1.RowLineBrush.Angle = 0F;
            this.igRating1.RowLineBrush.FromColor = System.Drawing.Color.LightGray;
            this.igRating1.RowLineBrush.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.igRating1.RowLineBrush.ToColor = System.Drawing.Color.Empty;
            this.igRating1.RowLineWidth = ((short)(1));
            this.igRating1.ShowCutOffText = true;
            this.igRating1.ShowHerePointer = true;
            this.igRating1.ShowNoteName = false;
            this.igRating1.ShowNoteValue = false;
            this.igRating1.Size = new System.Drawing.Size(113, 418);
            this.igRating1.TabIndex = 15;
            this.igRating1.Text = "ıgRating1";
            // 
            // igProcess2
            // 
            this.igProcess2.BackColor = System.Drawing.SystemColors.Control;
            this.igProcess2.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igProcess2.BackColorTo = System.Drawing.Color.Empty;
            this.igProcess2.Dock = System.Windows.Forms.DockStyle.Left;
            this.igProcess2.IsVertical = true;
            this.igProcess2.ItemAlignment = System.Drawing.StringAlignment.Near;
            this.igProcess2.ItemHeight = ((short)(70));
            this.igProcess2.ItemRoundingShape = TestProject.ItemRoundingShapeType.Rectangle;
            this.igProcess2.ItemRoundWidth = ((short)(30));
            this.igProcess2.ItemSeperatorWidth = ((short)(-7));
            this.igProcess2.ItemTextLeftRightEmptyWidth = ((short)(40));
            this.igProcess2.ItemTriangleWidth = ((short)(10));
            this.igProcess2.Location = new System.Drawing.Point(0, 186);
            this.igProcess2.Name = "igProcess2";
            this.igProcess2.Shrinker = true;
            this.igProcess2.Size = new System.Drawing.Size(158, 688);
            this.igProcess2.TabIndex = 10;
            // 
            // igProcess1
            // 
            this.igProcess1.BackColor = System.Drawing.SystemColors.Control;
            this.igProcess1.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igProcess1.BackColorTo = System.Drawing.Color.Empty;
            this.igProcess1.Dock = System.Windows.Forms.DockStyle.Top;
            this.igProcess1.IsVertical = false;
            this.igProcess1.ItemAlignment = System.Drawing.StringAlignment.Center;
            this.igProcess1.ItemHeight = ((short)(35));
            this.igProcess1.ItemRoundingShape = TestProject.ItemRoundingShapeType.Circular;
            this.igProcess1.ItemRoundWidth = ((short)(15));
            this.igProcess1.ItemSeperatorWidth = ((short)(-17));
            this.igProcess1.ItemTextLeftRightEmptyWidth = ((short)(50));
            this.igProcess1.ItemTriangleWidth = ((short)(20));
            this.igProcess1.Location = new System.Drawing.Point(0, 0);
            this.igProcess1.Name = "igProcess1";
            this.igProcess1.Shrinker = true;
            this.igProcess1.Size = new System.Drawing.Size(1388, 148);
            this.igProcess1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1388, 874);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.igProcess2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.igProcess1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button2;
        private IGProcess igProcess1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private IGProcess igProcess2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button5;
        private IGRating igRating1;
        private IGRating igRating2;
        private IGRating igRating3;
        private IGRating igRating4;
        private System.Windows.Forms.Panel panel3;
    }
}