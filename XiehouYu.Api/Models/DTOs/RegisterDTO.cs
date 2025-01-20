using System.ComponentModel.DataAnnotations;

namespace XiehouYu.Api.Models.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "用户名是必需的")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "密码是必需的")]
        [MinLength(6, ErrorMessage = "密码至少需要6个字符")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "邮箱是必需的")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; } = string.Empty;

        public string? DisplayName { get; set; }
    }
} 