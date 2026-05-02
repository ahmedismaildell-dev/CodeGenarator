using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Models
{
    public class RefrenseTableModel
    {
        public string ClassName {  get; set; }  
        public string EntityName {  get; set; }
        public DtoColumnModel DtoColumn { get; set; } 

    }
}
