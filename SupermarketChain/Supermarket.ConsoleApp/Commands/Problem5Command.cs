using System;
using System.IO;
using System.Linq;
using Supermarket.ConsoleApp.Interfaces;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MSSQLDB;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Supermarket.ConsoleApp.Commands
{
    public class Problem5Command : AbstractCommand
    {
        public Problem5Command(IEngine engine) : base(engine)
        {
        }

        public override void Execute()
        {
            using (var mssqlContext = new MSSQLContext())
            {
                var client = new MongoClient("mongodb://localhost:27017");

                var database = client.GetDatabase("test");

                database.CreateCollectionAsync("SalesByProductReports");

                var theCollection = database.GetCollection<BsonDocument>("SalesByProductReports");

                var salesByProduct = from supermarketProduct in mssqlContext.SupermarketProducts
                    group supermarketProduct by supermarketProduct.ProductId;

                if (!Directory.Exists("Json-Reports"))
                {
                    Directory.CreateDirectory("Json-Reports");
                }

                foreach (var productSale in salesByProduct)
                {
                    var theProduct = mssqlContext.Products.Find(productSale.Key);
                    int totalQuantitySoldSum = 0;
                    decimal totalIncomesSum = 0;

                    foreach (var record in productSale)
                    {
                        totalQuantitySoldSum += record.Quantity;
                        totalIncomesSum += record.Quantity * record.UnitPrice;
                    }

                    var jsonProduct = new
                    {
                        productId = theProduct.Id,
                        productName = theProduct.ProductName,
                        vendorName = theProduct.Vendor.VendorName,
                        totalQuantitySold = totalQuantitySoldSum,
                        totalIncomes = totalIncomesSum
                    };

                    string jsonString = JsonConvert.SerializeObject(jsonProduct);

                    StreamWriter streamWriter = new StreamWriter("Json-Reports\\" + theProduct.Id + ".json");

                    streamWriter.Write(jsonString);

                    streamWriter.Close();

                    theCollection.InsertOneAsync(new BsonDocument()
                    {
                        {"product-id", theProduct.Id},
                        {"product-name", theProduct.ProductName},
                        {"vendor-name", theProduct.Vendor.VendorName},
                        {"total-quantity-sold", totalQuantitySoldSum.ToString()},
                        {"total-incomes", totalIncomesSum.ToString()}
                    });
                }
            }
        }
    }
}
