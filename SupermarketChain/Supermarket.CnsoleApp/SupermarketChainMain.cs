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
            //using (var db = new MSSQLContext())
            //{
            //    Console.WriteLine("Enter Vendor Name:");
            //    var vendorName = Console.ReadLine();
            //    var newVendor = new Vendor { VendorName = vendorName };
            //    db.Vendors.Add(newVendor);
            //    db.SaveChanges();

            //    // show all vendors
            //    var query = from v in db.Vendors orderby v.VendorName select v;
            //    foreach (var vendor in query)
            //    {
            //        Console.WriteLine(vendor.VendorName);
            //    }
            //}

            using (var odb = new OracleContext())
            {
                var vendors = odb.VENDORS;

                foreach (var vendor in vendors)
                {
                    Console.WriteLine(vendor.VENDOR_NAME);
                }
            }
        }
    }
}