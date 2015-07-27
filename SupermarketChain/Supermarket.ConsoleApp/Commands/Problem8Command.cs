using System;
using System.IO;
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

                var sqliteContext = new SQLiteConnection("SQLLiteDB");

                var workbook = new ExcelFile();

                var worksheet = workbook.Worksheets.Add("Tax Information");

                //Setting Column Headers
                worksheet.Cells["A1"].Value = "Vendor";
                worksheet.Cells["B1"].Value = "Incomes";
                worksheet.Cells["C1"].Value = "Expenses";
                worksheet.Cells["D1"].Value = "Total Taxes";
                worksheet.Cells["E1"].Value = "Financial Result";

                workbook.Save("Tax_Information.xlsx");
            }
        }
    }
}
