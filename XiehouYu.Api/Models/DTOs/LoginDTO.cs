using System.ComponentModel.DataAnnotations;

namespace XiehouYu.Api.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "用户名是必需的")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "密码是必需的")]
        public string Password { get; set; } = string.Empty;
    }
} 