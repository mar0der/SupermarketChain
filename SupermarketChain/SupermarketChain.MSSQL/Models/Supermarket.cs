namespace SupermarketChain.MSSQL.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class Supermarket
    {
        private ICollection<SupermarketProduct> supermarketProducts;

        public Supermarket()
        {
            this.supermarketProducts = new HashSet<SupermarketProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

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