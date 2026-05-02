namespace CodeGenarator
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
            this.ctrlCodeFormat1 = new CodeGenarator.ctrlCodeFormat();
            this.SuspendLayout();
            // 
            // ctrlCodeFormat1
            // 
            this.ctrlCodeFormat1.BackColor = System.Drawing.Color.White;
            this.ctrlCodeFormat1.CodeText = "FRVefe";
            this.ctrlCodeFormat1.CodeTitle = null;
            this.ctrlCodeFormat1.ctrlVisiable = true;
            this.ctrlCodeFormat1.ftxtCodeSize = null;
            this.ctrlCodeFormat1.Location = new System.Drawing.Point(36, 46);
            this.ctrlCodeFormat1.Name = "ctrlCodeFormat1";
            this.ctrlCodeFormat1.Size = new System.Drawing.Size(725, 163);
            this.ctrlCodeFormat1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ctrlCodeFormat1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlCodeFormat ctrlCodeFormat1;
    }
}