using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    [Table("ActivityLog")]
    public partial class Activity
    {
        public int No { get; set; }
        [Key]
        public string ActivityID { get; set; }
        public string AccountID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
