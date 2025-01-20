using Microsoft.AspNetCore.Identity;
using XiehouYu.Api.Data.Entities;

namespace XiehouYu.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<GameProgress> GameProgresses { get; set; } = new List<GameProgress>();
    }
} 