namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System.IO;
    using System.Linq;
    using Supermarket.ConsoleApp.Interfaces;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using MSSQLDB;
    using Supermarket.ConsoleApp.Constants;

    #endregion

    public class Problem3Command : AbstractCommand
    {
        public Problem3Command(IEngine engine) : base(engine)
        {
        }

        private PdfPCell CreateBoldCell(string content, BaseColor color = null)
        {
            var cell = new PdfPCell(new Phrase(content));
            cell.Phrase.Font.SetStyle(Font.BOLD);
            cell.BackgroundColor = color;

            return cell;
        }

        private PdfPCell CreateAlignmentedCell(string content, int alignment)
        {
            var cell = new PdfPCell(new Phrase(content));
            cell.HorizontalAlignment = alignment;

            return cell;
        }

        public override void Execute()
        {
            using (FileStream fileStream = new FileStream("Output/Aggregated-Sales-Report.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            using (Document pdfDocument = new Document())
            using (var mssqlContext = new MSSQLContext())
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, fileStream);

                pdfDocument.Open();

                PdfPTable mainTable = new PdfPTable(5);
                PdfPCell mainHeaderCell = new PdfPCell(new Phrase("Aggregated Sales Report"));
                mainHeaderCell.Phrase.Font.SetStyle(Font.BOLD);
                mainHeaderCell.Colspan = 5;
                mainHeaderCell.HorizontalAlignment = 1;

                mainTable.AddCell(mainHeaderCell);

                var saleReportsByDate = from supermarketProduct in mssqlContext.SupermarketProducts
                    join product in mssqlContext.Products on supermarketProduct.ProductId equals product.Id
                    join supermarket in mssqlContext.Supermarkets on supermarketProduct.SupermarketId equals
                        supermarket.Id
                    group supermarketProduct by supermarketProduct.SaleDate;

                decimal grandTotalSum = 0;

                var productHeaderCell = CreateBoldCell("Product", new BaseColor(217, 217, 217));
                var quantityHeaderCell = CreateBoldCell("Quantity", new BaseColor(217, 217, 217));
                var unitPriceHeaderCell = CreateBoldCell("Unit Price", new BaseColor(217, 217, 217));
                var locationHeaderCell = CreateBoldCell("Location", new BaseColor(217, 217, 217));
                var sumHeaderCell = CreateBoldCell("Sum", new BaseColor(217, 217, 217));

                foreach (var date in saleReportsByDate)
                {
                    decimal totalSum = 0;

                    PdfPCell dateHeaderCell = new PdfPCell(new Phrase("Date: " + date.Key.ToString("dd-MMMM-yyyy")));
                    dateHeaderCell.BackgroundColor = new BaseColor(242, 242, 242);
                    dateHeaderCell.Colspan = 5;

                    mainTable.AddCell(dateHeaderCell);

                    mainTable.AddCell(productHeaderCell);
                    mainTable.AddCell(quantityHeaderCell);
                    mainTable.AddCell(unitPriceHeaderCell);
                    mainTable.AddCell(locationHeaderCell);
                    mainTable.AddCell(sumHeaderCell);

                    foreach (var record in date)
                    {
                        mainTable.AddCell(new PdfPCell(new Phrase(record.Product.ProductName)));
                        mainTable.AddCell(CreateAlignmentedCell(record.Quantity.ToString(), Element.ALIGN_CENTER));
                        mainTable.AddCell(CreateAlignmentedCell(record.UnitPrice.ToString(), Element.ALIGN_CENTER));
                        mainTable.AddCell(new PdfPCell(new Phrase(record.Supermarket.Name)));
                        mainTable.AddCell(CreateAlignmentedCell((record.Quantity * record.UnitPrice).ToString(), Element.ALIGN_RIGHT));

                        totalSum += record.Quantity * record.UnitPrice;
                    }

                    PdfPCell totalSumCellTitle = new PdfPCell(new Phrase("Total sum for " + date.Key.ToString("dd-MMMM-yyyy") + ":"));
                    totalSumCellTitle.Colspan = 4;
                    totalSumCellTitle.HorizontalAlignment = Element.ALIGN_RIGHT;

                    PdfPCell totalSumCell = new PdfPCell(new Phrase(totalSum.ToString()));
                    totalSumCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    totalSumCell.Phrase.Font.SetStyle(Font.BOLD);

                    mainTable.AddCell(totalSumCellTitle);
                    mainTable.AddCell(totalSumCell);

                    grandTotalSum += totalSum;
                }

                PdfPCell grandTotalCellTitle = new PdfPCell(new Phrase("Grand total:"));
                grandTotalCellTitle.Colspan = 4;
                grandTotalCellTitle.HorizontalAlignment = Element.ALIGN_RIGHT;
                grandTotalCellTitle.BackgroundColor = new BaseColor(182, 221, 232);

                PdfPCell grandTotalCell = new PdfPCell(new Phrase(grandTotalSum.ToString()));
                grandTotalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                grandTotalCell.BackgroundColor = new BaseColor(182, 221, 232);
                grandTotalCell.Phrase.Font.SetStyle(Font.BOLD);

                mainTable.AddCell(grandTotalCellTitle);
                mainTable.AddCell(grandTotalCell);

                pdfDocument.Add(mainTable);

                this.Engine.Output.AppendLine(Messages.PdfGenerationSuccess);
            }
        }
    }
}
