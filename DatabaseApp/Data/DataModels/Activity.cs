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
    internal class Activity
    {
        [Key]
        public string Username { get; set; }
        public string Role { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
