using LibrarySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(r => r.UserRoleID);

            modelBuilder.Entity<UserAccount>()
                .HasKey(u => u.UserAccountID);

            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.UserRole)
                .WithMany(r => r.UserAccounts)
                .HasForeignKey(u => u.UserRoleID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
