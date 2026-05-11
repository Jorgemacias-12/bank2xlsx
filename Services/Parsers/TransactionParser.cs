using System;
using System.Collections.Generic;
using System.Text;

namespace Bank2Pdf.Services.Parsers
{
    class TransactionParser
    {
        private const int IGNORE_LINES_COUNT = 8;
        private const int FOOTER_LINES_COUNT = 2;

        public List<List<string>> Parse(string[] lines)
        {
            return lines
                .Skip(IGNORE_LINES_COUNT)
                .Take(lines.Length
                    - IGNORE_LINES_COUNT
                    - FOOTER_LINES_COUNT)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(
                    "|",
                    StringSplitOptions.RemoveEmptyEntries))
                .Select(parts => parts
                    .Select(x => x.Trim())
                    .ToList())
                .ToList();
        }
    }
}
