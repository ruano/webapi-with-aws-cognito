using Microsoft.EntityFrameworkCore;

namespace GetUserinfo.Lambda.Data
{
    public class PoliciesContext : DbContext
    {
        public PoliciesContext(DbContextOptions<PoliciesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .ToTable("Users")
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Permissions)
                .WithOne()
                .HasForeignKey(x => x.UserId);

            modelBuilder
                .Entity<Permission>()
                .ToTable("Permissions")
                .HasKey(x => x.Id);
        }

        public DbSet<User> Users { get; set; }
    }
}
