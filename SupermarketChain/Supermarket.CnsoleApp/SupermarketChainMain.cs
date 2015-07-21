namespace Supermarket.CnsoleApp
{
    #region

    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    using MSSQLDB;

    using MSSQLModels.Models;

    using OracleDB;
    #endregion

    internal class SupermarketChainMain
    {
        private static void Main()
        {
            using (var mssqlContext = new MSSQLContext())
            using (var oracleContext = new OracleContext())
            {
                foreach (var measure in oracleContext.MEASURES)
                {
                    if (!mssqlContext.Measures.Any(m => m.Name == measure.MEASURE_NAME))
                    {
                        mssqlContext.Measures.Add(new Measure()
                        {
                            Name = measure.MEASURE_NAME
                        });

                        mssqlContext.SaveChanges();   
                    }
                }

                foreach (var vendor in oracleContext.VENDORS)
                {
                    if (!mssqlContext.Vendors.Any(v => v.VendorName == vendor.VENDOR_NAME))
                    {
                        mssqlContext.Vendors.Add(new Vendor()
                        {
                            VendorName = vendor.VENDOR_NAME
                        });

                        mssqlContext.SaveChanges();
                    }
                }

                foreach (var product in oracleContext.PRODUCTS)
                {
                    if (!mssqlContext.Products.Any(p => p.ProductName == product.PRODUCT_NAME))
                    {
                        mssqlContext.Products.Add(new Product()
                        {
                            ProductName = product.PRODUCT_NAME,
                            Price = product.PRICE,
                            MeasureId = product.MEASURE_ID,
                            VendorId = product.VENDOR_ID
                        });

                        mssqlContext.SaveChanges();
                    }
                }
            }
        }
    }
}