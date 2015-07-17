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
            using (var db = new MSSQLContext())
            {
                Console.WriteLine("enter vendor name:");
                var vendorname = Console.ReadLine();
                var newvendor = new Vendor { VendorName = vendorname };
                db.Vendors.Add(newvendor);
                db.SaveChanges();

                // show all vendors
                var query = from v in db.Vendors orderby v.VendorName select v;
                foreach (var vendor in query)
                {
                    Console.WriteLine(vendor.VendorName);
                }
            }
        }
    }
}