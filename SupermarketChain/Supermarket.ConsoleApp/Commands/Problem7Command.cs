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
            using (var mssqlContext = new MSSQLContext())
            using (var mysqlContext = new supermarket_chainEntities())
            {
                var allNewVendors =
                    mssqlContext.Vendors.Where(v => v.IsExportedToMysql == false)
                        .Select(v => new { v.Id, v.VendorName, v.IsExportedToMysql })
                        .ToList();
                var allNewProducts =
                    mssqlContext.Products.Where(v => v.IsExportedToMysql == false)
                        .Select(p => new { p.Id, p.ProductName, p.VendorId, p.Price })
                        .ToList();
                var allNewExpenses =
                    mssqlContext.Expenses.Where(v => v.IsExportedToMysql == false)
                        .Select(e => new { e.Id, e.Amount, e.DateTime, e.VendorId })
                        .ToList();
                var allNewVendorProducts =
                    mssqlContext.SupermarketProducts.Where(v => v.IsExportedToMysql == false)
                        .Select(sp => new { sp.ProductId, sp.Product.VendorId, sp.Quantity, sp.SaleDate, sp.UnitPrice })
                        .ToList();

                this.Engine.Output.AppendLine(Messages.Delimiter);
                this.Engine.Output.AppendLine(Messages.MysqlNewDataPulled);


                foreach (var vendor in allNewVendors)
                {
                    var mysqlVendor = new vendor { id = vendor.Id, vendor_name = vendor.VendorName };

                    mysqlContext.vendors.AddOrUpdate(v => v.id, mysqlVendor);
                }

                this.Engine.Output.AppendLine(Messages.MysqlNewVendorsAdded);

                foreach (var product in allNewProducts)
                {
                    var mysqlProduct = new product
                                           {
                                               id = product.Id,
                                               vendor_id = product.VendorId,
                                               product_name = product.ProductName,
                                               price = product.Price
                                           };

                    mysqlContext.products.AddOrUpdate(p => p.id, mysqlProduct);
                }

                this.Engine.Output.AppendLine(Messages.MysqlNewVProductsAdded);

                foreach (var expense in allNewExpenses)
                {
                    var mysqlExpense = new expens
                                          {
                                              id = expense.Id,
                                              amount = expense.Amount,
                                              date_time = expense.DateTime,
                                              vendor_id = expense.VendorId
                                          };

                    mysqlContext.expenses.AddOrUpdate(e => e.id, mysqlExpense);
                }

                this.Engine.Output.AppendLine(Messages.MysqlNewVExpensesAdded);

                foreach (var vendorProduct in allNewVendorProducts)
                {
                    var mysqlVendorProduct = new vendors_products
                                                 {
                                                     vendor_id = vendorProduct.VendorId,
                                                     product_id = vendorProduct.ProductId,
                                                     quantity = vendorProduct.Quantity,
                                                     sale_date = vendorProduct.SaleDate,
                                                     unit_price = vendorProduct.UnitPrice
                                                 };
                    mysqlContext.vendors_products.AddOrUpdate(
                        vp => new { vp.vendor_id, vp.product_id, vp.sale_date, vp.quantity, vp.unit_price }, 
                        mysqlVendorProduct);
                }

                //cuz i can`t do it smarter than that 
                mssqlContext.Database.ExecuteSqlCommand("UPDATE Vendors SET IsExportedToMysql = 1 WHERE IsExportedToMysql = 0");
                mssqlContext.Database.ExecuteSqlCommand("UPDATE Products SET IsExportedToMysql = 1 WHERE IsExportedToMysql = 0");
                mssqlContext.Database.ExecuteSqlCommand("UPDATE Expenses SET IsExportedToMysql = 1 WHERE IsExportedToMysql = 0");
                mssqlContext.Database.ExecuteSqlCommand("UPDATE SupermarketProducts SET IsExportedToMysql = 1 WHERE IsExportedToMysql = 0");

                mysqlContext.SaveChanges();

                this.Engine.Output.AppendLine(Messages.MysqDataTransferSuccess);
            }
        }
    }
}