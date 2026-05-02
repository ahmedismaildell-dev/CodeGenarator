namespace CodeGenarator
{
    partial class ftxtData
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
            this.lblQuestion = new System.Windows.Forms.Label();
            this.ctrlCodeFormat1 = new CodeGenarator.ctrlCodeFormat();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.ForeColor = System.Drawing.Color.Red;
            this.lblQuestion.Location = new System.Drawing.Point(47, 49);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(117, 20);
            this.lblQuestion.TabIndex = 14;
            this.lblQuestion.Text = "Please Enter ";
            // 
            // ctrlCodeFormat1
            // 
            this.ctrlCodeFormat1.BackColor = System.Drawing.Color.White;
            this.ctrlCodeFormat1.CodeText = null;
            this.ctrlCodeFormat1.CodeTitle = null;
            this.ctrlCodeFormat1.Location = new System.Drawing.Point(12, 72);
            this.ctrlCodeFormat1.Name = "ctrlCodeFormat1";
            this.ctrlCodeFormat1.Size = new System.Drawing.Size(679, 366);
            this.ctrlCodeFormat1.TabIndex = 15;
            this.ctrlCodeFormat1.OnExecute += new System.Action(this.ctrlCodeFormat1_OnExecute);
            // 
            // ftxtData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(721, 458);
            this.Controls.Add(this.ctrlCodeFormat1);
            this.Controls.Add(this.lblQuestion);
            this.Name = "ftxtData";
            this.Text = "frm";
            this.Load += new System.EventHandler(this.frm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblQuestion;
        private ctrlCodeFormat ctrlCodeFormat1;
    }
}