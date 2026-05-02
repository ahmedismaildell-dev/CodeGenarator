namespace CodeGenarator
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ctrlEnumCode = new CodeGenarator.ctrlCodeFormat();
            this.ctrlDtoCode = new CodeGenarator.ctrlCodeFormat();
            this.ctrlSqlQuery = new CodeGenarator.ctrlCodeFormat();
            this.ctrlBusinessCode = new CodeGenarator.ctrlCodeFormat();
            this.ctrlDataAccessCode = new CodeGenarator.ctrlCodeFormat();
            this.ctrlSpQuery = new CodeGenarator.ctrlCodeFormat();
            this.fastColoredTextBox2 = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlEnumCode
            // 
            this.ctrlEnumCode.BackColor = System.Drawing.Color.White;
            this.ctrlEnumCode.CodeText = "";
            this.ctrlEnumCode.CodeTitle = null;
            this.ctrlEnumCode.ctrlVisiable = true;
            this.ctrlEnumCode.Location = new System.Drawing.Point(731, 682);
            this.ctrlEnumCode.Name = "ctrlEnumCode";
            this.ctrlEnumCode.Size = new System.Drawing.Size(719, 163);
            this.ctrlEnumCode.TabIndex = 6;
            // 
            // ctrlDtoCode
            // 
            this.ctrlDtoCode.BackColor = System.Drawing.Color.White;
            this.ctrlDtoCode.CodeText = "fastColoredTextBox1";
            this.ctrlDtoCode.CodeTitle = null;
            this.ctrlDtoCode.ctrlVisiable = true;
            this.ctrlDtoCode.Location = new System.Drawing.Point(6, 682);
            this.ctrlDtoCode.Name = "ctrlDtoCode";
            this.ctrlDtoCode.Size = new System.Drawing.Size(725, 163);
            this.ctrlDtoCode.TabIndex = 5;
            // 
            // ctrlSqlQuery
            // 
            this.ctrlSqlQuery.BackColor = System.Drawing.Color.White;
            this.ctrlSqlQuery.CodeText = "\"\"";
            this.ctrlSqlQuery.CodeTitle = null;
            this.ctrlSqlQuery.ctrlVisiable = true;
            this.ctrlSqlQuery.Location = new System.Drawing.Point(7, 13);
            this.ctrlSqlQuery.Name = "ctrlSqlQuery";
            this.ctrlSqlQuery.Size = new System.Drawing.Size(720, 339);
            this.ctrlSqlQuery.TabIndex = 4;
            this.ctrlSqlQuery.OnExecute += new System.Action(this.ctrlSqlQuery_OnExecute_1);
            this.ctrlSqlQuery.OnNew += new System.Action(this.ctrlSqlQuery_OnNew);
            this.ctrlSqlQuery.Load += new System.EventHandler(this.ctrlSqlQuery_Load);
            // 
            // ctrlBusinessCode
            // 
            this.ctrlBusinessCode.BackColor = System.Drawing.Color.White;
            this.ctrlBusinessCode.CodeText = "fastColoredTextBox1";
            this.ctrlBusinessCode.CodeTitle = null;
            this.ctrlBusinessCode.ctrlVisiable = true;
            this.ctrlBusinessCode.Location = new System.Drawing.Point(732, 349);
            this.ctrlBusinessCode.Name = "ctrlBusinessCode";
            this.ctrlBusinessCode.Size = new System.Drawing.Size(720, 354);
            this.ctrlBusinessCode.TabIndex = 3;
            // 
            // ctrlDataAccessCode
            // 
            this.ctrlDataAccessCode.BackColor = System.Drawing.Color.White;
            this.ctrlDataAccessCode.CodeText = "fastColoredTextBox1";
            this.ctrlDataAccessCode.CodeTitle = null;
            this.ctrlDataAccessCode.ctrlVisiable = true;
            this.ctrlDataAccessCode.Location = new System.Drawing.Point(5, 349);
            this.ctrlDataAccessCode.Name = "ctrlDataAccessCode";
            this.ctrlDataAccessCode.Size = new System.Drawing.Size(719, 339);
            this.ctrlDataAccessCode.TabIndex = 2;
            // 
            // ctrlSpQuery
            // 
            this.ctrlSpQuery.BackColor = System.Drawing.Color.White;
            this.ctrlSpQuery.CodeText = "fastColoredTextBox1";
            this.ctrlSpQuery.CodeTitle = null;
            this.ctrlSpQuery.ctrlVisiable = true;
            this.ctrlSpQuery.Location = new System.Drawing.Point(730, 13);
            this.ctrlSpQuery.Name = "ctrlSpQuery";
            this.ctrlSpQuery.Size = new System.Drawing.Size(722, 339);
            this.ctrlSpQuery.TabIndex = 1;
            // 
            // fastColoredTextBox2
            // 
            this.fastColoredTextBox2.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBox2.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.fastColoredTextBox2.BackBrush = null;
            this.fastColoredTextBox2.CharCnWidth = 16;
            this.fastColoredTextBox2.CharHeight = 14;
            this.fastColoredTextBox2.CharWidth = 8;
            this.fastColoredTextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox2.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox2.IsReplaceMode = false;
            this.fastColoredTextBox2.Location = new System.Drawing.Point(505, 208);
            this.fastColoredTextBox2.Name = "fastColoredTextBox2";
            this.fastColoredTextBox2.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox2.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox2.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox2.ServiceColors")));
            this.fastColoredTextBox2.Size = new System.Drawing.Size(8, 8);
            this.fastColoredTextBox2.TabIndex = 8;
            this.fastColoredTextBox2.Text = "fastColoredTextBox2";
            this.fastColoredTextBox2.Zoom = 100;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1455, 857);
            this.Controls.Add(this.fastColoredTextBox2);
            this.Controls.Add(this.ctrlEnumCode);
            this.Controls.Add(this.ctrlDtoCode);
            this.Controls.Add(this.ctrlSqlQuery);
            this.Controls.Add(this.ctrlBusinessCode);
            this.Controls.Add(this.ctrlDataAccessCode);
            this.Controls.Add(this.ctrlSpQuery);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ctrlCodeFormat ctrlSpQuery;
        private ctrlCodeFormat ctrlBusinessCode;
        private ctrlCodeFormat ctrlDataAccessCode;
        private ctrlCodeFormat ctrlSqlQuery;
        private ctrlCodeFormat ctrlDtoCode;
        private ctrlCodeFormat ctrlEnumCode;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox2;
    }
}