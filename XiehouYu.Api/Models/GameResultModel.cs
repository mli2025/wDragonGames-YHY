namespace XiehouYu.Api.Models
{
    public class GameResultModel
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int TimeTaken { get; set; }
    }
} 