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
            this.igProcess2 = new TestProject.IGProcess();
            this.igProcess1 = new TestProject.IGProcess();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(157, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 40);
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
            this.button2.Location = new System.Drawing.Point(260, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(363, 106);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 40);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(768, 160);
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
            this.trackBar1.Location = new System.Drawing.Point(157, 206);
            this.trackBar1.Maximum = 360;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(711, 69);
            this.trackBar1.SmallChange = 5;
            this.trackBar1.TabIndex = 11;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(472, 106);
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
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.trackBar1);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(157, 186);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1231, 903);
            this.panel2.TabIndex = 13;
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
            this.igProcess2.Size = new System.Drawing.Size(157, 903);
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
            this.ClientSize = new System.Drawing.Size(1388, 1089);
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
    }
}