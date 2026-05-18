namespace Bank2xlsx.Services.Parsers
{
    class HeaderParser
    {
        public List<string> Parse(string[] lines)
        {
            string headerLine = lines[7];

            return headerLine
                .Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
        }
    }
}
