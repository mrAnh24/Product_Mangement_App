using DatabaseApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    [Table("Products")]
    public class Products //: ProductBase
    {
        public string Product { get; set; }
        [Key]
        public string ProductCode { get; set; }
        public string Description {  get; set; }
        public double Price { get; set; }

        //public Products(string product, string productCode, string description, double price)
        //{
        //    Product = product;
        //    ProductCode = productCode;
        //    Description = description;
        //    Price = price;
        //}
    }
}
