namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System.Data.Entity.Migrations;
    using System.Linq;

    using MSSQLDB;

    using MysqlDbFirst;

    using Supermarket.ConsoleApp.Constants;
    using Supermarket.ConsoleApp.Interfaces;

    #endregion

    public class Problem7Command : AbstractCommand
    {
        public Problem7Command(IEngine engine)
            : base(engine)
        {
        }

        public override void Execute()
        {
            // his should transfer the data holding the vendors, their products, their incomes by product and their expenses. Th
            using (var msdb = new MSSQLContext())
            using (var mydb = new supermarket_chainEntities())
            {
                var allNewVendors = msdb.Vendors.Select(v => new { v.Id, v.VendorName }).ToList();
                var allNewProducts =
                    msdb.Products.Select(p => new { p.Id, p.ProductName, p.VendorId, p.Price }).ToList();
                var allNewExpenses = msdb.Expenses.Select(e => new { e.Id, e.Amount, e.DateTime, e.VendorId }).ToList();
                var allNewVendorProducts =
                    msdb.SupermarketProducts.Select(
                        sp => new { sp.ProductId, sp.Product.VendorId, sp.Quantity, sp.SaleDate, sp.UnitPrice })
                        .ToList();
                this.Engine.Output.AppendLine(Messages.Delimiter);
                this.Engine.Output.AppendLine(Messages.MysqlNewDataPulled);

                foreach (var newVendor in allNewVendors)
                {
                    var mysqlVendor = new vendor { id = newVendor.Id, vendor_name = newVendor.VendorName };
                    mydb.vendors.AddOrUpdate(v => v.id, mysqlVendor);
                }

                this.Engine.Output.AppendLine(Messages.MysqlNewVProductsAdded);

                foreach (var newProduct in allNewProducts)
                {
                    var musqlProduct = new product
                                           {
                                               id = newProduct.Id, 
                                               vendor_id = newProduct.VendorId, 
                                               product_name = newProduct.ProductName, 
                                               price = newProduct.Price
                                           };
                    mydb.products.AddOrUpdate(p => p.id, musqlProduct);
                }

                this.Engine.Output.AppendLine(Messages.MysqlNewVProductsAdded);

                foreach (var newExpense in allNewExpenses)
                {
                    var mysqExpense = new expens
                                          {
                                              id = newExpense.Id, 
                                              amount = newExpense.Amount, 
                                              date_time = newExpense.DateTime, 
                                              vendor_id = newExpense.VendorId
                                          };
                    mydb.expenses.AddOrUpdate(e => e.id, mysqExpense);
                }

                this.Engine.Output.AppendLine(Messages.MysqlNewVExpensesAdded);

                foreach (var newVendorProduct in allNewVendorProducts)
                {
                    var mysqlVendorProduct = new vendors_products
                                                 {
                                                     vendor_id = newVendorProduct.VendorId, 
                                                     product_id = newVendorProduct.ProductId, 
                                                     quantity = newVendorProduct.Quantity, 
                                                     sale_date = newVendorProduct.SaleDate, 
                                                     unit_price = newVendorProduct.UnitPrice
                                                 };
                    mydb.vendors_products.AddOrUpdate(
                        vp => new { vp.vendor_id, vp.product_id, vp.sale_date, vp.quantity, vp.unit_price }, 
                        mysqlVendorProduct);
                }

                mydb.SaveChanges();

                this.Engine.Output.AppendLine(Messages.MysqDataTransferSuccess);
            }
        }
    }
}