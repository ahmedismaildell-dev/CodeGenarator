using CodeGenarator_Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CodeGenarator_Buisness.Generators
{
    public class BusinessGenerator
    {
        private static DtoModel _dto;
        private static CodeGenerationContext _context;
        public static string GenarateClassFile()
        {
            _dto = DtoGenerator.CurrentDtoModel;
            _context = clsGenaric.codeGenerations;


            return _GenerateClass();   



        }
        public static string GenarateDtoFile()
        {
            _dto = DtoGenerator.GenerateDTO();
            _context = clsGenaric.codeGenerations;
            return _GenarateDTO();
        }
        public static string GenaratEunmsFile()
        {
           
            _context = clsGenaric.codeGenerations;
            return _GenarateEnums();
        }

        private static string _GenerateClass()
        {
            var dto = $"{_context.Entity[0]}DTO";
            var sb = new StringBuilder();

            sb.AppendLine($"public class cls{_context.Entity}");
            sb.AppendLine("{");

            sb.AppendLine("    public enMode Mode = enMode.AddNew;");
            sb.AppendLine();

            // ================= DTO Property =================
            sb.AppendLine($"    public {_dto.DtoName} {dto}");
            sb.AppendLine("    {");
            sb.AppendLine("        get");
            sb.AppendLine("        {");
            sb.AppendLine($"            return new {_dto.DtoName}(");

            for (int i = 0; i < _dto.DtoColumns.Count; i++)
            {
                var col = _dto.DtoColumns[i];
                string line = $"                this.{col.Name}";
                if (i < _dto.DtoColumns.Count - 1)
                    line += ",";
                sb.AppendLine(line);
            }

            sb.AppendLine("            );");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine();

            // ================= Properties =================
            sb.AppendLine(_GenarateProperties());
            sb.AppendLine();

            // ================= Referance =================
            foreach (RefrenseTableModel rfTabel in _context.ReferenceTables)
            {
                sb.AppendLine(
                    $"public {rfTabel.ClassName}" +
                    $"{(rfTabel.DtoColumn.IsNullable? "?" : "")} " +
                    $"{rfTabel.EntityName}Info {{ get; set; }}"
                );
            }

           
            // ================= Constructor =================
            sb.AppendLine($"    public cls{_context.Entity}({_dto.DtoName} dto, enMode mode = enMode.AddNew)");
            sb.AppendLine("    {");

            foreach (var col in _dto.DtoColumns)
            {
                sb.AppendLine($"        this.{col.Name} = dto.{col.Name};");
            }

            foreach (RefrenseTableModel rfTabel in _context.ReferenceTables)
            {
              
                    sb.AppendLine(
                        $"this.{rfTabel.EntityName}Info =" +
                        $"{rfTabel.ClassName}.FindInfoBy{rfTabel.DtoColumn.Name}({rfTabel.DtoColumn.Name})" + $"{(rfTabel.DtoColumn.IsNullable ? "?" : "")} ;" 
                );
            }
            sb.AppendLine();
            sb.AppendLine("        Mode = mode;");
            sb.AppendLine("    }");

      
          
            sb.AppendLine();

            // ================= Add =================
            sb.AppendLine($"    private bool AddNew()");
            sb.AppendLine("    {");
            sb.AppendLine($"        this.{_context.IdColumn.Name} = cls{_context.Entity}Data.AddNew{_context.Entity}({dto});");
            sb.AppendLine($"        return this.{_context.IdColumn.Name} != -1;");
            sb.AppendLine("    }");
            sb.AppendLine();

            // ================= Update =================
            sb.AppendLine($"    private bool Update()");
            sb.AppendLine("    {");
            sb.AppendLine($"        return cls{_context.Entity}Data.Update{_context.Entity}({dto});");
            sb.AppendLine("    }");
            sb.AppendLine();

            foreach (DtoColumnModel rfTabel in _context.FindByColumns)
            {
                // ================= Find =================
                sb.AppendLine($"    public static cls{_context.Entity} FindInfoBy{rfTabel.Name}({rfTabel.DataType} {rfTabel.Name})");
                sb.AppendLine("    {");
                sb.AppendLine($"        var dto = cls{_context.Entity}Data.GetInfoBy{rfTabel.Name}({rfTabel.Name});");
                sb.AppendLine($"        return dto == null ? null : new cls{_context.Entity}(dto, enMode.Update);");
                sb.AppendLine("    }");
                sb.AppendLine();
            }

               

            // ================= GetALL =================
            sb.AppendLine($"    public static List<{_dto.DtoName}> GetAll{_context.Entity}()");
            sb.AppendLine("    {");
            sb.AppendLine($"        return cls{_context.Entity}Data.GetAll{_context.Entity}();");
            sb.AppendLine("    }");
            sb.AppendLine();

            // ================= Delete =================
            sb.AppendLine($"    public bool Delete{_context.Entity}()");
            sb.AppendLine("    {");
            sb.AppendLine($"        return cls{_context.Entity}Data.Delete{_context.Entity}(this.{_context.IdColumn.Name});");
            sb.AppendLine("    }");
            sb.AppendLine();

            // ================= Save =================
            sb.AppendLine("    public bool Save()");
            sb.AppendLine("    {");
            sb.AppendLine("        if (Mode == enMode.AddNew)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (AddNew())");
            sb.AppendLine("            {");
            sb.AppendLine("                Mode = enMode.Update;");
            sb.AppendLine("                return true;");
            sb.AppendLine("            }");
            sb.AppendLine("            return false;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        return Update();");
            sb.AppendLine("    }");
            sb.AppendLine();

            

            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string _GenarateProperties()
        {
            var sb = new StringBuilder();
            // Properties
            foreach (var col in DtoGenerator.CurrentDtoModel.DtoColumns)
            {

                sb.AppendLine($"public {clsGenaric.GetDataType(col)} {col.Name}  {{ get; set; }} ");
            }
           return sb.ToString();
        }
        private static string _GenarateDTO()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"public class {_dto.DtoName}");
            sb.AppendLine("{");

            // ================= Properties =================
            sb.AppendLine(_GenarateProperties());
            sb.AppendLine();

            // ================= Constructor =================
            sb.AppendLine($"    public {_dto.DtoName}(");

            for (int i = 0; i < _dto.DtoColumns.Count; i++)
            {
                var col = _dto.DtoColumns[i];
                string line = $"        {clsGenaric.GetDataType(col)} {col.Name}";
                if (i < _dto.DtoColumns.Count - 1)
                    line += ",";
                sb.AppendLine(line);
            }

            sb.AppendLine("    )");
            sb.AppendLine("    {");

            foreach (var col in _dto.DtoColumns)
            {
                sb.AppendLine($"        this.{col.Name} = {col.Name};");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string _GenarateEnums()
        {
            var sb = new StringBuilder();

            foreach (var item in _context.Enums)
            {
                sb.AppendLine(_GenarateOneEnum(item));
                sb.AppendLine(); // سطر فاضي بين كل enum
            }

            return sb.ToString();
        }

        private static string _GenarateOneEnum(enumModel enumModel)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"public enum {enumModel.enumName}");
            sb.AppendLine("{");

            for (int i = 0; i < enumModel.Items.Count; i++)
            {
                var item = enumModel.Items[i];
                string line = $"    {item.Name} = {item.Value}";

                if (i < enumModel.Items.Count - 1)
                    line += ",";

                sb.AppendLine(line);
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

    }

}
