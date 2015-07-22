namespace MSSQLModels.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Core.Metadata.Edm;

    using SupermarketChain.MSSQL;
    using SupermarketChain.MSSQL.Models;

    #endregion

    public class Product
    {
        private ICollection<SupermarketProduct> supermarketProducts;

        public Product()
        {
            this.supermarketProducts = new HashSet<SupermarketProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        public int MeasureId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("MeasureId")]
        public virtual Measure Measure { get; set; }

        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }

        public ICollection<SupermarketProduct> SupermarketProducts
        {
            get
            {
                return this.supermarketProducts;
            }

            set
            {
                this.supermarketProducts = value;
            }
        }
    }
}