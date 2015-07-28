using System.Data.Entity.Migrations;

namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;

    using MSSQLDB;

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
            using (var mssqlContext = new MSSQLContext())
            {
                var fileName = "Input\\Sample-Vendor-Expenses.xml";

                var xmlDoc = XDocument.Load(fileName);
                foreach (var vendorNode in xmlDoc.XPathSelectElements("expenses-by-month/vendor"))
                {
                    string vendorName = vendorNode.FirstAttribute.Value;
                    int vendorId = mssqlContext.Vendors.First(v => v.VendorName == vendorName).Id;

                    foreach (var expense in vendorNode.Descendants())
                    {
                        var newExpense = new Expense
                                             {
                                                 VendorId = vendorId,
                                                 Amount = decimal.Parse(expense.Value), 
                                                 DateTime = DateTime.Parse("01-" + expense.FirstAttribute.Value)
                                             };

                        mssqlContext.Expenses.AddOrUpdate(newExpense);
                    }
                }

                mssqlContext.SaveChanges();
                this.Engine.Output.AppendLine(Messages.XmlImportSuccess);
            }
        }
    }
}