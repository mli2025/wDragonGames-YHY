namespace XiehouYu.Api.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string Difficulty { get; set; } = "medium";
        public string Category { get; set; } = string.Empty;
    }
} 