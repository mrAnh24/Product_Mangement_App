﻿using DatabaseApp.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp.Data
{
    internal class CustomerDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=dbdemo;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
