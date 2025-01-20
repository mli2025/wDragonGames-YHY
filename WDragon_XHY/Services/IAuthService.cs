namespace XiehouYu.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string username, string email, string password);
        Task<string> LoginAsync(string email, string password);
        Task<bool> ResetPasswordAsync(string email);
        Task<bool> ChangePasswordAsync(string currentPassword, string newPassword);
        Task LogoutAsync();
    }
} 