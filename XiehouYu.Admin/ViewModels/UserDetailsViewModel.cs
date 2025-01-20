using XiehouYu.Admin.Models;

namespace XiehouYu.Admin.ViewModels
{
    public class UserDetailsViewModel
    {
        public ApplicationUser User { get; set; } = null!;

        public GameStatsViewModel? GameStats { get; set; }

        public List<UserReward> RecentRewards { get; set; } = new();
    }

    public class GameStatsViewModel
    {
        public int TotalGames { get; set; }
        public int CorrectAnswers { get; set; }
        public double AverageTime { get; set; }
        public double AccuracyRate => TotalGames > 0 ? (CorrectAnswers * 100.0 / TotalGames) : 0;
    }
} 