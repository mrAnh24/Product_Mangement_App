using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    [Table("CustomerOrder")]
    public class CustomerOrder
    {
        [Key]
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryPartner { get; set; }
        public string DeliveryMethod { get; set; }
        public string Vehicle { get; set; }
    }
}
