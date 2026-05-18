using Bank2xlsx.Helpers;
using Bank2xlsx.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics;

namespace Bank2xlsx.Services.Exporters;

public class ExcelExporter
{
    public XLWorkbook CreateWorkbook(ParsedFile parsedFile)
    {
        XLWorkbook workbook = new();

        IXLWorksheet worksheet =
            CreateWorksheet(workbook);

        InsertHeaders(
            worksheet,
            parsedFile.Headers);

        InsertRows(
            worksheet,
            parsedFile.Rows);

        InsertFooter(
            worksheet,
            parsedFile.Footer
        );

        CreateTable(
            worksheet,
            parsedFile.Headers.Count,
            parsedFile.Rows.Count);

        ApplyStyles(worksheet);

        return workbook;
    }

    public async Task ExportAsync(
        XLWorkbook workbook,
        string outputPath)
    {
        await Task.Run(() =>
        {
            workbook.SaveAs(outputPath);
        });
    }

    private static IXLWorksheet CreateWorksheet(
        XLWorkbook workbook)
    {
        return workbook.Worksheets
            .Add("Transactions");
    }

    private static void InsertHeaders(
        IXLWorksheet worksheet,
        List<string> headers)
    {
        for (int column = 0;
             column < headers.Count;
             column++)
        {
            worksheet.Cell(1, column + 1)
                .Value = headers[column];
        }
    }
    
    private static void InsertFooter(
        IXLWorksheet worksheet, 
        List<FooterItem> footerData)
    {
        int startRowPadding = 3;
        int startRow;

        IXLRow? lastRow =
            worksheet.LastRowUsed();

        if (lastRow is null)
            return;

        startRow =
            lastRow.RowNumber() + startRowPadding;        

        foreach((FooterItem item, int index) in footerData.Select((item, index) => (item, index)))
        {
            int currentRow = startRow + index;

            worksheet.Cell(currentRow, 1)
                .Value = item.Key;

            worksheet.Cell(currentRow, 2)
                .Value = item.Value;
        }
    }

    private static void InsertRows(
        IXLWorksheet worksheet,
        List<List<string>> rows)
    {
        for (int row = 0;
             row < rows.Count;
             row++)
        {
            List<string> currentRow =
                rows[row];

            for (int column = 0;
                 column < currentRow.Count;
                 column++)
            {
                worksheet.Cell(
                    row + 2,
                    column + 1)
                    .Value = currentRow[column];
            }
        }
    }

    private static void CreateTable(
        IXLWorksheet worksheet,
        int headersCount,
        int rowsCount)
    {
        int startIndex = 1;
        int lastColumn = headersCount;
        int lastRow = rowsCount;

        var tableRange = worksheet.Range(
            startIndex,
            startIndex,
            lastRow,
            lastColumn
        );

        IXLTable table =
            tableRange.CreateTable();

        table.Theme = XLTableTheme.TableStyleLight11;
    }

    private static void ApplyStyles(
        IXLWorksheet worksheet)
    {
        worksheet.Columns()
            .AdjustToContents();
    }

    public async Task ExportStreamingAsync(
        ParsedFile parsedFile,
        string outputPath)
    {
        if (parsedFile is null)
            return;

        if (string.IsNullOrWhiteSpace(outputPath))
            return;

        await Task.Run(() =>
        {
            using SpreadsheetDocument document =
                SpreadsheetDocument.Create(
                    outputPath,
                    SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookPart =
                document.AddWorkbookPart();

            workbookPart.Workbook =
                new Workbook();

            WorksheetPart worksheetPart =
                workbookPart.AddNewPart<WorksheetPart>();

            worksheetPart.Worksheet =
                new Worksheet(new SheetData());

            SheetData sheetData =
                worksheetPart.Worksheet
                    .GetFirstChild<SheetData>()!;

            Sheets sheets =
                workbookPart.Workbook
                    .AppendChild(new Sheets());

            Sheet sheet = new()
            {
                Id = workbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Transacciones"
            };

            sheets.Append(sheet);

            int rowIndex = 1;

            // Headers
            sheetData.Append(
                Excel.CreateRow(
                    parsedFile.Headers,
                    rowIndex++)
            );

            // Rows
            foreach (List<string> row in parsedFile.Rows)
            {
                sheetData.Append(
                    Excel.CreateRow(
                        row,
                        rowIndex++)
                );
            }

            // Empty spacing before footer
            rowIndex++;

            // Footer
            foreach (FooterItem footer in parsedFile.Footer)
            {
                sheetData.Append(
                    Excel.CreateRow(
                        new List<string>
                        {
                        footer.Key,
                        footer.Value
                        },
                        rowIndex++)
                );
            }

            worksheetPart.Worksheet.Save();
            workbookPart.Workbook.Save();

            Process.Start("explorer.exe", outputPath);
        });
    }
}