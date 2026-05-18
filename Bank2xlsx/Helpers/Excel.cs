using DocumentFormat.OpenXml.Spreadsheet;

namespace Bank2xlsx.Helpers
{
    internal class Excel
    {
        public static Row CreateRow(
            List<string> values,
            int rowIndex)
        {
            Row row = new()
            {
                RowIndex = (uint)rowIndex
            };

            for (int columnIndex = 0;
                 columnIndex < values.Count;
                 columnIndex++)
            {
                string cellReference =
                    GetCellReference(columnIndex + 1, rowIndex);

                Cell cell = new()
                {
                    CellReference = cellReference,
                    DataType = CellValues.String,
                    CellValue = new CellValue(values[columnIndex] ?? string.Empty)
                };

                row.Append(cell);
            }

            return row;
        }

        private static string GetCellReference(
             int columnNumber,
             int rowNumber)
        {
            const int alphabetLength = 26;
            const int asciiUppercaseA = 65;
            const int excelColumnOffset = 1;

            string columnName = string.Empty;

            while (columnNumber > 0)
            {
                int remainder =
                    (columnNumber - excelColumnOffset)
                    % alphabetLength;

                columnName =
                    (char)(asciiUppercaseA + remainder)
                    + columnName;

                columnNumber =
                    (columnNumber - remainder - excelColumnOffset)
                    / alphabetLength;
            }

            return $"{columnName}{rowNumber}";
        }
    }
}
