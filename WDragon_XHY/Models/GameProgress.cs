namespace XiehouYu.Models
{
    public class GameProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public bool IsCorrect { get; set; }
        public int TimeTaken { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 