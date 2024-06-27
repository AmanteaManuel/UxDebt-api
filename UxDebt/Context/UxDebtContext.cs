using UxDebt.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using UxDebt.Models.Entities;

namespace UxDebt.Context
{
    public class UxDebtContext : DbContext
    {
        public UxDebtContext(DbContextOptions<UxDebtContext> options)
            : base(options)
        {

        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<IssueTag> IssueTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().ToTable("Issue");
            modelBuilder.Entity<Issue>(entity => {
                entity.HasIndex(e => e.GitId).IsUnique();
            });

            modelBuilder.Entity<Repository>().ToTable("Repository");
            modelBuilder.Entity<Repository>()
            .HasMany(r => r.Issues)
            .WithOne(i => i.Repository)
            .HasForeignKey(i => i.RepositoryId); 

            modelBuilder.Entity<Repository>(entity => {
                entity.HasIndex(e => e.GitId).IsUnique();                
            });

            modelBuilder.Entity<IssueTag>()
               .HasKey(it => new { it.IssueId, it.TagId });

            modelBuilder.Entity<IssueTag>()
                .HasOne(it => it.Issue)
                .WithMany(i => i.IssueTags)
                .HasForeignKey(it => it.IssueId);

            modelBuilder.Entity<IssueTag>()
                .HasOne(it => it.Tag);

        }
    }
}