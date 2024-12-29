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
        public DbSet<Address> Address { get; set; }        

        public TutorlyDbContext(DbContextOptions options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator(r => r.Role)
                .HasValue<Tutor>(Role.Tutor)
                .HasValue<Student>(Role.Student);


            modelBuilder.Entity<Tutor>()
                .HasMany(p => p.Posts)
                .WithOne(t => t.Tutor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostsStudents>(ps =>
            {
                ps.HasKey(ps => new { ps.PostId, ps.StudentId });

                ps.HasOne(c => c.Student)
                .WithMany(r => r.Posts)
                .HasForeignKey(k => k.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

                ps.HasOne(c => c.Post)
               .WithMany(r => r.Students)
               .HasForeignKey(k => k.PostId)
               .OnDelete(DeleteBehavior.Cascade);
                    
            });
               
          
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Address)
                .WithOne(a => a.Post)
                .OnDelete(DeleteBehavior.Cascade); 



            modelBuilder.Entity<Post>()
                .HasOne(a => a.Address)
                .WithOne(p => p.Post);

            modelBuilder.Entity<Address>(a =>
            {
                a.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(15);

                a.Property(s => s.Street)
                .IsRequired()
                .HasMaxLength(20);

                a.Property(n => n.Number)
                .IsRequired()
                .HasMaxLength(10);

            });

            modelBuilder.Entity<Category>()
                .Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(30);

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
