using CodeGenarator_Buisness.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Generators
{
    public class DtoGenerator
    {
        public static DtoModel CurrentDtoModel { get; set; }
        private static CodeGenerationContext _context;
        public static DtoModel GenerateDTO()
        {
            _context = clsGenaric.codeGenerations;
            DtoModel dtoModel = new DtoModel();

            dtoModel.DtoName = _context.Entity + "DTO";

            foreach (var col in _context.table.Columns)
            {
               
                dtoModel.DtoColumns.Add(GenarateCoulmnDto( col));
            }
            CurrentDtoModel=new DtoModel();
            CurrentDtoModel = dtoModel; 
            return dtoModel;




    }
        public static DtoColumnModel GenarateCoulmnDto(ColumnModel col)
        {
            DtoColumnModel dtoColumn = new DtoColumnModel();
            dtoColumn.Name = col.Name;

            // tinyint → enum
            if (col.SqlType.Equals("tinyint", StringComparison.OrdinalIgnoreCase))
            {
                var enumModel = _context.Enums
                    .FirstOrDefault(e => GetNameColumnFromEnumModel(e.enumName).Contains(col.Name));

                if (enumModel == null)
                    throw new Exception($"Enum not found for column {col.Name}");


                dtoColumn.DataType = enumModel.enumName;

            }

            if (col.IsNullable&& !col.IsIdentity)
                dtoColumn.IsNullable = true;

            if (!col.SqlType.Equals("tinyint", StringComparison.OrdinalIgnoreCase))
                dtoColumn.DataType = clsGenaric.MapType(col.SqlType);
            return dtoColumn;   
        }
        public static string PrintDto()
        {
            clsGenaric.dto = DtoGenerator.GenerateDTO();

            var dto = clsGenaric.dto;

            var sb = new StringBuilder();

            sb.AppendLine($"public class {dto.DtoName}");
            sb.AppendLine("{");

            foreach (var item in dto.DtoColumns)
            {
                // tinyint → enum

                sb.AppendLine(
                    $"   public {item.DataType} {item.Name} {{ get; set; }}"
                );

            }
            sb.AppendLine("}");
            return sb.ToString();

        }
        private static string GetNameColumnFromEnumModel(string enumName)
        {
            return enumName.StartsWith("en")
                        ? enumName.Substring(2)
                        : enumName;

        }



    }
}
