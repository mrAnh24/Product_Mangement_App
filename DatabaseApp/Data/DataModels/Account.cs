using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseApp.Data.DataModels
{
    [Table("Account")]
    public partial class Accounts
    {
        [Key]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int PhoneNumbers { get; set; }
        public string Gender { get; set; }

    }

    public enum Role
    {
        admin, Lv1, Lv2, Lv3, Lv4, Lv5
    }

    public enum Gender
    {
        Male, Female, Unknown
    }
}
