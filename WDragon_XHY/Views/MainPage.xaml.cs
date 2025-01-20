using Microsoft.Maui.Controls;
using XiehouYu.Services;

namespace XiehouYu.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(
                Handler.MauiContext.Services.GetService<IGameService>()));
        }

        private async void OnLeaderboardClicked(object sender, EventArgs e)
        {
            // 导航到排行榜页面
            // await Navigation.PushAsync(new LeaderboardPage());
            await DisplayAlert("提示", "排行榜功能正在开发中", "确定");
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage(
                Handler.MauiContext.Services.GetService<IGameService>()));
        }
    }
} 