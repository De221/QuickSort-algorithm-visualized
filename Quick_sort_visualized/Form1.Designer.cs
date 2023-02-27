
namespace Quick_sort_visualized
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
            this.arrayBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.label0 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // arrayBox
            // 
            this.arrayBox.Location = new System.Drawing.Point(12, 157);
            this.arrayBox.Name = "arrayBox";
            this.arrayBox.Size = new System.Drawing.Size(390, 20);
            this.arrayBox.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(408, 157);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(114, 20);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(117, 141);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(35, 13);
            this.label0.TabIndex = 2;
            this.label0.Text = "label0";
            this.label0.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 194);
            this.Controls.Add(this.label0);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.arrayBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "QuickSort";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox arrayBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label0;
    }
}

