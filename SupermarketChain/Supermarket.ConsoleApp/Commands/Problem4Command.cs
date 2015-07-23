using System;
using System.Linq;
using System.Xml;
using MSSQLDB;
using Supermarket.ConsoleApp.Interfaces;

namespace Supermarket.ConsoleApp.Commands
{
    public class Problem4Command : AbstractCommand
    {
        public Problem4Command(IEngine engine) : base(engine)
        {
        }
        public override void Execute()
        {
            using (var mssqlContext = new MSSQLContext())
            using (XmlWriter xmlWriter = XmlWriter.Create("Sales-by-Vendors-Report.xml"))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("sales");

                var salesByVendors = from supermarketProduct in mssqlContext.SupermarketProducts
                    group supermarketProduct by supermarketProduct.Product.VendorId;


                foreach (var sale in salesByVendors)
                {
                    var vendorName = mssqlContext.Vendors.Find(sale.Key).VendorName;
                    xmlWriter.WriteStartElement("sale");
                    xmlWriter.WriteAttributeString("vendor", vendorName); 

                    foreach (var record in sale)
                    {
                        xmlWriter.WriteStartElement("summary");
                        xmlWriter.WriteAttributeString("date", record.SaleDate.ToString("dd-MMMM-yyyy"));
                        xmlWriter.WriteAttributeString("total-sum", (record.Quantity * record.UnitPrice).ToString()); 
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                }
                
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }
    }
}
