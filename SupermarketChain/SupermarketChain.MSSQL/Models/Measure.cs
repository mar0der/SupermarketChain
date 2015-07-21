namespace MSSQLModels.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MSSQLDB;

    #endregion

    public class Measure
    {
        private ICollection<Product> products;

        public Measure()
        {
            this.products = new List<Product>();

            using (var context = new MSSQLContext())
            {
                this.Id = (context.Measures.Any() ? context.Measures.Max(m => m.Id) + 100 : 100);
            }
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
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