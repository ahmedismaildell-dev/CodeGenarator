using CodeGenarator_Buisness.Generators;
using CodeGenarator_Buisness.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Parser
{
    public class CreateTableParser
    {
        private static CodeGenerationContext context;
        private static List<RefrenseTableModel> refrenseTabels = new List<RefrenseTableModel>();
        private static TableModel table ;
        private static List<DtoColumnModel> _FindByColumns = new List<DtoColumnModel>();

        private static void _RestartDefault()
        {
            context = new CodeGenerationContext();
            refrenseTabels = new List<RefrenseTableModel>();
            _FindByColumns = new List<DtoColumnModel>();
            table = new TableModel(); ;
        }
        public static void Parse(string sql)
        {


            _RestartDefault();



            CreateTableObject(sql);
            GenarateContext();

        }
        private static void DetectCustomReferance(string definition, ColumnModel col)
        {
            if(col.IsPrimaryKey)
            {
                _FindByColumns.Add(new DtoColumnModel
                {
                    Name = col.Name,
                    IsNullable = col.IsNullable,
                    DataType = clsGenaric.MapType(col.SqlType),
                });
                return;
            }
            if (!Regex.IsMatch(definition, @"\bFIND\s+BY\b", RegexOptions.IgnoreCase))
                return;

            bool alreadyExists = refrenseTabels.Any(r =>
                 r.DtoColumn.Name.Equals(col.Name, StringComparison.OrdinalIgnoreCase));

            if (alreadyExists)
               return;
            _FindByColumns.Add(new DtoColumnModel
            {
                Name = col.Name,
                IsNullable = col.IsNullable,
                DataType = clsGenaric.MapType(col.SqlType),
            });

        }
        private static void CreateTableObject(string sql)
        {
            var cleanSQL = RemoveSqlComments(sql);
            // ===== Table Name =====
            var tableMatch = Regex.Match(
     cleanSQL,
     @"CREATE\s+TABLE\s+(?:\[[^\]]+\]\.)?\[?(\w+)\]?\s*\(([\s\S]*)\)",
     RegexOptions.IgnoreCase
 );




            if (!tableMatch.Success)
                throw new Exception("CREATE TABLE not found");

            // اسم الجدول
            table.TableName = tableMatch.Groups[1].Value;

            // جسم الجدول
            string tableBody = tableMatch.Groups[2].Value;
            CreateColumsObject(tableBody);


        }
        private static void CreateColumsObject(string tableBody)
        {
            var columnMatches = Regex.Matches(
    tableBody,
    @"^\s*(?!CONSTRAINT\b|FOREIGN\b|PRIMARY\b|REFERENCES\b)\[?(\w+)\]?\s+\[?(\w+)\]?(?:\(([0-9,\s]+)\))?(.*?)(?:,|$)",
    RegexOptions.Multiline | RegexOptions.IgnoreCase
);

            foreach (Match match in columnMatches)
            {
                var col = new ColumnModel
                {
                    Name = match.Groups[1].Value,
                    SqlType = match.Groups[2].Value.ToUpper(),
                };

                string definition = match.Groups[4].Value;

                // Identity
                col.IsIdentity = Regex.IsMatch(definition, @"\bIDENTITY\b", RegexOptions.IgnoreCase);

                // Primary Key
                col.IsPrimaryKey = Regex.IsMatch(definition, @"\bPRIMARY\s+KEY\b", RegexOptions.IgnoreCase);

                // Nullable
                col.IsNullable =
                    !Regex.IsMatch(definition, @"\bNOT\s+NULL\b", RegexOptions.IgnoreCase)
                    && !col.IsPrimaryKey;

                DetectInlineReferance(definition, col);
                DetectCustomReferance(definition, col);

                table.Columns.Add(col);
            }

            DetectInTableLevelReferance(tableBody);
        }
       
        private static void DetectInTableLevelReferance(string tableBody)
        {
            var fkMatches = Regex.Matches(
               tableBody,
               @"FOREIGN\s+KEY\s*\((\w+)\)\s*REFERENCES\s+(\w+)\s*\((\w+)\)",
               RegexOptions.IgnoreCase
           );

            foreach (Match fk in fkMatches)
            {
                string columnName = fk.Groups[1].Value;
                string refTable = fk.Groups[2].Value;

                // منع التكرار لو Inline FK موجود
                bool alreadyExists = refrenseTabels.Any(r =>
                    r.DtoColumn.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));

                if (alreadyExists)
                    continue;

                var column = table.Columns.FirstOrDefault(c =>
                    c.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));

                if (column == null)
                    continue;

                refrenseTabels.Add(new RefrenseTableModel
                {
                    ClassName = $"cls{ToSingular(refTable)}",
                    EntityName = ToSingular(refTable),
                    DtoColumn = DtoGenerator.GenarateCoulmnDto(column)
                });
            }
        }
        private static void DetectInlineReferance(string definition, ColumnModel col)
        {
            // ===== Inline Foreign Key =====
            var inlineRefMatch = Regex.Match(
                definition,
                @"REFERENCES\s+(\w+)\s*\((\w+)\)",
                RegexOptions.IgnoreCase
            );

            if (inlineRefMatch.Success)
            {
                refrenseTabels.Add(new RefrenseTableModel
                {
                    ClassName = $"cls{ToSingular(inlineRefMatch.Groups[1].Value)}",
                    EntityName = ToSingular(inlineRefMatch.Groups[1].Value),
                    DtoColumn = DtoGenerator.GenarateCoulmnDto(col)
                });
            }
        }

        private static string RemoveSqlComments(string sql)
        {
            // Remove -- comments
            sql = Regex.Replace(
                sql,
                @"--.*?$",
                "",
                RegexOptions.Multiline
            );

            // Remove /* */ comments
            sql = Regex.Replace(
                sql,
                @"/\*[\s\S]*?\*/",
                "",
                RegexOptions.Multiline
            );

            return sql;
        }

        static string ToSingular(string word)
        {
            if (word.EndsWith("ies", StringComparison.OrdinalIgnoreCase))
                return word.Substring(0, word.Length - 3) + "y";

            if (word.EndsWith("ses", StringComparison.OrdinalIgnoreCase) ||
                word.EndsWith("xes", StringComparison.OrdinalIgnoreCase) ||
                word.EndsWith("ches", StringComparison.OrdinalIgnoreCase) ||
                word.EndsWith("shes", StringComparison.OrdinalIgnoreCase))
                return word.Substring(0, word.Length - 2);

            if (word.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                return word.Substring(0, word.Length - 1);

            return word; // already singular
        }
        private static void GenarateContext()
        {
            var entity = ToSingular(table.TableName);

            var idColumn = table.Columns.FirstOrDefault(c => c.IsIdentity);
            if (idColumn == null)
                throw new Exception("No Identity column found in table.");

            clsGenaric.codeGenerations = new CodeGenerationContext
            {
                table = table,
                Entity = ToSingular(table.TableName),
                IdColumn = DtoGenerator.GenarateCoulmnDto(table.Columns[0]),
                Enums = DetectTinyint(table),
                Procedures = GenareteNamesForSP(entity),
                ReferenceTables = refrenseTabels,
                FindByColumns = _FindByColumns
            };


        }
        
        private static List<ProcedureModel> GenareteNamesForSP(string Entity)
        {
            List<ProcedureModel> Procedures = new List<ProcedureModel>();
            ProcedureModel procedure = new ProcedureModel();
            procedure.Name = $"SP_Get{Entity}InfoByID";

            ProcedureModel procedure1 = new ProcedureModel();
            procedure1.Name = $"SP_AddNew{Entity}";

            ProcedureModel procedure2 = new ProcedureModel();
            procedure2.Name = $"SP_Update{Entity}Info";

            

            ProcedureModel procedure3 = new ProcedureModel();
            procedure3.Name = $"SP_GetAll{Entity}";
            ProcedureModel procedure4 = new ProcedureModel();
            procedure4.Name = $"SP_Delete{Entity}";

            Procedures.Add(procedure);
            Procedures.Add(procedure1);
            Procedures.Add(procedure2);
            Procedures.Add(procedure3);
            Procedures.Add(procedure4);

            return Procedures;

        }
        private static List<enumModel> DetectTinyint(TableModel table)
        {
            List<ColumnModel> tinyIntColumns =
                table.Columns
                    .FindAll(x => x.SqlType.Trim().Equals("TINYINT", StringComparison.OrdinalIgnoreCase));

            List<enumModel> enumModels = new List<enumModel>();

            foreach (var column in tinyIntColumns)
            {
                enumModel enumModel1 = new enumModel
                {
                    enumName = $"en{column.Name}"
                };

                enumModels.Add(enumModel1);
            }

           
                return enumModels;

        }


    }
}
