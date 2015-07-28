using System;
using System.Data;
using System.IO;
using System.Linq;
using GemBox.Spreadsheet;
using MysqlDbFirst;
using SQLite;
using Supermarket.ConsoleApp.Interfaces;
using SupermarketChain.SQLite.Models;

namespace Supermarket.ConsoleApp.Commands
{
    public class Problem8Command : AbstractCommand
    {
        public Problem8Command(IEngine engine) : base(engine)
        {
        }

        public override void Execute()
        {
            using (var mySqlContext = new supermarket_chainEntities())
            {
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                var sqliteContext = new SQLiteConnection("Input\\SQLLiteDB");
                var tax_information = sqliteContext.Table<tax_information>();

                var workbook = new ExcelFile();

                var worksheet = workbook.Worksheets.Add("Tax Information");

                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("Vendor", typeof(string));
                dataTable.Columns.Add("Incomes", typeof(decimal));
                dataTable.Columns.Add("Expenses", typeof(decimal));
                dataTable.Columns.Add("Total Taxes", typeof(decimal));
                dataTable.Columns.Add("Financial Result", typeof(decimal));

                foreach (var vendor in mySqlContext.vendors.ToList())
                {
                    decimal incomes = vendor.vendors_products.Sum(vp => vp.unit_price * vp.quantity);
                    decimal expenses = vendor.expenses.Sum(e => e.amount);
                    decimal totalTaxes = 0;

                    foreach (var vendorProduct in vendor.products)
                    {
                        if (tax_information.Any(vp => vp.product_name == vendorProduct.product_name))
                        {
                            totalTaxes += vendor.vendors_products.Where(vp => vp.product_id == vendorProduct.id).Sum(vp => vp.unit_price*vp.quantity);
                        }
                    }

                    decimal financialResult = incomes - expenses - totalTaxes;

                    dataTable.Rows.Add(new object []
                    {
                        vendor.vendor_name,
                        incomes,
                        expenses,
                        totalTaxes,
                        financialResult
                    });
                }

                worksheet.InsertDataTable(dataTable,
                new InsertDataTableOptions()
                {
                    ColumnHeaders = true,
                    StartRow = 0
                });

                worksheet.Cells["A1"].Style.Font.Weight = ExcelFont.BoldWeight;
                worksheet.Cells["B1"].Style.Font.Weight = ExcelFont.BoldWeight;
                worksheet.Cells["C1"].Style.Font.Weight = ExcelFont.BoldWeight;
                worksheet.Cells["D1"].Style.Font.Weight = ExcelFont.BoldWeight;
                worksheet.Columns[4].Style.Font.Weight = ExcelFont.BoldWeight;

                worksheet.Columns[0].Width = 9000;
                worksheet.Columns[1].Width = 4000;
                worksheet.Columns[2].Width = 4000;
                worksheet.Columns[3].Width = 4000;
                worksheet.Columns[4].Width = 4000;

                for (int i = 0; i <= 4; i++)
                {
                    worksheet.Columns[i].Cells[0].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(217, 217, 217));
                }

                workbook.Save("Output\\Tax_Information.xlsx");
            }
        }
    }
}
