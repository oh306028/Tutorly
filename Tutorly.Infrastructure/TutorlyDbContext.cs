using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Domain.Models;

namespace Tutorly.Infrastructure
{
    public class TutorlyDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(r => r.Role)
                .HasValue<Tutor>(Role.Tutor)
                .HasValue<Student>(Role.Student);

            modelBuilder.Entity<Post>()
                .HasMany(s => s.Students)
                .WithOne(p => p.Post)
                .HasForeignKey(k => k.PostId);


            modelBuilder.Entity<Post>()
                .HasOne(c => c.Category)
                .WithOne(p => p.Post);



            modelBuilder.Entity<User>(u =>
            {
                u.Property(fN => fN.FirstName)
                .IsRequired()
                .HasMaxLength(15);   

                u.Property(lN => lN.LastName)
                .IsRequired()
                .HasMaxLength(20);

                u.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30);

                u.Property(h => h.PasswordHash)
                .IsRequired();

                u.Property(r => r.Role)
                 .IsRequired();

            });
        }
    }
}
