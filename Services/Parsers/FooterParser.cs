using Bank2Pdf.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Bank2Pdf.Services.Parsers
{
    class FooterParser
    {
        private static readonly Regex FooterRegex =
            new(@"([A-Z.\s]+?)\s+(\$?\s*[\d,]+(?:\.\d{2})?)");
    
        public List<FooterItem> Parse(string[] lines)
        {
            List<FooterItem> items = [];

            IEnumerable<string> footerLines =
                lines.TakeLast(2);

            foreach(string line in footerLines)
            {
                MatchCollection matches =
                    FooterRegex.Matches(line);

                foreach(Match match in matches)
                {
                    items.Add(new FooterItem
                    {
                        Key = match.Groups[1]
                            .Value
                            .Trim(),

                        Value = match.Groups[2]
                            .Value
                            .Trim(),

                        IsCurrency = match.Groups[2]
                            .Value
                            .Contains("$")
                    });
                }
            }

            return items;
        }
    }
}
