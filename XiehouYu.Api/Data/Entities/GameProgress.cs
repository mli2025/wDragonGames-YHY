using XiehouYu.Api.Models;

namespace XiehouYu.Api.Data.Entities
{
    public class GameProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime PlayedAt { get; set; }
        public bool IsCorrect { get; set; }
        public int TimeTaken { get; set; }
        
        public virtual ApplicationUser? User { get; set; }
    }
} 