using XiehouYu.Services;

namespace XiehouYu.Views
{
    public partial class ChangePasswordPage : ContentPage
    {
        private readonly IAuthService _authService;

        public ChangePasswordPage(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentPasswordEntry.Text) ||
                string.IsNullOrEmpty(NewPasswordEntry.Text) ||
                string.IsNullOrEmpty(ConfirmPasswordEntry.Text))
            {
                await DisplayAlert("错误", "请填写所有密码字段", "确定");
                return;
            }

            if (NewPasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                await DisplayAlert("错误", "新密码与确认密码不匹配", "确定");
                return;
            }

            try
            {
                var success = await _authService.ChangePasswordAsync(
                    CurrentPasswordEntry.Text,
                    NewPasswordEntry.Text);

                if (success)
                {
                    await DisplayAlert("成功", "密码已成功修改", "确定");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("错误", "修改密码失败，请检查当前密码是否正确", "确定");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("错误", $"修改密码失败: {ex.Message}", "确定");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
} 