namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;

    using MSSQLDB;

    using MSSQLModels.Models;

    using Supermarket.ConsoleApp.Constants;
    using Supermarket.ConsoleApp.Interfaces;

    using SupermarketChain.MSSQL.Models;

    #endregion

    public class Problem6Command : AbstractCommand
    {
        public Problem6Command(IEngine engine)
            : base(engine)
        {
        }

        public override void Execute()
        {
            using (var db = new MSSQLContext())
            {
                var fileName = "Input/Sample-Vendor-Expenses.xml";

                var xmlDoc = XDocument.Load(fileName);
                foreach (var vendorNode in xmlDoc.XPathSelectElements("expenses-by-month/vendor"))
                {
                    string vendorName = vendorNode.FirstAttribute.Value;
                    int vendorId = db.Vendors.First(v => v.VendorName == vendorName).Id;

                    foreach (var expense in vendorNode.Descendants())
                    {
                        var newExpense = new Expense
                                             {
                                                 VendorId = vendorId,
                                                 Amount = decimal.Parse(expense.Value), 
                                                 DateTime = DateTime.Parse("01-" + expense.FirstAttribute.Value)
                                             };

                        db.Expenses.Add(newExpense);
                    }
                }

                db.SaveChanges();
                this.Engine.Output.AppendLine(Messages.XmlImportSuccess);
            }
        }
    }
}