using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace XiehouYu.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5144/api/auth";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/register", new
                {
                    username,
                    email,
                    password
                });

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("注册失败", ex);
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", new
                {
                    email,
                    password
                });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    return result?.Token ?? throw new Exception("登录失败：未获取到令牌");
                }

                throw new Exception("登录失败：用户名或密码错误");
            }
            catch (Exception ex)
            {
                throw new Exception("登录失败", ex);
            }
        }

        public async Task<bool> ResetPasswordAsync(string email)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/reset-password", new { email });
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("重置密码失败", ex);
            }
        }

        public async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/change-password", new
                {
                    currentPassword,
                    newPassword
                });

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("修改密码失败", ex);
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                // 清除本地存储的令牌
                await SecureStorage.Default.SetAsync("auth_token", string.Empty);
                
                // 可选：调用服务器的登出接口
                await _httpClient.PostAsync($"{BaseUrl}/logout", null);
            }
            catch (Exception ex)
            {
                throw new Exception("登出失败", ex);
            }
        }

        private class LoginResponse
        {
            public string Token { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }
    }
} 