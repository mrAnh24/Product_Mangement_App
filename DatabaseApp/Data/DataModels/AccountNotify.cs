using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    [Table("AccountNotify")]
    internal class AccountNotify
    {
        public int No { get; set; }
        [Key]
        public string NotifyID { get; set; }
        public string AccountID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Display { get; set; }
        public string Details { get; set; }
        public string Category { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
