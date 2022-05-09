using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassesStudentsAPI.Models
{
    public class ClassesStudentsAPIContext : DbContext
    {
        public ClassesStudentsAPIContext(DbContextOptions<ClassesStudentsAPIContext> options) : base(options)
        {
        }

        public DbSet<Trida> Tridy { get; set; }

        public DbSet<Student> Studenti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Student>(b =>
            {


                b.HasOne<Trida>()    
                    .WithMany()       
                    .HasForeignKey(c => c.TridaId);



            });
        }
    }
}
