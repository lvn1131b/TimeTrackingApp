using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingAP.Models
{
    public class Context : DbContext
    {
        //public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Reports> Reports { get; set; }
        //public Context()
        //{
        //    Database.EnsureCreated();
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=");
        }
    }

}
