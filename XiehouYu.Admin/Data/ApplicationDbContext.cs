using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XiehouYu.Admin.Models;

namespace XiehouYu.Admin.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // 移除这行，我们会在程序启动时单独处理数据库初始化
            // Database.EnsureCreated();
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<GameProgress> GameProgress { get; set; }
        public DbSet<UserReward> UserRewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置 Question 表
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Questions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Answer).IsRequired();
                entity.Property(e => e.Difficulty).HasMaxLength(50);
                entity.Property(e => e.Category).HasMaxLength(50);
            });

            // 配置关系
            modelBuilder.Entity<GameProgress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(g => g.User)
                      .WithMany(u => u.GameProgresses)
                      .HasForeignKey(g => g.UserId);
                entity.HasOne(g => g.Question)
                      .WithMany(q => q.GameProgresses)
                      .HasForeignKey(g => g.QuestionId);
            });

            modelBuilder.Entity<UserReward>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rewards)
                .HasForeignKey(r => r.UserId);
        }
    }
} 