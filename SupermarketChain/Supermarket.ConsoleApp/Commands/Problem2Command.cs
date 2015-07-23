using System;
using System.Data.Entity.Migrations;
using System.IO;
using GemBox.Spreadsheet;
using Ionic.Zip;
using SupermarketChain.MSSQL;

namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System.Linq;

    using MSSQLDB;

    using MSSQLModels.Models;

    using OracleDB;

    using Supermarket.ConsoleApp.Constants;
    using Supermarket.ConsoleApp.Interfaces;

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
                //replicating measures
                foreach (var measure in oracleContext.MEASURES)
                {
                    if (!mssqlContext.Measures.Any(m => m.Name == measure.MEASURE_NAME))
                    {
  
                        mssqlContext.Measures.Add(new Measure { Name = measure.MEASURE_NAME });

                        mssqlContext.SaveChanges();
                    }
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlMeasures);

                //replicationg vendors
                foreach (var vendor in oracleContext.VENDORS)
                {
                    if (!mssqlContext.Vendors.Any(v => v.VendorName == vendor.VENDOR_NAME))
                    {
                        mssqlContext.Vendors.Add(new Vendor { VendorName = vendor.VENDOR_NAME });

                        mssqlContext.SaveChanges();
                    }
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlVendors);

                //replicationg products
                foreach (var product in oracleContext.PRODUCTS)
                {
                    if (!mssqlContext.Products.Any(p => p.ProductName == product.PRODUCT_NAME))
                    {
                        mssqlContext.Products.Add(
                            new Product
                            {
                                ProductName = product.PRODUCT_NAME,
                                Price = product.PRICE,
                                MeasureId = product.MEASURE_ID,
                                VendorId = product.VENDOR_ID
                            });

                        mssqlContext.SaveChanges();
                    }
                }

                this.Engine.Output.AppendLine(Messages.Oracle2MssqlProducts);
            }
            this.Engine.Output.AppendLine(Messages.Oracle2MssqlSuccess); 
        }

        private void InsertDataFromExcelFile(string filePath)
        {
            var defaultZipFileName = "Sample-Sales-Reports.zip";

            using (Ionic.Zip.ZipFile zipFile = Ionic.Zip.ZipFile.Read((filePath == null ? defaultZipFileName : filePath)))
            using (var mssqlContext = new MSSQLContext())
            {
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                DateTime theDate = DateTime.Now;
                bool changed = false;

                foreach (ZipEntry zip in zipFile)
                {
                    changed = false;

                    if (zip.IsDirectory)
                    {
                        theDate = DateTime.Parse(zip.FileName.Substring(0, zip.FileName.Length - 1));
                        changed = true;
                    }
                    else
                    {
                        zip.Extract();

                        ExcelFile excelFile = ExcelFile.Load(zip.FileName);

                        string supermarketName = string.Empty;

                        foreach (ExcelWorksheet sheet in excelFile.Worksheets)
                        {
                            supermarketName = (string)sheet.Rows[1].AllocatedCells[1].Value;
                            supermarketName = supermarketName.Substring(13, supermarketName.Length - 14);

                            if (!mssqlContext.Supermarkets.Any(s => s.Name == supermarketName))
                            {
                                mssqlContext.Supermarkets.AddOrUpdate(new SupermarketChain.MSSQL.Models.Supermarket()
                                {
                                    Name = supermarketName
                                });

                                mssqlContext.SaveChanges();
                            }

                            int currentRowsLength = sheet.Rows.Count;

                            foreach (ExcelRow row in sheet.Rows)
                            {
                                if (row.Index > 2 && row.Index < currentRowsLength - 1)
                                {
                                    string productName = (string) row.AllocatedCells[1].Value;
                                    int quantity = row.AllocatedCells[2].IntValue;

                                    mssqlContext.SupermarketProducts.AddOrUpdate(new SupermarketProduct()
                                    {
                                        ProductId = mssqlContext.Products.First(p => p.ProductName == productName).Id,
                                        SupermarketId = mssqlContext.Supermarkets.First(s => s.Name == supermarketName).Id,
                                        Quantity = quantity,
                                        SaleDate = theDate
                                    });

                                    mssqlContext.SaveChanges();
                                }
                            }    
                        }

                        File.Delete(zip.FileName);
                        Directory.Delete(Directory.GetParent(zip.FileName).FullName);
                    }
                }
            }
        }

        public override void Execute()
        {
            this.CopyOracleDataToSqlServer();
            this.InsertDataFromExcelFile((this.Data.Count > 1 ? this.Data.Last() : null));
        }
    }
}