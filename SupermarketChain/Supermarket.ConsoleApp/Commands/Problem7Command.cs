namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using MSSQLDB;

    using MSSQLModels.Models;

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
                var allNewVendors =
                    msdb.Vendors.Where(v => v.IsExportedToMysql == false)
                        .Select(v => new { v.Id, v.VendorName, v.IsExportedToMysql })
                        .ToList();
                var allNewProducts =
                    msdb.Products.Where(v => v.IsExportedToMysql == false)
                        .Select(p => new { p.Id, p.ProductName, p.VendorId, p.Price })
                        .ToList();
                var allNewExpenses =
                    msdb.Expenses.Where(v => v.IsExportedToMysql == false)
                        .Select(e => new { e.Id, e.Amount, e.DateTime, e.VendorId })
                        .ToList();
                var allNewVendorProducts =
                    msdb.SupermarketProducts.Where(v => v.IsExportedToMysql == false)
                        .Select(sp => new { sp.ProductId, sp.Product.VendorId, sp.Quantity, sp.SaleDate, sp.UnitPrice })
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

                //cuz i can`t do it smarter than that 
                msdb.Database.ExecuteSqlCommand("UPDATE Vendors SET IsExportedToMysql = 1 WHERE IsExportedToMysql = 0");
                msdb.Database.ExecuteSqlCommand("UPDATE Products SET IsExportedToMysql = 1 WHERE IsExportedToMysql = 0");
                msdb.Database.ExecuteSqlCommand("UPDATE Expenses SET IsExportedToMysql = 1 WHERE IsExportedToMysql = 0");
                msdb.Database.ExecuteSqlCommand("UPDATE SupermarketProducts SET IsExportedToMysql = 1 WHERE IsExportedToMysql = 0");

                mydb.SaveChanges();

                this.Engine.Output.AppendLine(Messages.MysqDataTransferSuccess);
            }
        }
    }
}