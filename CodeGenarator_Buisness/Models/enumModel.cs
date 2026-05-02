using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Models
{
    public class enumModel
    {
        public string enumName
        {
            set;get;
        }
        public List<EnumItemModel> Items = new List<EnumItemModel>();
    }
}
