using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using XiehouYu.Models;

namespace XiehouYu.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<GameProgress> GameProgress { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置关系
            modelBuilder.Entity<GameProgress>()
                .HasOne(gp => gp.User)
                .WithMany()
                .HasForeignKey(gp => gp.UserId);

            modelBuilder.Entity<GameProgress>()
                .HasOne(gp => gp.Question)
                .WithMany()
                .HasForeignKey(gp => gp.QuestionId);

            // 添加示例歇后语数据
            modelBuilder.Entity<Question>().HasData(
                new Question 
                { 
                    Id = 1, 
                    FirstHalf = "望梅止渴", 
                    SecondHalf = "想梅子酸", 
                    Difficulty = DifficultyLevel.Easy, 
                    Category = "生活", 
                    CreatedAt = DateTime.UtcNow 
                },
                new Question 
                { 
                    Id = 2, 
                    FirstHalf = "一分耕耘", 
                    SecondHalf = "一分收获", 
                    Difficulty = DifficultyLevel.Easy, 
                    Category = "生活", 
                    CreatedAt = DateTime.UtcNow 
                },
                new Question 
                { 
                    Id = 3, 
                    FirstHalf = "画蛇添足", 
                    SecondHalf = "多此一举", 
                    Difficulty = DifficultyLevel.Medium, 
                    Category = "成语", 
                    CreatedAt = DateTime.UtcNow 
                }
            );
        }
    }
} 