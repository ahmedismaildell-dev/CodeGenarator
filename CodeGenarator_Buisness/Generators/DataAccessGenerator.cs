using CodeGenarator_Buisness.Models;
using CodeGenarator_Buisness.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Generators
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;
    using System.Text;

    namespace CodeGenarator_Buisness.Generators
    {
        public static class DataAccessGenerator
        {
            private static CodeGenerationContext context;
            
            private static DtoModel _dto;
            private static readonly Dictionary<string, string> ReaderMap =
    new Dictionary<string, string>
{
    { "int", "GetInt32" },
    { "long", "GetInt64" },
    { "short", "GetInt16" },
    { "byte", "GetByte" },
    { "bool", "GetBoolean" },
    { "string", "GetString" },
    { "decimal", "GetDecimal" },
    { "double", "GetDouble" },
    { "float", "GetFloat" },
    { "DateTime", "GetDateTime" },
    { "Guid", "GetGuid" }
};

            public static string Print()
            {
                context = clsGenaric.codeGenerations;

               
                    _dto = DtoGenerator.GenerateDTO();
                DtoGenerator.CurrentDtoModel = _dto;


                var sb = new StringBuilder();

              
                sb.AppendLine($"public static class cls{context.Entity}Data");
                sb.AppendLine("{");

                foreach (var proc in context.Procedures)
                {
                    if (proc.Name.StartsWith($"SP_Get{context.Entity}"))
                    {

                        foreach (DtoColumnModel rfTabel in context.FindByColumns)
                        {
                            sb.AppendLine(GenerateGetBy(proc, rfTabel));

                        }
                    }
                    else if (proc.Name.StartsWith("SP_Add"))
                        sb.AppendLine(GenerateAdd(proc));
                    else if (proc.Name.StartsWith("SP_Update"))
                        sb.AppendLine(GenerateUpdate(proc));
                    else if (proc.Name.StartsWith("SP_GetAll"))
                        sb.AppendLine(GenerateGetAll(proc));
                    else if (proc.Name.StartsWith("SP_Delete"))
                        sb.AppendLine(GenerateDelete(proc));
                }

                

                sb.AppendLine("}");
                return sb.ToString();
            }

            // ================= GET BY ID =================
            // ================= GET BY ID =================
            private static string GenerateGetBy(ProcedureModel proc,DtoColumnModel dtoColumn)
            {
                var sb = new StringBuilder();

                sb.AppendLine($"public static {_dto.DtoName} GetInfoBy{dtoColumn.Name}({dtoColumn.DataType} {dtoColumn.Name})");
                sb.AppendLine("{");
                sb.AppendLine("    try");
                sb.AppendLine("    {");
                sb.AppendLine("        using (SqlConnection connection =");
                sb.AppendLine("               new SqlConnection(clsDataAccessSettings.ConnectionString))");
                sb.AppendLine($"        using (SqlCommand command =");
                sb.AppendLine($"               new SqlCommand(\"{proc.Name}\", connection))");
                sb.AppendLine("        {");
                sb.AppendLine("            command.CommandType = CommandType.StoredProcedure;");
                sb.AppendLine();
                sb.AppendLine($"            command.Parameters.AddWithValue(\"@{dtoColumn.Name}\", {dtoColumn.Name});");
                sb.AppendLine();
                sb.AppendLine("            connection.Open();");
                sb.AppendLine();
                sb.AppendLine("            using (SqlDataReader reader = command.ExecuteReader())");
                sb.AppendLine("            {");
                sb.AppendLine("                if (reader.Read())");
                sb.AppendLine("                {");
                sb.AppendLine($"                    return new {_dto.DtoName}");
                sb.AppendLine("                    (");

                for (int i = 0; i < _dto.DtoColumns.Count; i++)
                {
                    var col = _dto.DtoColumns[i];
                    string line = GenerateReaderWithNull(col);

                    if (i < _dto.DtoColumns.Count - 1)
                        line += ",";

                    sb.AppendLine("                        " + line);
                }

                sb.AppendLine("                    );");
                sb.AppendLine("                }");
                sb.AppendLine("            }");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("    catch (Exception ex)");
                sb.AppendLine("    {");
                sb.AppendLine("        clsDataAccessSettings.LogException(");
                sb.AppendLine($"            $\"Error in {proc.Name}: {{ex}}\",");
                sb.AppendLine("            EventLogEntryType.Error");
                sb.AppendLine("        );");
                sb.AppendLine("    }");
                sb.AppendLine();
                sb.AppendLine("    return null;");
                sb.AppendLine("}");

                return sb.ToString();
            }


            // ================= ADD =================
            // ================= ADD =================
            private static string GenerateAdd(ProcedureModel proc)
            {
                var sb = new StringBuilder();

                sb.AppendLine($"public static int AddNew{context.Entity}({_dto.DtoName} dto)");
                sb.AppendLine("{");
                sb.AppendLine("    int id = -1;");
                sb.AppendLine();
                sb.AppendLine("    try");
                sb.AppendLine("    {");
                sb.AppendLine("        using (SqlConnection connection =");
                sb.AppendLine("               new SqlConnection(clsDataAccessSettings.ConnectionString))");
                sb.AppendLine($"        using (SqlCommand command =");
                sb.AppendLine($"               new SqlCommand(\"{proc.Name}\", connection))");
                sb.AppendLine("        {");
                sb.AppendLine("            command.CommandType = CommandType.StoredProcedure;");
                sb.AppendLine();

                foreach (var col in _dto.DtoColumns.Where(c => c.Name != context.IdColumn.Name))
                {
                    sb.AppendLine($"            command.Parameters.AddWithValue(\"@{col.Name}\", dto.{col.Name});");
                }

                sb.AppendLine();
                sb.AppendLine($"            SqlParameter output = new SqlParameter(\"@{context.IdColumn}\", SqlDbType.Int)");
                sb.AppendLine("            {");
                sb.AppendLine("                Direction = ParameterDirection.Output");
                sb.AppendLine("            };");
                sb.AppendLine();
                sb.AppendLine("            command.Parameters.Add(output);");
                sb.AppendLine();
                sb.AppendLine("            connection.Open();");
                sb.AppendLine("            command.ExecuteNonQuery();");
                sb.AppendLine();
                sb.AppendLine("            id = Convert.ToInt32(output.Value);");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("    catch (Exception ex)");
                sb.AppendLine("    {");
                sb.AppendLine("        clsDataAccessSettings.LogException(");
                sb.AppendLine($"            $\"Error in {proc.Name}: {{ex}}\",");
                sb.AppendLine("            EventLogEntryType.Error");
                sb.AppendLine("        );");
                sb.AppendLine("    }");
                sb.AppendLine();
                sb.AppendLine("    return id;");
                sb.AppendLine("}");

                return sb.ToString();
            }


            // ================= UPDATE =================
            // ================= UPDATE =================
            private static string GenerateUpdate(ProcedureModel proc)
            {
                var sb = new StringBuilder();

                sb.AppendLine($"public static bool Update{context.Entity}({_dto.DtoName} dto)");
                sb.AppendLine("{");
                sb.AppendLine("    bool isUpdated = false;");
                sb.AppendLine();
                sb.AppendLine("    using (SqlConnection connection =");
                sb.AppendLine("           new SqlConnection(clsDataAccessSettings.ConnectionString))");
                sb.AppendLine($"    using (SqlCommand command =");
                sb.AppendLine($"           new SqlCommand(\"{proc.Name}\", connection))");
                sb.AppendLine("    {");
                sb.AppendLine("        command.CommandType = CommandType.StoredProcedure;");
                sb.AppendLine();

                foreach (var col in _dto.DtoColumns)
                {
                    sb.AppendLine($"        command.Parameters.AddWithValue(\"@{col.Name}\", dto.{col.Name});");
                }

                sb.AppendLine();
                sb.AppendLine("        SqlParameter output = new SqlParameter(\"@IsUpdate\", SqlDbType.Bit)");
                sb.AppendLine("        {");
                sb.AppendLine("            Direction = ParameterDirection.Output");
                sb.AppendLine("        };");
                sb.AppendLine();
                sb.AppendLine("        command.Parameters.Add(output);");
                sb.AppendLine();
                sb.AppendLine("        connection.Open();");
                sb.AppendLine("        command.ExecuteNonQuery();");
                sb.AppendLine();
                sb.AppendLine("        isUpdated = Convert.ToBoolean(output.Value);");
                sb.AppendLine("    }");
                sb.AppendLine();
                sb.AppendLine("    return isUpdated;");
                sb.AppendLine("}");

                return sb.ToString();
            }

            // ================= Get All=================
            // ================= GET ALL =================
            // ================= GET ALL =================
            private static string GenerateGetAll(ProcedureModel proc)
            {
                var sb = new StringBuilder();

                sb.AppendLine($"public static List<{_dto.DtoName}> GetAll{context.Entity}()");
                sb.AppendLine("{");
                sb.AppendLine($"    List<{_dto.DtoName}> list = new List<{_dto.DtoName}>();");
                sb.AppendLine();
                sb.AppendLine("    try");
                sb.AppendLine("    {");
                sb.AppendLine("        using (SqlConnection connection =");
                sb.AppendLine("               new SqlConnection(clsDataAccessSettings.ConnectionString))");
                sb.AppendLine($"        using (SqlCommand command =");
                sb.AppendLine($"               new SqlCommand(\"{proc.Name}\", connection))");
                sb.AppendLine("        {");
                sb.AppendLine("            command.CommandType = CommandType.StoredProcedure;");
                sb.AppendLine();
                sb.AppendLine("            connection.Open();");
                sb.AppendLine();
                sb.AppendLine("            using (SqlDataReader reader = command.ExecuteReader())");
                sb.AppendLine("            {");
                sb.AppendLine("                while (reader.Read())");
                sb.AppendLine("                {");
                sb.AppendLine($"                    list.Add(new {_dto.DtoName}");
                sb.AppendLine("                    (");

                for (int i = 0; i < _dto.DtoColumns.Count; i++)
                {
                    var col = _dto.DtoColumns[i];
                    string line = GenerateReaderWithNull(col);

                    if (i < _dto.DtoColumns.Count - 1)
                        line += ",";

                    sb.AppendLine("                        " + line);
                }

                sb.AppendLine("                    ));");
                sb.AppendLine("                }");
                sb.AppendLine("            }");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("    catch (Exception ex)");
                sb.AppendLine("    {");
                sb.AppendLine("        clsDataAccessSettings.LogException(");
                sb.AppendLine($"            $\"Error in {proc.Name}: {{ex}}\",");
                sb.AppendLine("            EventLogEntryType.Error");
                sb.AppendLine("        );");
                sb.AppendLine("    }");
                sb.AppendLine();
                sb.AppendLine("    return list;");
                sb.AppendLine("}");

                return sb.ToString();
            }

            // ================= DELETE =================
            private static string GenerateDelete(ProcedureModel proc)
            {
                var sb = new StringBuilder();

                sb.AppendLine($"public static bool Delete{context.Entity}(int id)");
                sb.AppendLine("{");
                sb.AppendLine("    bool isDeleted = false;");
                sb.AppendLine();
                sb.AppendLine("    using (SqlConnection connection =");
                sb.AppendLine("           new SqlConnection(clsDataAccessSettings.ConnectionString))");
                sb.AppendLine($"    using (SqlCommand command =");
                sb.AppendLine($"           new SqlCommand(\"{proc.Name}\", connection))");
                sb.AppendLine("    {");
                sb.AppendLine("        command.CommandType = CommandType.StoredProcedure;");
                sb.AppendLine();
                sb.AppendLine($"        command.Parameters.AddWithValue(\"@{context.IdColumn}\", id);");
                sb.AppendLine();
                sb.AppendLine("        SqlParameter output = new SqlParameter(\"@IsDeleted\", SqlDbType.Bit)");
                sb.AppendLine("        {");
                sb.AppendLine("            Direction = ParameterDirection.Output");
                sb.AppendLine("        };");
                sb.AppendLine();
                sb.AppendLine("        command.Parameters.Add(output);");
                sb.AppendLine();
                sb.AppendLine("        connection.Open();");
                sb.AppendLine("        command.ExecuteNonQuery();");
                sb.AppendLine();
                sb.AppendLine("        isDeleted = Convert.ToBoolean(output.Value);");
                sb.AppendLine("    }");
                sb.AppendLine();
                sb.AppendLine("    return isDeleted;");
                sb.AppendLine("}");

                return sb.ToString();
            }


            // ================= Reader Helpers =================
            private static string GenerateReader(DtoColumnModel col)
            {
                string name = col.Name;
                string ordinal = $"reader.GetOrdinal(\"{name}\")";

                // لو النوع معروف
                if (ReaderMap.TryGetValue(col.DataType, out var method))
                {
                    return $"reader.{method}({ordinal})";
                }

                // fallback → enum (TINYINT)
                return $"({col.DataType})reader.GetByte({ordinal})";
            }





            private static string GenerateReaderWithNull(DtoColumnModel col)
            {
                string name = col.Name;
                string ordinal = $"reader.GetOrdinal(\"{name}\")";

                if (!col.IsNullable)
                    return GenerateReader(col);

                // string و byte[]
                if (col.DataType.Equals("string", StringComparison.OrdinalIgnoreCase))
                    return $"reader.IsDBNull({ordinal}) ? null : reader.GetString({ordinal})";

                if (col.DataType.Equals("byte[]", StringComparison.OrdinalIgnoreCase))
                    return $"reader.IsDBNull({ordinal}) ?({col.DataType}?) null : (byte[])reader[\"{name}\"]";

                // باقي الأنواع + enum
                return $"reader.IsDBNull({ordinal}) ?({col.DataType}?) null : {GenerateReader(col)}";
            }

        }
    }

}
