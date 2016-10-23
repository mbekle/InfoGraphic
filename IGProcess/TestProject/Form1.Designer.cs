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
            this.igProcess1 = new TestProject.IGProcess();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 230);
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
            this.button2.Location = new System.Drawing.Point(129, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // igProcess1
            // 
            this.igProcess1.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.igProcess1.BackColorTo = System.Drawing.Color.Empty;
            this.igProcess1.Dock = System.Windows.Forms.DockStyle.Top;
            this.igProcess1.ItemAlignment = System.Drawing.StringAlignment.Center;
            this.igProcess1.ItemHeight = ((short)(35));
            this.igProcess1.ItemRoundingShape = TestProject.ItemRoundingShapeType.Circular;
            this.igProcess1.ItemRoundWidth = ((short)(15));
            this.igProcess1.ItemSeperatorWidth = ((short)(-17));
            this.igProcess1.ItemTriangleWidth = ((short)(20));
            this.igProcess1.Location = new System.Drawing.Point(0, 0);
            this.igProcess1.Name = "igProcess1";
            this.igProcess1.Size = new System.Drawing.Size(1481, 103);
            this.igProcess1.TabIndex = 5;
            this.igProcess1.Text = "ıgProcess1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1481, 384);
            this.Controls.Add(this.igProcess1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button2;
        private IGProcess igProcess1;
    }
}