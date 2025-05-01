using DAL__Data_Access_Layer_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL__Data_Access_Layer_.AppContext
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<IssueRecord> IssueRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                       .HasIndex(b => b.ISBN)
                       .IsUnique();

            // Configure relationships explicitly
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<IssueRecord>()
                .HasOne(ir => ir.User)
                .WithMany(u => u.IssueRecords)
                .HasForeignKey(ir => ir.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<IssueRecord>()
                .HasOne(ir => ir.Book)
                .WithMany(b => b.IssueRecords)
                .HasForeignKey(ir => ir.BookId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Cyper" },
            new Category { Id = 2, Name = "Ai" },
            new Category { Id = 3, Name = "Bio" },
            new Category { Id = 4, Name = "History" },
            new Category { Id = 5, Name = "Tech" }
            );
            }
    } 
}
