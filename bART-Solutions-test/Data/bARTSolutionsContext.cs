using bART_Solutions_test.Models;
using Microsoft.EntityFrameworkCore;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bART_Solutions_test.Data
{
    public class bARTSolutionsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Incident> Incidents { get; set; } = null!;

        public bARTSolutionsContext(DbContextOptions<bARTSolutionsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contact>(entity => {
                entity.HasIndex(c => c.Email).IsUnique();
            });
            builder.Entity<Account>(entity => {
                entity.HasIndex(a => a.Name).IsUnique();
            });
            builder.Entity<Incident>(entity => {
                entity.HasIndex(i => i.Name).IsUnique();
            });
        }
    }
}
