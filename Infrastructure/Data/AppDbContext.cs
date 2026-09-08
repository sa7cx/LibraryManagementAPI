using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Identity;
namespace LibraryManagementAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .HasQueryFilter(b => !b.IsDeleted);
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<Member> Members { get; set; }

    }
}
