using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    public class CustomerList
    {
        public string Product { get; set; }
        public string ProductCode { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }

        public CustomerList(string product, string productCode, double price, int amount)
        {
            Product = product;
            ProductCode = productCode;
            Price = price;
            Amount = amount;
        }
    }
}
