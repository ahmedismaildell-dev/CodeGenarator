using CodeGenarator_Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Parser
{
    public static class TabelModelParser
    {
        public static DtoModel Parse(TableModel tableModel, List<enumModel> enumModels)
        {
             DtoModel dtoModel = new DtoModel();
            string entity = tableModel.TableName.Substring(0, tableModel.TableName.Length - 1);

            dtoModel.DtoName = entity+"DTO";


            foreach(ColumnModel column in tableModel.Columns)
            {
                DtoColumnModel dtoColumn = new DtoColumnModel();
                dtoColumn.Name = column.Name;

                if(column .SqlType== "tinyint")
                {
                 //   var enumModel = enumModels.FirstOrDefault(e => e.ModelName == column.Name);
                  //  dtoColumn.DataType = enumModel.enumName;
                }

                if(column.SqlType != "tinyint")
                    dtoColumn.DataType = clsGenaric.MapType(column.SqlType);

                dtoColumn.IsNullable= column.IsNullable;
                dtoModel.DtoColumns.Add(dtoColumn);

            }
            return dtoModel;
        }
    }

}
