namespace XiehouYu.Api.Models
{
    public class UserStatsModel
    {
        public int TotalScore { get; set; }
        public int TotalGames { get; set; }
        public int CorrectAnswers { get; set; }
        public double AccuracyRate { get; set; }
        public List<GameHistoryModel> RecentGames { get; set; } = new();
    }

    public class GameHistoryModel
    {
        public string Question { get; set; } = string.Empty;
        public string UserAnswer { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public DateTime PlayedAt { get; set; }
        public int TimeTaken { get; set; }
    }
} 