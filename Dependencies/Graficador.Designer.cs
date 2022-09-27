
namespace Analisis_Numerico
{
    partial class Graficador
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxAxis = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAxis)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxAxis
            // 
            this.pictureBoxAxis.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxAxis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxAxis.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAxis.Name = "pictureBoxAxis";
            this.pictureBoxAxis.Size = new System.Drawing.Size(600, 600);
            this.pictureBoxAxis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxAxis.TabIndex = 1;
            this.pictureBoxAxis.TabStop = false;
            // 
            // Graficador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pictureBoxAxis);
            this.Name = "Graficador";
            this.Size = new System.Drawing.Size(600, 600);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAxis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxAxis;
    }
}
