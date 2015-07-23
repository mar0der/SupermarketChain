namespace MSSQLModels.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MSSQLDB;

    using SupermarketChain.MSSQL.Models;

    #endregion

    public class Vendor
    {
        private ICollection<Product> products;

        private ICollection<Expense> expenses; 

        public Vendor()
        {
            this.products = new HashSet<Product>();
            this.expenses = new HashSet<Expense>();

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

        public virtual ICollection<Expense> Expenses
        {
            get
            {
                return this.expenses;
            }

            set
            {
                this.expenses = value;
            }
        }
    }
}