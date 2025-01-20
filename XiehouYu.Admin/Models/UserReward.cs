using System;

namespace XiehouYu.Admin.Models
{
    public class UserReward
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string RewardType { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public virtual ApplicationUser? User { get; set; }
    }
} 