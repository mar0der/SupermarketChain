namespace SupermarketChainMain
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class SupermarketChainMain
    {
        public static void Main(string[] args)
        {
            using (var db = new Supermarket())
            {
                Console.WriteLine("Enter Vendor Name:");
                var vendorName = Console.ReadLine();
                var newVendor = new Vendor { VendorName = vendorName };
                db.Vendors.Add(newVendor);
                db.SaveChanges();
                //show all vendors
                var query = from v in db.Vendors orderby v.VendorName select v;
                foreach (var vendor in query)
                {
                    Console.WriteLine(vendor.VendorName);
                }
            }
        }
    }
}
