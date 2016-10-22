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
            this.RotateButton = new System.Windows.Forms.Button();
            this.CenterShapesButton = new System.Windows.Forms.Button();
            this.VerticalButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.igProcess1 = new TestProject.IGProcess();
            this.processInfoGraphic1 = new TestProject.ProcessInfoGraphic();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RotateButton
            // 
            this.RotateButton.Location = new System.Drawing.Point(26, 754);
            this.RotateButton.Name = "RotateButton";
            this.RotateButton.Size = new System.Drawing.Size(102, 45);
            this.RotateButton.TabIndex = 1;
            this.RotateButton.Text = "Rotate";
            this.RotateButton.UseVisualStyleBackColor = true;
            this.RotateButton.Click += new System.EventHandler(this.RotateButton_Click);
            // 
            // CenterShapesButton
            // 
            this.CenterShapesButton.Location = new System.Drawing.Point(26, 805);
            this.CenterShapesButton.Name = "CenterShapesButton";
            this.CenterShapesButton.Size = new System.Drawing.Size(102, 52);
            this.CenterShapesButton.TabIndex = 2;
            this.CenterShapesButton.Tag = "0";
            this.CenterShapesButton.Text = "Center Shapes";
            this.CenterShapesButton.UseVisualStyleBackColor = true;
            this.CenterShapesButton.Click += new System.EventHandler(this.CenterShapesButton_Click);
            // 
            // VerticalButton
            // 
            this.VerticalButton.Location = new System.Drawing.Point(134, 805);
            this.VerticalButton.Name = "VerticalButton";
            this.VerticalButton.Size = new System.Drawing.Size(102, 52);
            this.VerticalButton.TabIndex = 2;
            this.VerticalButton.Tag = "0";
            this.VerticalButton.Text = "Vertical";
            this.VerticalButton.UseVisualStyleBackColor = true;
            this.VerticalButton.Click += new System.EventHandler(this.VerticalButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 197);
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
            // igProcess1
            // 
            this.igProcess1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.igProcess1.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igProcess1.BackColorTo = System.Drawing.Color.Empty;
            this.igProcess1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("igProcess1.BackgroundImage")));
            this.igProcess1.Dock = System.Windows.Forms.DockStyle.Top;
            this.igProcess1.ItemAlignment = System.Drawing.StringAlignment.Center;
            this.igProcess1.ItemHeight = ((short)(35));
            this.igProcess1.ItemRoundingShape = TestProject.ItemRoundingShapeType.Circular;
            this.igProcess1.ItemRoundWidth = ((short)(15));
            this.igProcess1.ItemSeperatorWidth = ((short)(-17));
            this.igProcess1.ItemTriangleWidth = ((short)(20));
            this.igProcess1.Location = new System.Drawing.Point(0, 0);
            this.igProcess1.Name = "igProcess1";
            this.igProcess1.Size = new System.Drawing.Size(1482, 98);
            this.igProcess1.TabIndex = 3;
            // 
            // processInfoGraphic1
            // 
            this.processInfoGraphic1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.processInfoGraphic1.ForeColor = System.Drawing.Color.Coral;
            this.processInfoGraphic1.IsVertical = false;
            this.processInfoGraphic1.Location = new System.Drawing.Point(358, 601);
            this.processInfoGraphic1.Name = "processInfoGraphic1";
            this.processInfoGraphic1.Size = new System.Drawing.Size(581, 222);
            this.processInfoGraphic1.TabIndex = 0;
            this.processInfoGraphic1.Visible = false;
            this.processInfoGraphic1.Resize += new System.EventHandler(this.processInfoGraphic1_Resize);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 874);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.igProcess1);
            this.Controls.Add(this.VerticalButton);
            this.Controls.Add(this.CenterShapesButton);
            this.Controls.Add(this.RotateButton);
            this.Controls.Add(this.processInfoGraphic1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ProcessInfoGraphic processInfoGraphic1;
        private System.Windows.Forms.Button RotateButton;
        private System.Windows.Forms.Button CenterShapesButton;
        private System.Windows.Forms.Button VerticalButton;
        private IGProcess igProcess1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button2;
    }
}