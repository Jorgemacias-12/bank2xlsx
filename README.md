# Bank2xlsx

A tool to convert poorly formatted bank transaction exports (TXT) into well-structured Excel tables. This repository was born from a colleague's need to transform the transaction files downloaded from the company's bank into a usable Excel sheet.

---

## Project overview

This is a small WPF/.NET application that reads bank export files (commonly messy TXT files), parses them into rows and columns, and exports the data into Excel-friendly formats (single-file publish supported). The main purpose is to rescue and normalize transaction data so it can be analyzed in a spreadsheet.

## Why this project exists

A coworker regularly downloads transaction histories from the corporate bank. The bank's exports are inconsistent and often appear as badly formatted plain text. Converting that data by hand into Excel was slow and error-prone, so this project was developed to automate the conversion and deliver a tabular, cleaned result.

## Parsers (what they do)

The core of the project are format-specific parsers that:

- Detect the bank export format and its quirks (delimiters, fixed-width columns, header/footer lines).
- Normalize dates and numeric fields (amounts, balances), handling locale differences.
- Clean stray characters, merge broken lines for multi-line descriptions, and split combined fields into structured columns.
- Emit a consistent in-memory table representation that is handed off to the exporter.

The `ExcelExporter` component receives the cleaned table and writes a `.xlsx` (or single-file `.exe` publish can be used to distribute the app). Parsers are designed to be extensible so new bank formats can be added with minimal changes.

## How it works (high level)

1. Launch the application (WPF UI).
2. Load the exported TXT file from the bank.
3. The app selects a parser based on the file contents.
4. The parser produces a normalized tabular model (rows/columns).
5. Export to Excel using the exporter.


## Adding or improving parsers

- Create a new parser to handle a new bank's export format.
- Focus on robust field extraction, date/number normalization, and cleaning broken descriptions.
- Add unit tests (if available) for edge cases encountered in real exports.

## Contributing

Contributions are welcome. If you add a parser, include example input files (sanitized) and expected output so the behavior is reproducible.

---

## Translations

A Spanish translation of this README is available in `README.es-MX.md`. That file is a translation of this English README; keep both files in sync when you make updates.

 
