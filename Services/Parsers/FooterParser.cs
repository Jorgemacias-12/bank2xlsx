using Bank2Pdf.Helpers;
using Bank2Pdf.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Bank2Pdf.Services.Parsers
{
    class FooterParser
    {
        private static readonly Regex ValueRegex =
            new(
                @"\$?[\d,]+(?:\.\d{2})?$",
                RegexOptions.Compiled
            );

        public List<FooterItem> Parse(string[] lines)
        {
            List<FooterItem> items = [];

            IEnumerable<string> footerLines =
                lines.TakeLast(2);

            foreach (string line in footerLines)
            {
                bool isCurrency =
                    line.Contains('$');

                string normalizedLine =
                    line.Replace("$", "");

                string[] tokens =
                    normalizedLine.Split(
                        ' ',
                        StringSplitOptions.RemoveEmptyEntries);

                List<string> currentKey = [];

                foreach (string token in tokens)
                {
                    if (Number.IsNumeric(token))
                    {
                        items.Add(new FooterItem
                        {
                            Key = string.Join(' ', currentKey),
                            Value = isCurrency
                                ? $"${token}"
                                : token,
                            IsCurrency = isCurrency
                        });

                        currentKey.Clear();
                    }
                    else
                    {
                        currentKey.Add(token);
                    }
                }
            }

            return items;
        }
    }
}
