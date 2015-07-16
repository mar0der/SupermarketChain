namespace MSSQLModels.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class Vendor
    {
        private ICollection<Product> products;

        public Vendor()
        {
            this.products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string VendorName { get; set; }

        public virtual ICollection<Product> Products
        {
            get
            {
                return this.products;
            }

            set
            {
                this.products = value;
            }
        }
    }
}