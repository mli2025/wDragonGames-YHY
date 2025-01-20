using Microsoft.Maui.Controls;
using XiehouYu.Services;
using XiehouYu.Models;

namespace XiehouYu.Views
{
    public partial class GamePage : ContentPage
    {
        private readonly IGameService _gameService;
        private int remainingTime = 30;
        private IDispatcherTimer? timer;
        private QuestionModel? currentQuestion;
        private readonly int startTime;

        public GamePage(IGameService gameService)
        {
            InitializeComponent();
            _gameService = gameService;
            startTime = remainingTime;
            InitializeTimer();
            _ = LoadQuestionAsync();
        }

        private void InitializeTimer()
        {
            timer = Application.Current.Dispatcher.CreateTimer();
            if (timer != null)
            {
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            remainingTime--;
            TimerLabel.Text = $"剩余时间: {remainingTime}";

            if (remainingTime <= 0)
            {
                timer?.Stop();
                _ = OnTimeUpAsync();
            }
        }

        private async Task OnTimeUpAsync()
        {
            await DisplayAlert("提示", "时间到！", "确定");
            await Navigation.PopAsync();
        }

        private async Task LoadQuestionAsync()
        {
            try
            {
                currentQuestion = await _gameService.GetRandomQuestionAsync();
                if (currentQuestion != null)
                {
                    QuestionLabel.Text = currentQuestion.Content;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("错误", "加载题目失败", "确定");
                await Navigation.PopAsync();
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (currentQuestion == null)
            {
                await DisplayAlert("错误", "题目加载失败", "确定");
                return;
            }

            if (string.IsNullOrWhiteSpace(AnswerEntry.Text))
            {
                await DisplayAlert("提示", "请输入答案", "确定");
                return;
            }

            timer?.Stop();
            int timeTaken = startTime - remainingTime;

            try
            {
                var result = new GameResultModel
                {
                    QuestionId = currentQuestion.Id,
                    Answer = AnswerEntry.Text.Trim(),
                    IsCorrect = AnswerEntry.Text.Trim() == currentQuestion.Answer,
                    TimeTaken = timeTaken
                };

                await _gameService.SubmitGameResultAsync(result);

                if (result.IsCorrect)
                {
                    await DisplayAlert("恭喜", "回答正确！", "确定");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("提示", "答案不正确，请重试", "确定");
                    AnswerEntry.Text = string.Empty;
                    remainingTime = startTime;
                    timer?.Start();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("错误", "提交答案失败", "确定");
                timer?.Start();
            }
        }

        private async void OnHintClicked(object sender, EventArgs e)
        {
            if (currentQuestion == null)
            {
                await DisplayAlert("错误", "题目加载失败", "确定");
                return;
            }

            // 这里可以根据难度级别提供不同的提示
            string hint = currentQuestion.Difficulty switch
            {
                "easy" => "这是一个简单的歇后语",
                "medium" => "需要动动脑筋哦",
                "hard" => "这个比较难，仔细想想",
                _ => "提示功能正在开发中"
            };

            await DisplayAlert("提示", hint, "确定");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            timer?.Stop();
        }
    }
} 