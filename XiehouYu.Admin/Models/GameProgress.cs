using System;

namespace XiehouYu.Admin.Models
{
    public class GameProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime PlayedAt { get; set; }
        public bool IsCorrect { get; set; }
        public int TimeTaken { get; set; }
        public int QuestionId { get; set; }
        
        public virtual Question? Question { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
} 