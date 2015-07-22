namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System.Linq;

    using MSSQLDB;

    using MSSQLModels.Models;

    using OracleDB;

    using Supermarket.ConsoleApp.Constants;
    using Supermarket.ConsoleApp.Interfaces;

    #endregion

    public class Problem2Command : AbstractCommand
    {
        public Problem2Command(IEngine engine)
            : base(engine)
        {
        }

        public override void Execute()
        {
            using (var mssqlContext = new MSSQLContext())
            using (var oracleContext = new OracleContext())
            {

                //replicating measures
                foreach (var measure in oracleContext.MEASURES)
                {
                    if (!mssqlContext.Measures.Any(m => m.Name == measure.MEASURE_NAME))
                    {
                        mssqlContext.Measures.Add(new Measure { Name = measure.MEASURE_NAME });

                        mssqlContext.SaveChanges();
                    }
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlMeasures);

                //replicationg vendors
                foreach (var vendor in oracleContext.VENDORS)
                {
                    if (!mssqlContext.Vendors.Any(v => v.VendorName == vendor.VENDOR_NAME))
                    {
                        mssqlContext.Vendors.Add(new Vendor { VendorName = vendor.VENDOR_NAME });

                        mssqlContext.SaveChanges();
                    }
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlVendors);

                //replicationg products
                foreach (var product in oracleContext.PRODUCTS)
                {
                    if (!mssqlContext.Products.Any(p => p.ProductName == product.PRODUCT_NAME))
                    {
                        mssqlContext.Products.Add(
                            new Product
                                {
                                    ProductName = product.PRODUCT_NAME, 
                                    Price = product.PRICE, 
                                    MeasureId = product.MEASURE_ID, 
                                    VendorId = product.VENDOR_ID
                                });

                        mssqlContext.SaveChanges();
                    }
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlProducts);
            }
            this.Engine.Output.AppendLine(Messages.Oracle2MssqlSuccess);
        }
    }
}