using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Models
{
    public class TableModel
    {
        public string TableName { get; set; }
        public List<ColumnModel> Columns { get; set; } = new List<ColumnModel>();
    }
}
