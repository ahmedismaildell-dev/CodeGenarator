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

namespace CodeGenarator
{
    public partial class ftxtData : Form
    {
        private string _enumName;
      
        public ftxtData(string enumName)
        {
            InitializeComponent();
            _enumName=enumName;
        }
        public delegate void DataBackEventHandler(object sender, enumModel enModel);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

     
        private void frm_Load(object sender, EventArgs e)
        {
            lblQuestion.Text = $"Please Enter {_enumName} code ?";

            ctrlCodeFormat1.CodeTitle = "C_Sharp";
            ctrlCodeFormat1.SelectC_SharpFormateWithExecute();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
           
        }

        private void ctrlCodeFormat1_OnExecute()
        {
            enumModel enumModel=null;
            try
            {
               enumModel = enumParser.Parse(ctrlCodeFormat1.CodeText);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
                if (enumModel != null)
                    DataBack?.Invoke(this, enumModel);
                this.Close();

           
        }
    }
}
