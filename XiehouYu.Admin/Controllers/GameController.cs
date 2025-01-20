using Microsoft.AspNetCore.Mvc;
using XiehouYu.Admin.Data;
using XiehouYu.Admin.Models;
using Microsoft.EntityFrameworkCore;
using XiehouYu.Api.Models;

namespace XiehouYu.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/game")]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GameController> _logger;
        private readonly Random _random = new Random();

        public GameController(ApplicationDbContext context, ILogger<GameController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("random")]
        public async Task<ActionResult<QuestionModel>> GetRandomQuestion()
        {
            try
            {
                var count = await _context.Questions.CountAsync();
                var skip = _random.Next(count);
                var question = await _context.Questions
                    .Skip(skip)
                    .Take(1)
                    .FirstOrDefaultAsync();

                if (question == null)
                    return NotFound("没有可用的题目");

                return Ok(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取随机题目时发生错误");
                return StatusCode(500, "获取题目失败");
            }
        }

        [HttpPost("check")]
        public async Task<ActionResult<bool>> CheckAnswer([FromBody] AnswerRequest request)
        {
            try
            {
                var question = await _context.Questions.FindAsync(request.QuestionId);
                if (question == null)
                    return NotFound("题目不存在");

                return Ok(question.Answer.Equals(request.Answer, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "检查答案时发生错误");
                return StatusCode(500, "检查答案失败");
            }
        }

        [HttpGet("stats")]
        public async Task<ActionResult<UserStatsModel>> GetUserStats()
        {
            try
            {
                // TODO: 使用实际的用户ID
                var userId = "test-user";

                var gameProgress = await _context.GameProgress
                    .Where(g => g.UserId == userId)
                    .OrderByDescending(g => g.PlayedAt)
                    .Take(10)
                    .ToListAsync();

                var stats = new UserStatsModel
                {
                    TotalScore = gameProgress.Count(g => g.IsCorrect) * 10,
                    TotalGames = gameProgress.Count,
                    CorrectAnswers = gameProgress.Count(g => g.IsCorrect),
                    AccuracyRate = gameProgress.Any() 
                        ? (double)gameProgress.Count(g => g.IsCorrect) / gameProgress.Count * 100 
                        : 0,
                    RecentGames = new List<GameHistoryModel>()
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取用户统计数据时发生错误");
                return StatusCode(500, "获取统计数据失败");
            }
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitResult([FromBody] GameResultModel result)
        {
            try
            {
                // TODO: 使用实际的用户ID
                var userId = "test-user";

                var gameProgress = new GameProgress
                {
                    UserId = userId,
                    PlayedAt = DateTime.UtcNow,
                    IsCorrect = result.IsCorrect,
                    TimeTaken = result.TimeTaken
                };

                _context.GameProgress.Add(gameProgress);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "提交游戏结果时发生错误");
                return StatusCode(500, "提交结果失败");
            }
        }
    }

    public class AnswerRequest
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
    }
} 