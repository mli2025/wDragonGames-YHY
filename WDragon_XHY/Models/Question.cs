namespace XiehouYu.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string FirstHalf { get; set; }    // 歇后语上半部分
        public required string SecondHalf { get; set; }   // 歇后语下半部分
        public DifficultyLevel Difficulty { get; set; }
        public required string Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
} 