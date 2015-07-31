namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    using GemBox.Spreadsheet;

    using Ionic.Zip;

    using MSSQLDB;

    using MSSQLModels.Models;

    using OracleDB;

    using Supermarket.ConsoleApp.Constants;
    using Supermarket.ConsoleApp.Interfaces;

    using SupermarketChain.MSSQL;
    using SupermarketChain.MSSQL.Models;

    #endregion

    public class Problem2Command : AbstractCommand
    {
        public Problem2Command(IEngine engine)
            : base(engine)
        {
        }

        private void CopyOracleDataToSqlServer()
        {
            using (var mssqlContext = new MSSQLContext())
            using (var oracleContext = new OracleContext())
            {
                this.Engine.Output.AppendLine(Messages.Delimiter);

                var products = from product in oracleContext.PRODUCTS
                    select new
                    {
                        ProductName = product.PRODUCT_NAME,
                        Price = product.PRICE,
                        MeasureId = product.MEASURE_ID,
                        VendorId = product.VENDOR_ID,
                    };

                // replicating measures
                foreach (var measureName in oracleContext.MEASURES.Select(m => m.MEASURE_NAME))
                {
                    mssqlContext.Measures.AddOrUpdate(m => m.Name, new Measure { Name = measureName });

                    mssqlContext.SaveChanges();
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlMeasures);

                // replicationg vendors
                foreach (var vendorName in oracleContext.VENDORS.Select(v => v.VENDOR_NAME))
                {
                    mssqlContext.Vendors.AddOrUpdate(v => v.VendorName, new Vendor { VendorName = vendorName });

                    mssqlContext.SaveChanges();                   
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlVendors);

                // replicationg products
                foreach (var product in products)
                {
                    mssqlContext.Products.AddOrUpdate(p => p.ProductName,
                        new Product
                            {
                                ProductName = product.ProductName, 
                                Price = product.Price, 
                                MeasureId = product.MeasureId, 
                                VendorId = product.VendorId
                            });

                    mssqlContext.SaveChanges();
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlProducts);
            }

            this.Engine.Output.AppendLine(Messages.Oracle2MssqlSuccess);
        }

        private void InsertDataFromExcelFile(string filePath)
        {
            var defaultZipFileName = "../../Input/Sample-Sales-Reports.zip";

            using (var zipFile = ZipFile.Read(filePath == null ? defaultZipFileName : filePath))
            using (var mssqlContext = new MSSQLContext())
            {
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                DateTime currentDate = DateTime.Now;

                foreach (var zip in zipFile)
                {
                    if (zip.IsDirectory)
                    {
                        currentDate = DateTime.Parse(zip.FileName.Substring(0, zip.FileName.Length - 1));
                    }
                    else
                    {
                        zip.Extract();

                        var excelFile = ExcelFile.Load(zip.FileName);

                        string supermarketName = string.Empty;

                        foreach (var worksheet in excelFile.Worksheets)
                        {
                            supermarketName = worksheet.Rows[1].AllocatedCells[1].StringValue;
                            supermarketName = supermarketName.Substring(13, supermarketName.Length - 14);

                            mssqlContext.Supermarkets.AddOrUpdate(s => s.Name,
                                new Supermarket
                                {
                                    Name = supermarketName
                                });

                            mssqlContext.SaveChanges();

                            int currentRowsLength = worksheet.Rows.Count;

                            foreach (var row in worksheet.Rows)
                            {
                                if (row.Index > 2 && row.Index < currentRowsLength - 1)
                                {
                                    string productName = row.AllocatedCells[1].StringValue;
                                    int quantity = row.AllocatedCells[2].IntValue;
                                    decimal unitPrice = decimal.Parse(row.AllocatedCells[3].DoubleValue.ToString());

                                    mssqlContext.SupermarketProducts.AddOrUpdate(
                                        new SupermarketProduct
                                            {
                                                ProductId =
                                                    mssqlContext.Products.First(
                                                        p => p.ProductName == productName).Id, 
                                                SupermarketId =
                                                    mssqlContext.Supermarkets.First(
                                                        s => s.Name == supermarketName).Id, 
                                                Quantity = quantity,
                                                SaleDate = currentDate, 
                                                UnitPrice = unitPrice
                                            });

                                    mssqlContext.SaveChanges();
                                }
                            }
                        }

                        File.Delete(zip.FileName);
                        Directory.Delete(Directory.GetParent(zip.FileName).FullName);
                    }
                }

                this.Engine.Output.AppendLine(Messages.ExcelDataLoadedSuccess);
            }
        }

        public override void Execute()
        {
            this.CopyOracleDataToSqlServer();
            this.InsertDataFromExcelFile(this.Data.Count > 1 ? this.Data.Last() : null);
        }
    }
}