using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext()
        {
        }

        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(local);uid=quannpm;pwd=admin;database=BookDataBase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PubId);
            });
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.AuthorId);
            });
            modelBuilder.Entity<Book>(entity =>
            {

                entity.Property(t => t.BookId).ValueGeneratedOnAdd();
                entity.HasKey(e => e.BookId);
                entity.HasOne(d => d.Publisher)
                  .WithMany(p => p.Books)
                  .HasForeignKey(d => d.PubId);
            });
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PubId);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasOne(d => d.Publisher)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.PubId);
                entity.HasOne(d => d.Role)
                 .WithMany(p => p.Users)
                 .HasForeignKey(d => d.RoleId);
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
            });
            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.BookId });
                entity.HasOne(d => d.Book)
                 .WithMany(p => p.BookAuthors)
                 .HasForeignKey(d => d.BookId);

                entity.HasOne(d => d.Author)
              .WithMany(p => p.BookAuthors)
              .HasForeignKey(d => d.AuthorId);

            });

        }
    }
}
