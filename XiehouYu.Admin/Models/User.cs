using System.ComponentModel.DataAnnotations;

namespace XiehouYu.Admin.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public int Coins { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public virtual ICollection<GameProgress> GameProgresses { get; set; } = new List<GameProgress>();
        public virtual ICollection<UserReward> Rewards { get; set; } = new List<UserReward>();
    }
} 