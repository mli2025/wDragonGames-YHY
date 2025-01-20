using XiehouYu.Models;
using XiehouYu.Services;

namespace XiehouYu.Views
{
    public partial class ProfilePage : ContentPage
    {
        private readonly IGameService _gameService;

        public ProfilePage(IGameService gameService)
        {
            InitializeComponent();
            _gameService = gameService;
            _ = LoadUserDataAsync();
        }

        private async Task LoadUserDataAsync()
        {
            try
            {
                var stats = await _gameService.GetUserStatsAsync();
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // 更新UI
                    ScoreLabel.Text = stats.TotalScore.ToString();
                    TotalGamesLabel.Text = stats.TotalGames.ToString();
                    AccuracyLabel.Text = $"{stats.AccuracyRate:F1}%";
                    
                    // 更新游戏历史
                    HistoryCollectionView.ItemsSource = stats.RecentGames;
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("错误", $"加载用户数据失败: {ex.Message}", "确定");
            }
        }

        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage(
                Handler.MauiContext.Services.GetService<IAuthService>()));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            var logout = await DisplayAlert("提示", "确定要退出登录吗？", "确定", "取消");
            if (logout)
            {
                // TODO: 清除用户登录状态
                await Navigation.PopToRootAsync();
            }
        }
    }
} 