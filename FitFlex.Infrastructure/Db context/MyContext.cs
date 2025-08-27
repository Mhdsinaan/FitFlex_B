using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Domain.Entities.Trainer_model;
using FitFlex.Domain.Entities.Users_Model;
using Microsoft.EntityFrameworkCore;

namespace FitFlex.Infrastructure.Db_context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Trainer> Trainers{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                        .Property(u => u.Role)
                        .HasConversion<string>();
        }


    }

}
