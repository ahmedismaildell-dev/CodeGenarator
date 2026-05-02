using CodeGenarator_Buisness.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGenarator_Buisness.Parser
{
    public class enumParser
    {
        public static enumModel Parse(string code)
        {
            enumModel enumModel = new enumModel();

            // 1️⃣ اسم الـ enum
            var enumNameMatch = Regex.Match(code, @"enum\s+(\w+)", RegexOptions.IgnoreCase);
            if (!enumNameMatch.Success)
                throw new Exception("Enum name not found");

           
            enumModel.enumName = enumNameMatch.Groups[1].Value;


            // 2️⃣ عناصر الـ enum
            var itemMatches = Regex.Matches(
                code,
                @"(\w+)\s*=\s*(\d+)",
                RegexOptions.IgnoreCase
            );

            foreach (Match match in itemMatches)
            {
                enumModel.Items.Add(new EnumItemModel
                {
                    Name = match.Groups[1].Value,
                    Value = int.Parse(match.Groups[2].Value)
                });
            }

            return enumModel;
        }
    }
}
