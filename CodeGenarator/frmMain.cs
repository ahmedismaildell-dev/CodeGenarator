using CodeGenarator_Buisness;
using CodeGenarator_Buisness.Generators;
using CodeGenarator_Buisness.Generators.CodeGenarator_Buisness.Generators;
using CodeGenarator_Buisness.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenarator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private enumModel _completeEnumModel;
        private CodeGenerationContext _context;

        private void _SettingCtrCodeFormat()
        {
            ctrlSqlQuery.CodeTitle = "Sql Creation Code";
            ctrlSpQuery.CodeTitle = "SP Generation Code";
            ctrlDataAccessCode.CodeTitle = "DataAccess Generation Code";
            ctrlBusinessCode.CodeTitle = "Class Generation Code";
            ctrlDtoCode.CodeTitle = "DTO Generation Code";
            ctrlEnumCode.CodeTitle = "Enum Generation Code";

            ctrlSqlQuery.SelectSQLFormateWithExecute();
            ctrlSpQuery.SelectSQLFormateWithoutExecute();
            ctrlDataAccessCode.SelectC_SharpFormateWithoutExecute();
            ctrlBusinessCode.SelectC_SharpFormateWithoutExecute();
            ctrlDtoCode.SelectC_SharpFormateWithoutExecute();
            ctrlDtoCode.ftxtCodeSize = Tuple.Create(695, 100);
            ctrlEnumCode.ftxtCodeSize = Tuple.Create(695, 100);

            ctrlEnumCode.SelectC_SharpFormateWithoutExecute();

            ctrlSqlQuery.CodeText = "";
            ctrlDataAccessCode.CodeText = "";
            ctrlBusinessCode.CodeText = "";
            ctrlDtoCode.CodeText = "";
            ctrlEnumCode.CodeText = "";
            ctrlSpQuery.CodeText = "";

            ctrlSqlQuery.txtFocus();

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            _SettingCtrCodeFormat();
            
        }

      
        private void DataBackEvent(object sender, enumModel enModel)
        {
            // Handle the data received
            _completeEnumModel = enModel;
        }

        private void ctrlSqlQuery_OnExecute_1()
        {

            if (ctrlSqlQuery.CodeText == "")
            {
                MessageBox.Show("An empty query cannot be executed ,Please Enter the Query", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ctrlSpQuery.CodeText = StoredProcedureGenerator.Generate(ctrlSqlQuery.CodeText);
                _context = clsGenaric.codeGenerations;

                ctrlEnumCode.Visible = false;
                if (_context.Enums.Count>0)
                {
                     for (int i = 0; i < _context.Enums.Count; i++)
                {
                    _completeEnumModel = null;

                    var currentEnum = _context.Enums[i];
                    using (ftxtData frm = new ftxtData(currentEnum.enumName))
                    {
                        frm.DataBack += DataBackEvent;
                        frm.ShowDialog();
                    }
                        if (_completeEnumModel != null)
                        _context.Enums[i] = _completeEnumModel;
                }
                    clsGenaric.codeGenerations = _context;

                    if (_completeEnumModel == null)
                    throw new Exception("can not Genarator DataAccess AND Business Because Invalid enum , try anothor ");
                    ctrlEnumCode.Visible = true;
                    ctrlEnumCode.CodeText= BusinessGenerator.GenaratEunmsFile();

                }
                ctrlDtoCode.CodeText= BusinessGenerator.GenarateDtoFile();
                ctrlDataAccessCode.CodeText = DataAccessGenerator.Print();
                ctrlBusinessCode.CodeText = BusinessGenerator.GenarateClassFile();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void _AddNew()
        {
            ctrlSqlQuery.CodeText = "";
            ctrlSpQuery.CodeText = "";
            ctrlDataAccessCode.CodeText = "";
            ctrlBusinessCode.CodeText = "";
            ctrlDtoCode.CodeText = "";

            ctrlEnumCode.Visible = false;
            ctrlSqlQuery.CodeFocus();
        }
        private void ctrlSqlQuery_OnNew()
        {
            _AddNew();
        }

        private void ctrlSqlQuery_Load(object sender, EventArgs e)
        {

        }
    }
}
