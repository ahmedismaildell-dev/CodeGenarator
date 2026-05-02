using CodeGenarator_Buisness.Generators;
using CodeGenarator_Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeGenarator_Buisness
{
    public class clsPrinter
    {
       
        public static string PrintDataAccess()
        {
            var sb = new StringBuilder();
            sb.AppendLine(DtoGenerator.PrintDto());
            return sb.ToString();   
        }

    }
}
