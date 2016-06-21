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
            this.processInfoGraphic1 = new TestProject.ProcessInfoGraphic();
            this.SuspendLayout();
            // 
            // RotateButton
            // 
            this.RotateButton.Location = new System.Drawing.Point(787, 543);
            this.RotateButton.Name = "RotateButton";
            this.RotateButton.Size = new System.Drawing.Size(102, 44);
            this.RotateButton.TabIndex = 1;
            this.RotateButton.Text = "Rotate";
            this.RotateButton.UseVisualStyleBackColor = true;
            this.RotateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // processInfoGraphic1
            // 
            this.processInfoGraphic1.Dock = System.Windows.Forms.DockStyle.Top;
            this.processInfoGraphic1.Location = new System.Drawing.Point(0, 0);
            this.processInfoGraphic1.Name = "processInfoGraphic1";
            this.processInfoGraphic1.Size = new System.Drawing.Size(949, 499);
            this.processInfoGraphic1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 824);
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
    }
}

