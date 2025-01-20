
using XiehouYu.Views;

namespace XiehouYu
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // 注册路由
            Routing.RegisterRoute(nameof(GamePage), typeof(Views.GamePage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(Views.ProfilePage));
        }
    }
}
