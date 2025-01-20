namespace XiehouYu.Admin.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string Difficulty { get; set; } = "medium";
        public string Category { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<GameProgress> GameProgresses { get; set; } = new List<GameProgress>();
    }
} 