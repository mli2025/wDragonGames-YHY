using Microsoft.Extensions.Logging;
using XiehouYu.Services;
using XiehouYu.Views;

namespace XiehouYu
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // 注册 HttpClient
            builder.Services.AddSingleton<HttpClient>(sp => new HttpClient 
            { 
                BaseAddress = new Uri("http://localhost:5144/")  // Admin 项目的地址
            });

            // 注册服务
            builder.Services.AddSingleton<IGameService, GameService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            // 注册页面
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<GamePage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ChangePasswordPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
