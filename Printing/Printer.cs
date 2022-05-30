using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace supermarket.Printing
{
    static internal class Printer
    {
        public static void PrintDataGrid(List<string[]> list, string[] columnNames)
        {
            if (list[0].Length != columnNames.Length)
            {
                throw new Exception("Not the same length");
            }

            var printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                var table = new Table();
                table.RowGroups.Add(new TableRowGroup());

                var columnsNameRow = new TableRow();
                for (int i = 0; i < columnNames.Length; ++i)
                {
                    var column = new TableColumn();
                    table.Columns.Add(column);

                    var cell = new TableCell(new Paragraph(new Run(columnNames[i])));
                    columnsNameRow.Cells.Add(cell);
                }

                table.RowGroups[0].Rows.Add(columnsNameRow);

                for (int i = 0; i < list.Count; ++i)
                {
                    var row = new TableRow();
                    for (int l = 0; l < columnNames.Length; ++l)
                    {
                        var cell = new TableCell(new Paragraph(new Run(list[i][l].ToString())));
                        row.Cells.Add(cell);

                    }

                    table.RowGroups[0].Rows.Add(row);
                }

                var size = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

                var doc = new FlowDocument(table);
                doc.PageWidth = size.Width;
                doc.PageHeight = size.Height;
                doc.ColumnWidth = 1024;
                doc.FontSize = 6;

                IDocumentPaginatorSource idpSource = doc;

                printDialog.PrintDocument(idpSource.DocumentPaginator, "");
            }   
        }
    }
}
