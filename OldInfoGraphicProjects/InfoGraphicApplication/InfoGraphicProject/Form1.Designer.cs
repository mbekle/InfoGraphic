namespace InfoGraphicProject
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
            this.ınfoGraphic1 = new InfoGraphicProject.InfoGraphic();
            this.SuspendLayout();
            // 
            // ınfoGraphic1
            // 
            this.ınfoGraphic1.AnimationInterval = 50D;
            this.ınfoGraphic1.AnimationMethod = InfoGraphicProject.InfoGraphic.AnimationMethods.Default;
            this.ınfoGraphic1.BottomIndent = 0;
            this.ınfoGraphic1.IsVertical = false;
            this.ınfoGraphic1.LeftIndent = 0;
            this.ınfoGraphic1.Location = new System.Drawing.Point(42, 110);
            this.ınfoGraphic1.Name = "ınfoGraphic1";
            this.ınfoGraphic1.RightIndent = 0;
            this.ınfoGraphic1.Size = new System.Drawing.Size(1093, 288);
            this.ınfoGraphic1.TabIndex = 0;
            this.ınfoGraphic1.TopIndent = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 615);
            this.Controls.Add(this.ınfoGraphic1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private InfoGraphic ınfoGraphic1;
    }
}

