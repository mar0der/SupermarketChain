namespace oracledb
{
    #region

    using System;
    using System.Linq;

    using OracleDB;

    #endregion

    public class OracleMain
    {
        public static void Main(string[] args)
        {
            using (var db = new OracleModel())
            {
                Console.WriteLine("Enter Vendor Name:");
                var vendorName = Console.ReadLine();
                //var newVendor = new VENDOR { VENDOR_NAME = vendorName };
                //db.Database.ExecuteSqlCommand(
                //    "INSERT INTO PRODUCTS (ID,VENDOR_ID, PRODUCT_NAME, MEASURE_ID,PRICE) VALUES (PRODUCT_SEQUENCE.NEXTVAL, 30, 'Vodka “Targovishte”', 100, 1.20);");
                ////db.VENDORS.Add(newVendor);
                //db.SaveChanges();

                // show all vendors
                var query = from v in db.VENDORS orderby v.VENDOR_NAME select v;
                foreach (var vendor in query)
                {
                    Console.WriteLine(vendor.VENDOR_NAME);
                }
            }
        }
    }
}