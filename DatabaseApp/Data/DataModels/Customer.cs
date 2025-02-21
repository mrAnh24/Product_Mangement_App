using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    [Table("Customer")]
    public class Customer
    {
        public int No { get; set; }
        [Key]
        public string CustomerID { get; set; }
        public string AccountID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string InputName { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PaymentMethod { get; set; }
        public double Bill { get; set; }
        public string CouponCode { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}