namespace SupermarketChain.MSSQL
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MSSQLModels.Models;

    using SupermarketChain.MSSQL.Models;

    #endregion

    public class SupermarketProduct
    {
 
        [Key, Column(Order = 0)]
        [ForeignKey("Supermarket")]
        public int SupermarketId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Supermarket Supermarket { get; set; }

        public virtual Product Product { get; set; }

        public DateTime SaleDate { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}