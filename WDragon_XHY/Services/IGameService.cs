using XiehouYu.Models;

namespace XiehouYu.Services
{
    public interface IGameService
    {
        Task<QuestionModel> GetRandomQuestionAsync();
        Task<bool> CheckAnswerAsync(int questionId, string answer);
        Task<bool> SubmitGameResultAsync(GameResultModel result);
        Task<UserStatsModel> GetUserStatsAsync();
    }
} 