using CodeGenarator.Properties;
using CodeGenarator_Buisness;
using CodeGenarator_Buisness.Generators;
using CodeGenarator_Buisness.Models;
using CodeGenarator_Buisness.Parser;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CodeGenarator.ctrlCodeFormat;

namespace CodeGenarator
{
    public partial class ctrlCodeFormat : UserControl
    {
        private string _CodeText;
        public enum enFormatType { SQL=0,C_Sharp=1}


        public string CodeText
        {
            get => ftxtDataAccess.Text;
            set
            {
                if (ftxtDataAccess != null)
                    ftxtDataAccess.Text = value ?? string.Empty;
            }
        }
        private bool _txtFocus;
        public void txtFocus()
        {


            ftxtDataAccess.Focus();
            ftxtDataAccess.Select();    
        }

        private string _CodeTitle;

        public string CodeTitle
        {
            get
            {
                return _CodeTitle;
            }
            set
            {
                _CodeTitle = value;
                groupBox3.Text = _CodeTitle;
            }
        }

        private bool _ctrlVisiable = true;

        public bool ctrlVisiable 
        {
            get
            {
                return _ctrlVisiable;
            }
            set
            {
                _ctrlVisiable = value;
                ftxtDataAccess.Visible = _ctrlVisiable;
            }
        }
        private Tuple<int, int> _ftxtCodeSize;

        public Tuple<int, int> ftxtCodeSize
        {
            get
            {
                return _ftxtCodeSize;
            }
            set
            {
                _ftxtCodeSize = value;
                ftxtDataAccess.Size = new Size(value.Item1 , value.Item2);
                groupBox3.Size = new Size(value.Item1 +15,value.Item2 + 60);
            }
        }

        public void CodeFocus()
        {
            ftxtDataAccess.Focus();
        }


        // Define a custom event handler delegate with parameters
        public event Action OnExecute;
        public event Action OnNew;

        // Create a protected method to raise the event with a parameter
        private readonly TextStyle KeywordStyle =
 new TextStyle(new SolidBrush(Color.FromArgb(86, 156, 214)), null, FontStyle.Regular); // أزرق

        private readonly TextStyle TypeStyle =
            new TextStyle(new SolidBrush(Color.FromArgb(78, 201, 176)), null, FontStyle.Regular); // أخضر مزرق

        private readonly TextStyle MethodStyle =
            new TextStyle(new SolidBrush(Color.FromArgb(220, 220, 170)), null, FontStyle.Regular); // أصفر فاتح

        private readonly TextStyle StringStyle =
            new TextStyle(new SolidBrush(Color.FromArgb(214, 157, 133)), null, FontStyle.Regular); // برتقالي

        private readonly TextStyle CommentStyle =
            new TextStyle(new SolidBrush(Color.FromArgb(87, 166, 74)), null, FontStyle.Italic); // أخضر

        private enFormatType _FormatType;


        public void SelectSQLFormateWithExecute()
        {
            _FormatType = enFormatType.SQL;

            btnCopayDataAccess.Visible = true;
            btnExecute.Visible = true;
            btnNew.Visible = true;

            pbFormatType.Image = Resources.SQL;
            btnExecute.BackgroundImage = Resources.ExecuteSQL;
            btnCopayDataAccess.BackgroundImage = Resources.CopaySQL;
            SettingBox();

        }
        public void SelectSQLFormateWithoutExecute()
        {
            _FormatType = enFormatType.SQL;


            btnCopayDataAccess.Visible = true;
            btnExecute.Visible = false;
            btnNew.Visible = false;

            pbFormatType.Image = Resources.SQL;
            btnCopayDataAccess.BackgroundImage = Resources.CopaySQL;

            SettingBox();

        } 
        private void SettingExecuteSql()
        {
            
        }
        public void SelectC_SharpFormateWithExecute()
        {
            _FormatType = enFormatType.C_Sharp;


            btnCopayDataAccess.Visible = true;
            btnExecute.Visible = true;
            btnNew.Visible = true;

            pbFormatType.Image = Resources.C_Sharp;
            btnExecute.BackgroundImage = Resources.ExecuteC_Sharp;

            btnCopayDataAccess.BackgroundImage = Resources.CopayC_Sharp;

            SettingBox();

        }
       
        public void SelectC_SharpFormateWithoutExecute()
        {
            _FormatType = enFormatType.C_Sharp;

            btnCopayDataAccess.Visible = true;
            btnExecute.Visible = false;
            btnNew.Visible = false;

            pbFormatType.BackgroundImage = Resources.C_Sharp;
            btnCopayDataAccess.BackgroundImage = Resources.CopayC_Sharp;

            SettingBox();

        }



        public ctrlCodeFormat()
        {
            InitializeComponent();
        }
       
        private bool _isEditorConfigured;

        private void SettingBox()
        {
            if (_isEditorConfigured)
                return;

            if (_FormatType == enFormatType.C_Sharp)
                ConfigureCSharpEditor(ftxtDataAccess);
            else
                ConfigureSQLEditor();

            _isEditorConfigured = true;
        }

        private void ctrlCodeFormat_Load(object sender, EventArgs e)
        {

        }
        private void ConfigureSQLEditor()
        {
            ftxtDataAccess.Language = Language.SQL;
            ftxtDataAccess.AutoIndent = false;
            ftxtDataAccess.Font = new Font("Consolas", 11);

        }

        private void ConfigureCSharpEditor(FastColoredTextBox editor)
        {
            editor.Language = Language.CSharp;
            editor.Font = new Font("Consolas", 11);

            editor.BackColor = Color.FromArgb(30, 30, 30);
            editor.ForeColor = Color.FromArgb(220, 220, 220);

            editor.LineNumberColor = Color.FromArgb(110, 110, 110);
            editor.IndentBackColor = Color.FromArgb(40, 40, 40);
            editor.CurrentLineColor = Color.FromArgb(45, 45, 45);
            editor.SelectionColor = Color.FromArgb(60, 120, 200);

            editor.WordWrap = false;
            editor.AutoIndent = true;
            editor.ShowFoldingLines = true;

            // ربط الـ Syntax Highlighting
            editor.TextChanged -= Editor_TextChanged; // أمان
            editor.TextChanged += Editor_TextChanged;
        }
        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(StyleIndex.All);

            // 🔵 Keywords
            e.ChangedRange.SetStyle(
                KeywordStyle,
                @"\b(public|private|protected|internal|static|void|return|new|using|namespace|try|catch|finally|if|else|while|for|foreach|true|false|null)\b"
            );

            // 🟢 Types (List, SqlCommand, int, string...)
            e.ChangedRange.SetStyle(
                TypeStyle,
                @"\b(List|SqlConnection|SqlCommand|SqlDataReader|int|string|bool|float|double|decimal|var)\b"
            );

            // 🟡 Method names
            e.ChangedRange.SetStyle(
                MethodStyle,
                @"\b[A-Za-z_][A-Za-z0-9_]*\s*(?=\()"
            );

            // 🟠 Strings
            e.ChangedRange.SetStyle(
                StringStyle,
                @"""([^""\\]|\\.)*"""
            );

            // 🟢 Comments
            e.ChangedRange.SetStyle(
                CommentStyle,
                @"//.*$|/\*[\s\S]*?\*/",
                RegexOptions.Multiline
            );
        }
       

        private void btnExecute_Click(object sender, EventArgs e)
        {
            clsGenaric.Restart();

            OnExecute?.Invoke();
        }

        private void btnCopayDataAccess_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ftxtDataAccess.Text))
            {
                Clipboard.SetText(ftxtDataAccess.Text);
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            clsGenaric.Restart();   
            OnNew?.Invoke();

        }

        private void btnPast_Click(object sender, EventArgs e)
        {
            ftxtDataAccess.Text = "";
            ftxtDataAccess.Text= Clipboard.GetText();   
        }
    }
}

