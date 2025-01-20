using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace XiehouYu.Admin.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // 添加导航属性
        public virtual ICollection<GameProgress> GameProgresses { get; set; } = new List<GameProgress>();
        public virtual ICollection<UserReward> Rewards { get; set; } = new List<UserReward>();
    }
} 