namespace SupermarketChain.MSSQL.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MSSQLModels.Models;

    public class Expense
    {
        public Expense()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("Vendor")]
        public int VendorId { get; set; }
        
        public virtual Vendor Vendor { get; set; } 
    }
}