namespace SupermarketChainMain
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Oracle.ManagedDataAccess.Client;
    using Oracle.ManagedDataAccess.Types;
    using global::SupermarketChainMain.CoreLogic;

    public class SupermarketChainMain
    {
        public static void Main()
        {

            //string oracleDB = "Data Source=ORCL;User Id=supermarket;Password=123;";
            //OracleConnection oracleConnection = new OracleConnection(oracleDB);
            //oracleConnection.Open();

            //OracleCommand oracleCommand = new OracleCommand();
            //oracleCommand.Connection = oracleConnection;
            //oracleCommand.CommandText = "SELECT * FROM PRODUCTS";
            //oracleCommand.CommandType = CommandType.Text;

            //OracleDataReader oracleDataReader = oracleCommand.ExecuteReader();
            //oracleDataReader.Read();

            //Console.WriteLine(oracleDataReader.GetString(0));

            //oracleConnection.Close();

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
