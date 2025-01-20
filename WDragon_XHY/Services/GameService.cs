using System.Net.Http.Json;
using XiehouYu.Models;

namespace XiehouYu.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5144/api/game";
        private readonly Random _random = new Random();
        
        // 模拟题库
        private readonly List<QuestionModel> _sampleQuestions = new()
        {
            new QuestionModel
            {
                Id = 1,
                Content = "泥菩萨过河",
                Answer = "自身难保",
                Difficulty = "easy",
                Category = "生活"
            },
            new QuestionModel
            {
                Id = 2,
                Content = "八仙过海",
                Answer = "各显神通",
                Difficulty = "easy",
                Category = "神话"
            },
            new QuestionModel
            {
                Id = 3,
                Content = "孔明借箭",
                Answer = "草船借箭",
                Difficulty = "medium",
                Category = "历史"
            }
        };

        public GameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<QuestionModel> GetRandomQuestionAsync()
        {
            try
            {
                // 使用实际的API调用
                var response = await _httpClient.GetFromJsonAsync<QuestionModel>($"{BaseUrl}/random");
                return response ?? throw new Exception("获取题目失败");

                // 备用的模拟数据代码
                // await Task.Delay(500);
                // return _sampleQuestions[_random.Next(_sampleQuestions.Count)];
            }
            catch (Exception ex)
            {
                throw new Exception("获取题目时发生错误", ex);
            }
        }

        public async Task<bool> CheckAnswerAsync(int questionId, string answer)
        {
            try
            {
                // 使用实际的API调用
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/check", new { questionId, answer });
                return await response.Content.ReadFromJsonAsync<bool>();

                // 备用的模拟数据代码
                // await Task.Delay(300);
                // var question = _sampleQuestions.FirstOrDefault(q => q.Id == questionId);
                // return question?.Answer.Equals(answer, StringComparison.OrdinalIgnoreCase) ?? false;
            }
            catch (Exception ex)
            {
                throw new Exception("检查答案时发生错误", ex);
            }
        }

        public async Task<bool> SubmitGameResultAsync(GameResultModel result)
        {
            try
            {
                // 使用实际的API调用
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/submit", result);
                return response.IsSuccessStatusCode;

                // 备用的模拟数据代码
                // await Task.Delay(300);
                // return true;
            }
            catch (Exception ex)
            {
                throw new Exception("提交游戏结果时发生错误", ex);
            }
        }

        public async Task<UserStatsModel> GetUserStatsAsync()
        {
            try
            {
                // 使用实际的API调用
                var response = await _httpClient.GetFromJsonAsync<UserStatsModel>($"{BaseUrl}/stats");
                return response ?? throw new Exception("获取用户统计数据失败");

                // 备用的模拟数据代码
                // await Task.Delay(500);
                // return new UserStatsModel { ... };
            }
            catch (Exception ex)
            {
                throw new Exception("获取用户统计数据时发生错误", ex);
            }
        }
    }
} 