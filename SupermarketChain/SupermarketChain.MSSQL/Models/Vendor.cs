namespace MSSQLModels.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MSSQLDB;
    #endregion

    public class Vendor
    {
        private ICollection<Product> products;

        public Vendor()
        {
            this.products = new List<Product>();

            using (var context = new MSSQLContext())
            {
                this.Id = (context.Vendors.Any() ? context.Vendors.Max(v => v.Id) + 10 : 10);
            }
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
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