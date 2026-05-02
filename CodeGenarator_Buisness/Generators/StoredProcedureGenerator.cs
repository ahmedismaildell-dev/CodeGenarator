using CodeGenarator_Buisness.Models;
using CodeGenarator_Buisness.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CodeGenarator_Buisness.Generators
{
    public class StoredProcedureGenerator
    {
        private static CodeGenerationContext _context;


        public static string Generate(string sql)

        {

             CreateTableParser.Parse(sql);
            _context = clsGenaric.codeGenerations;




            if (_context.table == null || (_context.table.Columns.Count == 0 && _context.table.TableName == ""))
                {
                    throw new ArgumentException("Invalid SQL syntax. Unable to parse table model.", nameof(sql));

                }

                var sb = new StringBuilder();


            foreach (var proc in _context.Procedures)
            {

                if (proc.Name.StartsWith($"SP_Get{_context.Entity}"))
                {
                    sb.AppendLine(GenerateGetById(proc));
                    sb.AppendLine("GO\n\n");
                }



                else if (proc.Name.StartsWith("SP_Add"))
                {
                    sb.AppendLine(GenerateAdd(proc));
                    sb.AppendLine("GO\n \n");

                }

                else if (proc.Name.StartsWith("SP_Update"))
                {
                    sb.AppendLine(GenerateUpdate(proc));
                    sb.AppendLine("GO\n \n");

                }
                else if (proc.Name.StartsWith("SP_GetAll"))
                {
                    sb.AppendLine(GenerateGetAll(proc));
                    sb.AppendLine("GO\n \n");

                }
                else if (proc.Name.StartsWith("SP_Delete"))
                {
                    sb.AppendLine(GenerateDelete(proc));
                    sb.AppendLine("GO\n \n");

                }
            }

           

            return sb.ToString();
        }
        
        

        // ================= GET BY ID =================
        private static string GenerateGetById(ProcedureModel procedure)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"CREATE PROCEDURE [dbo].[{procedure.Name}]");
            sb.AppendLine($"@{_context.IdColumn.Name} INT");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    SET NOCOUNT ON;");
            sb.AppendLine("    SELECT");

            sb.AppendLine(string.Join(",\n",
                _context.table.Columns.Select(c => $"        {c.Name}")
            ));

            sb.AppendLine($"    FROM {_context.table        .TableName}");
            sb.AppendLine($"    WHERE {_context.IdColumn.Name} = @{_context.IdColumn.Name}");
            sb.AppendLine("END");

            return sb.ToString();
        }

        // ================= ADD NEW =================
        private static string GenerateAdd(ProcedureModel procedure)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"CREATE PROCEDURE [dbo].[{procedure.Name}]");

            sb.AppendLine(string.Join(",\n",
                        _context.table.Columns
                    .Where(c => !c.IsIdentity)
                    .Select(c => $"@{c.Name} {c.SqlType}")
            ));

            sb.AppendLine($",@{_context.IdColumn.Name} INT OUTPUT");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");

            sb.AppendLine($"    INSERT INTO {_context.table.TableName}");
            sb.AppendLine("    (");

            sb.AppendLine(string.Join(",\n",
                _context.table.Columns
                    .Where(c => !c.IsIdentity)
                    .Select(c => $"        {c.Name}")
            ));

            sb.AppendLine("    )");
            sb.AppendLine("    VALUES");
            sb.AppendLine("    (");

            sb.AppendLine(string.Join(",\n",
                _context.table.Columns
                    .Where(c => !c.IsIdentity)
                    .Select(c => $"        @{c.Name}")
            ));

            sb.AppendLine("    );");
            sb.AppendLine($"    SET @{_context.IdColumn.Name} = SCOPE_IDENTITY();");
            sb.AppendLine("END");

            return sb.ToString();
        }

        // ================= UPDATE =================
        private static string GenerateUpdate(ProcedureModel procedure)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"CREATE PROCEDURE [dbo].[{procedure.Name}]");

            sb.AppendLine(string.Join(",\n",
                _context.table.Columns.Select(c => $"@{c.Name} {c.SqlType}")
            ));

            sb.AppendLine(",@IsUpdate BIT OUTPUT");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");

            sb.AppendLine($"    UPDATE {_context.table.TableName}");
            sb.AppendLine("    SET");

            sb.AppendLine(string.Join(",\n",
                _context.table.Columns
                    .Where(c => c.Name != _context.IdColumn.Name)
                    .Select(c => $"        {c.Name} = @{c.Name}")
            ));

            sb.AppendLine($"    WHERE {_context.IdColumn.Name} = @{_context.IdColumn.Name}");
            sb.AppendLine("    IF @@ROWCOUNT > 0");
            sb.AppendLine("        SET @IsUpdate = 1;");
            sb.AppendLine("    ELSE");
            sb.AppendLine("        SET @IsUpdate = 0;");
            sb.AppendLine("END");

            return sb.ToString();
        }
        private static string GenerateDelete(ProcedureModel procedure)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"CREATE PROCEDURE [dbo].[{procedure.Name}]");

            sb.AppendLine($"@{_context.IdColumn.Name} INT");


            sb.AppendLine(",@IsDeleted BIT OUTPUT");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine($"    SET NOCOUNT ON;");
            sb.AppendLine($"");
            sb.AppendLine($"    DELETE FROM {_context.table.TableName}");
            
            sb.AppendLine($"    WHERE {_context.IdColumn.Name} = @{_context.IdColumn.Name}");
            sb.AppendLine("    IF @@ROWCOUNT > 0");
            sb.AppendLine("        SET @IsDeleted = 1;");
            sb.AppendLine("    ELSE");
            sb.AppendLine("        SET @IsDeleted = 0;");
            sb.AppendLine("END");

            return sb.ToString();
        }
        private static string GenerateGetAll(ProcedureModel procedure)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"CREATE PROCEDURE [dbo].[{procedure.Name}]");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine($"");
            sb.AppendLine($"    SELECT * FROM {_context.table.TableName}");

          
            sb.AppendLine("END");

            return sb.ToString();
        }
    }

}
