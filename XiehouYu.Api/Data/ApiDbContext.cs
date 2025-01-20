using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XiehouYu.Api.Data.Entities;
using XiehouYu.Api.Models;

namespace XiehouYu.Api.Data
{
    public class ApiDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<GameProgress> GameProgress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置实体关系和约束
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Answer).IsRequired();
                entity.Property(e => e.Difficulty).HasMaxLength(50);
                entity.Property(e => e.Category).HasMaxLength(50);
            });

            modelBuilder.Entity<GameProgress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(g => g.User)
                      .WithMany(u => u.GameProgresses)
                      .HasForeignKey(g => g.UserId);
            });
        }
    }
} 