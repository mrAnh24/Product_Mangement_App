﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data.DataModels
{
    [Table("AccountTest")]
    public partial class AccountTest
    {
        [Key]
        public string AccountID {  get; set; }
        public string Username { get; set; }
        public string Email { get; set; }      
        public string Password { get; set; }
        public string Role { get; set; }
        public string PhoneNumbers { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
