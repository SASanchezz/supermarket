﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using supermarket.Utils;

namespace supermarket.Printing
{
    internal class Printer
    {
        public Printer()
        {
            PrintCommand = new RelayCommand<object>(_ => OpenPrintDialog());
        }
        
        public FlowDocument Document { get; set; }

        public RelayCommand<object> PrintCommand { get; }

        private void OpenPrintDialog()
        {
            var printDialog = new PrintDialog();
            printDialog.ShowDialog();
            printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
            IDocumentPaginatorSource idpSource = Document;
            printDialog.PrintDocument(idpSource.DocumentPaginator, "");
        }

        public void PrintDataGrid(List<string[]> list, string tableName, string[] columnNames)
        {
            PrintPreviewWindow printPreviewWindow = new PrintPreviewWindow();
            
            if (list[0].Length != columnNames.Length)
            {
                throw new Exception("Not the same length");
            }

            var table = new Table();
            table.BorderBrush = Brushes.Black;
            table.BorderThickness = new Thickness(0.5);
            table.RowGroups.Add(new TableRowGroup());

            var tableHeader = new TableRow();
            tableHeader.FontSize = 40;
            var cellTableHeader = new TableCell(new Paragraph(new Run(tableName)));
            cellTableHeader.ColumnSpan = columnNames.Length;
            tableHeader.Cells.Add(cellTableHeader);
            table.RowGroups[0].Rows.Add(tableHeader);

            var columnsNameRow = new TableRow();
            foreach (var name in columnNames)
            {
                var column = new TableColumn();
                table.Columns.Add(column);

                var cell = new TableCell(new Paragraph(new Run(name)));

                cell.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                cell.BorderBrush = Brushes.Black;
                cell.BorderThickness = new Thickness(0.5);

                columnsNameRow.Cells.Add(cell);
            }
            table.RowGroups[0].Rows.Add(columnsNameRow);

            foreach (var element in list)
            {
                var row = new TableRow();
                for (int l = 0; l < columnNames.Length; ++l)
                {
                    TableCell cell;

                    if (string.IsNullOrEmpty(element[l]))
                    {
                        cell = new TableCell(new Paragraph(new Run("")));
                    }
                    else
                    {
                        cell = new TableCell(new Paragraph(new Run(element[l].ToString())));
                    }
                    cell.BorderBrush = Brushes.Black;
                    cell.BorderThickness = new Thickness(0.5);
                    row.Cells.Add(cell);
                }
                table.RowGroups[0].Rows.Add(row);
            }

            Document = new FlowDocument(table)
            {
                FontFamily = new FontFamily("Calibri"),
                ColumnWidth = 1024,
                FontSize = 14
            };
            printPreviewWindow.DataContext = this;
            printPreviewWindow.Show();
        }
    }
}