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
            this.RotateButton = new System.Windows.Forms.Button();
            this.CenterShapesButton = new System.Windows.Forms.Button();
            this.VerticalButton = new System.Windows.Forms.Button();
            this.igProcess1 = new TestProject.IGProcess();
            this.processInfoGraphic1 = new TestProject.ProcessInfoGraphic();
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
            // igProcess1
            // 
            this.igProcess1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.igProcess1.ItemHeight = 30;
            this.igProcess1.Location = new System.Drawing.Point(43, 38);
            this.igProcess1.Name = "igProcess1";
            this.igProcess1.Size = new System.Drawing.Size(678, 97);
            this.igProcess1.TabIndex = 3;
            // 
            // processInfoGraphic1
            // 
            this.processInfoGraphic1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.processInfoGraphic1.ForeColor = System.Drawing.Color.Coral;
            this.processInfoGraphic1.IsVertical = false;
            this.processInfoGraphic1.Location = new System.Drawing.Point(404, 510);
            this.processInfoGraphic1.Name = "processInfoGraphic1";
            this.processInfoGraphic1.Size = new System.Drawing.Size(502, 338);
            this.processInfoGraphic1.TabIndex = 0;
            this.processInfoGraphic1.Resize += new System.EventHandler(this.processInfoGraphic1_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 874);
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
    }
}

