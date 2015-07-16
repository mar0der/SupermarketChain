namespace MSSQLModels.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class Measure
    {
        private ICollection<Product> products;

        public Measure()
        {
            this.products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

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