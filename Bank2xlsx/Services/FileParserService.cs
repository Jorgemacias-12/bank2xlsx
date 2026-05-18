using Bank2xlsx.Models;
using Bank2xlsx.Services.Exporters;
using Bank2xlsx.Services.Parsers;
using System.IO;

namespace Bank2xlsx.Services
{
    /// <summary>
    /// Servicio encargado de orquestar el proceso completo de lectura,
    /// parseo y exportación de archivos bancarios a formato Excel.
    /// </summary>
    /// <remarks>
    /// Este servicio actualmente utiliza un exportador basado en ClosedXML.
    /// Está previsto su reemplazo por una implementación basada en OpenXML
    /// con streaming por filas para mejorar el manejo de memoria en archivos grandes.
    /// </remarks>
    class FileParserService
    {
        private readonly HeaderParser _headerParser;
        private readonly TransactionParser _transactionParser;
        private readonly FooterParser _footerParser;
        private readonly ExcelExporter _excelExporter;

        public FileParserService()
        {
            _headerParser = new HeaderParser();
            _transactionParser = new TransactionParser();
            _footerParser = new FooterParser();
            _excelExporter = new ExcelExporter();
        }

        /// <summary>
        /// Procesa un archivo arrastrado, lo parsea en memoria y lo exporta a Excel.
        /// </summary>
        /// <param name="droppedFile">Archivo fuente arrastrado por el usuario.</param>
        /// <returns>Tarea asíncrona que representa el proceso completo.</returns>
        /// <remarks>
        /// ⚠️ DEPRECATED LOGIC NOTICE:
        /// Este método actualmente utiliza un flujo basado en ClosedXML (XLWorkbook en memoria).
        /// Este enfoque puede causar alto consumo de memoria en archivos grandes.
        /// 
        /// Será reemplazado por una versión basada en OpenXML con escritura en streaming
        /// (fila por fila) para mejorar rendimiento y escalabilidad.
        /// </remarks>
        [Obsolete(
            "Este flujo será reemplazado por un exportador basado en OpenXML streaming (FileParserServiceV2 o ExcelExporterStream).")]
        public async Task ParseAsync(DroppedFile droppedFile)
        {
            string[] lines = await File.ReadAllLinesAsync(droppedFile.FullPath);

            var parsedFile = await Task.Run(() =>
            {
                return new ParsedFile
                {
                    SourceFile = droppedFile,
                    Headers = _headerParser.Parse(lines),
                    Rows = _transactionParser.Parse(lines),
                    Footer = _footerParser.Parse(lines)
                };
            });

            var workbook =
                _excelExporter.CreateWorkbook(parsedFile);

            string outputPath = Path.ChangeExtension(droppedFile.FullPath, ".xlsx");

            await _excelExporter.ExportAsync(workbook, outputPath);
        }

         public async Task ParseStreamingAsync(DroppedFile droppedFile)
        {
            string[] lines = 
                await File.ReadAllLinesAsync(droppedFile.FullPath);

            var parsedFile = await Task.Run(() =>
            {
                return new ParsedFile
                {
                    SourceFile = droppedFile,
                    Headers = _headerParser.Parse(lines),
                    Rows = _transactionParser.Parse(lines),
                    Footer = _footerParser.Parse(lines)
                };
            });

            string outputPath =
                Path.ChangeExtension(
                    droppedFile.FullPath,
                    ".xlsx");

            await _excelExporter.ExportStreamingAsync(
                parsedFile,
                outputPath
            );
        }
    }
}