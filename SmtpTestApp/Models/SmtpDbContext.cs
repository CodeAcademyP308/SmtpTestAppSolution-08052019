using SmtpTestApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmtpTestApp.Models
{
    public class SmtpDbContext : DbContext
    {
        public SmtpDbContext()
            :base("name=cString")
        {

        }

        public DbSet<Subscriber> Subscribers { get; set; }
    }
}