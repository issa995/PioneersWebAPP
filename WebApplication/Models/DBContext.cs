using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DBContext :DbContext
    {
        public DBContext() : base("DefaultConnection")
        {

        }
        public DbSet<Information> Informations { get; set; }

    }
}