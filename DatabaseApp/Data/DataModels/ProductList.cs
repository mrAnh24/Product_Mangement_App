using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    [Table("ProductLists")]
    public class ProductLists
    {       
        [Key]
        public string ProductCode { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime TimeCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime TimeModified { get; set; }
        public double SalePercent { get; set; }
    }

    // Type: Meat, Dairy, Vegetable, Drink, Fruit, Dessert, Snack, Other

    // Status : Available, Unavailable, Sold Out, Discontinue, On sale
}
