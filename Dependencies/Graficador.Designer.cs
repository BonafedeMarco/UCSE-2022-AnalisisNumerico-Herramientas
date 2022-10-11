
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
            this.pictureBoxContainer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxAxis
            // 
            this.pictureBoxAxis.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxAxis.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAxis.Name = "pictureBoxAxis";
            this.pictureBoxAxis.Size = new System.Drawing.Size(1000, 1000);
            this.pictureBoxAxis.TabIndex = 1;
            this.pictureBoxAxis.TabStop = false;
            // 
            // pictureBoxContainer
            // 
            this.pictureBoxContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxContainer.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxContainer.Name = "pictureBoxContainer";
            this.pictureBoxContainer.Size = new System.Drawing.Size(1000, 1000);
            this.pictureBoxContainer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxContainer.TabIndex = 2;
            this.pictureBoxContainer.TabStop = false;
            // 
            // Graficador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pictureBoxContainer);
            this.Controls.Add(this.pictureBoxAxis);
            this.Name = "Graficador";
            this.Size = new System.Drawing.Size(1003, 1003);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxAxis;
        private System.Windows.Forms.PictureBox pictureBoxContainer;
    }
}
