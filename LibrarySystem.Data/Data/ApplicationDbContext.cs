using LibrarySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<BookDatabase> BookDatabase { get; set; }
        public DbSet<BorrowedBook> BorrowedBook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(r => r.UserRoleID);

            modelBuilder.Entity<UserAccount>()
                .HasKey(u => u.UserAccountID);

            modelBuilder.Entity<BookDatabase>()
                .HasKey(b => b.BookDatabaseID);

            modelBuilder.Entity<BorrowedBook>()
                .HasKey(b => b.BorrowedBookID);

            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.UserRole)
                .WithMany(r => r.UserAccounts)
                .HasForeignKey(u => u.UserRoleID);

            modelBuilder.Entity<BorrowedBook>()
                .HasOne(u => u.UserAccount)
                .WithMany(r => r.BorrowedBooks)
                .HasForeignKey(u => u.UserAccountID);

            modelBuilder.Entity<BorrowedBook>()
                .HasOne(u => u.BookDatabase)
                .WithMany(r => r.BorrowedBooks)
                .HasForeignKey(u => u.BookDatabaseID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
