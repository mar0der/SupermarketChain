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
            using (var mssqlContext = new MSSQLContext())
            using (var mysqlContext = new supermarket_chainEntities())
            {
                foreach (var vendor in mssqlContext.Vendors)
                {
                    var mysqlVendor = new vendor { id = vendor.Id, vendor_name = vendor.VendorName };

                    mysqlContext.vendors.AddOrUpdate(v => v.id, mysqlVendor);
                }

                this.Engine.Output.AppendLine(Messages.MysqlNewVendorsAdded);

                foreach (var product in mssqlContext.Products)
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

                foreach (var expense in mssqlContext.Expenses)
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

                foreach (var vendorProduct in mssqlContext.SupermarketProducts.Include("Product"))
                {
                    var mysqlVendorProduct = new vendors_products
                                                 {
                                                     vendor_id = vendorProduct.Product.Vendor.Id,
                                                     product_id = vendorProduct.ProductId,
                                                     quantity = vendorProduct.Quantity,
                                                     sale_date = vendorProduct.SaleDate,
                                                     unit_price = vendorProduct.UnitPrice
                                                 };
                    mysqlContext.vendors_products.AddOrUpdate(
                        vp => new { vp.vendor_id, vp.product_id, vp.sale_date, vp.quantity, vp.unit_price }, 
                        mysqlVendorProduct);
                }

                mysqlContext.SaveChanges();

                this.Engine.Output.AppendLine(Messages.MysqDataTransferSuccess);
            }
        }
    }
}