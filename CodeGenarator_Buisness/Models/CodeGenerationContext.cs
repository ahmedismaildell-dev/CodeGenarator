using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Models
{
    public class CodeGenerationContext
    {
        public TableModel table {  get; set; }  
        public string Entity { get; set; }
        public  DtoColumnModel IdColumn { get; set; }
        public List<DtoColumnModel> FindByColumns { get; set; } = new List<DtoColumnModel>();
        public List<enumModel> Enums { get; set; } = new List<enumModel>();
        public List<ProcedureModel> Procedures { get; set; } = new List<ProcedureModel>();
        public List<RefrenseTableModel> ReferenceTables { get; set; } = new List<RefrenseTableModel>();
    }
}
