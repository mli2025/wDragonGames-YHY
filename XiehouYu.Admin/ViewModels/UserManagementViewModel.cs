using System.ComponentModel.DataAnnotations;

namespace XiehouYu.Admin.ViewModels
{
    public class UserManagementViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "用户名")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "邮箱")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "显示名称")]
        public string DisplayName { get; set; } = string.Empty;

        [Display(Name = "积分")]
        public int Points { get; set; }

        [Display(Name = "注册时间")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "最后登录")]
        public DateTime? LastLoginTime { get; set; }

        [Display(Name = "状态")]
        public bool IsLocked { get; set; }

        [Display(Name = "游戏次数")]
        public int GameCount { get; set; }

        [Display(Name = "正确率")]
        public double AccuracyRate { get; set; }
    }
} 