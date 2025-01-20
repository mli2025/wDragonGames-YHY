namespace XiehouYu.Api.Data.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public virtual ICollection<GameProgress> GameProgresses { get; set; } = new List<GameProgress>();
    }
} 