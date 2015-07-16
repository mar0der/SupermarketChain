
namespace SupermarketChainMain
{

    using System;
    using MSSQLDB;

    using MSSQLModels.Models;

    public class SupermarketChainMain
    {
        public static void Main()
        {

            using (var db = new MSSQLContext())
            {
                Console.WriteLine("Enter Vendor Name:");
                //var vendorName = Console.ReadLine();
                //var newVendor = new Vendor { VendorName = vendorName };
                //db.Vendors.Add(newVendor);
                //db.SaveChanges();

                //// show all vendors
                //var query = from v in db.Vendors orderby v.VendorName select v;
                //foreach (var vendor in query)
                //{
                //    Console.WriteLine(vendor.VendorName);
                //}
            }
        }
    }
}
