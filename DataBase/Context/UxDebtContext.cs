using DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Context
{
    public class UxDebtContext : DbContext
    {
        public UxDebtContext(DbContextOptions<UxDebtContext> options)
            : base(options)
        {

        }


        public DbSet<Issue> Issues { get; set; }
        public DbSet<Repository> Repositorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().ToTable("Issue");
            modelBuilder.Entity<Repository>().ToTable("Repository");
        }
    }
}