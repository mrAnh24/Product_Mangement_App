using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    internal class CustomerInvoice
    {
        public string Name { get; set; }
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
        public string Bill { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
