using XiehouYu.Views;

namespace XiehouYu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // 创建导航页面，并设置 MainPage 为根页面
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["Primary"],
                BarTextColor = (Color)Application.Current.Resources["White"]
            };
        }
    }
}
