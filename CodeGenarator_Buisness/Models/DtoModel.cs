using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Models
{
    public class DtoModel
    {
        public string DtoName { get; set; }
        public List<DtoColumnModel> DtoColumns { get; set; } = new List<DtoColumnModel>();
    }
}
