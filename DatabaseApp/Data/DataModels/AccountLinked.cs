using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    [Table("AccountLinked")]
    public class AccountLinked
    {
        public string AccountID { get; set; }
        [Key]
        public string Username { get; set; }
        public string Apple { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Github { get; set; }
        public double NotifyCount { get; set; }
    }
}
