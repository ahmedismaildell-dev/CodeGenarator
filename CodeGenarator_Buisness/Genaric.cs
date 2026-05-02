using CodeGenarator_Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness
{
    public static class clsGenaric
    {
     
        public static DtoModel dto;
       
        public static CodeGenerationContext codeGenerations;

        public static string GetDataType(DtoColumnModel dtoColumn)
        {
            if (dtoColumn.IsNullable&& dtoColumn.DataType!="string")
                return $"{dtoColumn.DataType}?";
            else
                return $"{dtoColumn.DataType} ";

        }
        public static string GetEntityFronTabelName(string Name)
        {
            return Name.Substring(0, Name.Length - 1);
        }
       
        private static readonly Dictionary<string, string> TypeMap =
   new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    { "INT", "int" },
    { "BIGINT", "long" },
    { "SMALLINT", "short" },
    { "TINYINT", "byte" },

    { "DECIMAL", "decimal" },
    { "NUMERIC", "decimal" },
    { "MONEY", "decimal" },

    { "FLOAT", "double" },
    { "REAL", "float" },

    { "VARCHAR", "string" },
    { "NVARCHAR", "string" },

    { "DATE", "DateTime" },
    { "DATETIME", "DateTime" },

    { "BIT", "bool" },
    { "UNIQUEIDENTIFIER", "Guid" }
};
        public static string MapType(string sqlType)
        {
            return TypeMap.TryGetValue(sqlType.Trim(), out var type)
                ? type
                : "string";
        }
        public static  void Restart()
        {
            codeGenerations = null;
        }
    }
    

}
