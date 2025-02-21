using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    [Table("CustomerList")]
    public class CustomerList
    {
        [Key]
        public string OrderID { get; set; }
        public string AccountID { get; set; }
        public string Username { get; set; }
        public string Product { get; set; }
        public string ProductCode { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
