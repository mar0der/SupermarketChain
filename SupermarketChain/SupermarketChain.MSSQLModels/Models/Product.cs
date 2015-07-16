namespace MSSQLModels.Models
{
    #region

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class Product
    {
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
    }
}