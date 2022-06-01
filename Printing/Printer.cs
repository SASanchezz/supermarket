using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace supermarket.Printing
{
    internal static class Printer
    {
        public static void PrintDataGrid(List<string[]> list, string[] columnNames)
        {
            if (list[0].Length != columnNames.Length)
            {
                throw new Exception("Not the same length");
            }

            var printDialog = new PrintDialog();

            if (printDialog.ShowDialog() != true) return;
            
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());

            var columnsNameRow = new TableRow();
            foreach (var name in columnNames)
            {
                var column = new TableColumn();
                table.Columns.Add(column);

                var cell = new TableCell(new Paragraph(new Run(name)));
                columnsNameRow.Cells.Add(cell);
            }
            table.RowGroups[0].Rows.Add(columnsNameRow);

            foreach (var element in list)
            {
                var row = new TableRow();
                for (int l = 0; l < columnNames.Length; ++l)
                {
                    var cell = new TableCell(new Paragraph(new Run(element[l].ToString())));
                    row.Cells.Add(cell);
                }
                table.RowGroups[0].Rows.Add(row);
            }

            var size = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            var doc = new FlowDocument(table)
            {
                FontFamily = new FontFamily("Calibri"),
                PageWidth = size.Width,
                PageHeight = size.Height,
                ColumnWidth = 1024,
                FontSize = 14
            };

            IDocumentPaginatorSource idpSource = doc;
            printDialog.PrintDocument(idpSource.DocumentPaginator, "");
        }
    }
}