using System.ComponentModel.DataAnnotations;

namespace XiehouYu.Admin.ViewModels
{
    public class LeaderboardViewModel
    {
        public string UserId { get; set; } = string.Empty;

        [Display(Name = "用户名")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "总分")]
        public int TotalScore { get; set; }

        [Display(Name = "正确答题数")]
        public int CorrectAnswers { get; set; }

        [Display(Name = "总答题数")]
        public int TotalQuestions { get; set; }

        [Display(Name = "平均用时(秒)")]
        public double AverageTime { get; set; }

        [Display(Name = "正确率")]
        public double AccuracyRate => TotalQuestions > 0 
            ? (double)CorrectAnswers / TotalQuestions * 100 
            : 0;
    }
} 