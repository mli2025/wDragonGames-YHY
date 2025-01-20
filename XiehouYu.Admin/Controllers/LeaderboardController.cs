using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XiehouYu.Admin.Data;
using XiehouYu.Admin.Models;
using XiehouYu.Admin.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XiehouYu.Admin.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ILogger<LeaderboardController> _logger;
        private readonly ApplicationDbContext _context;

        public LeaderboardController(ILogger<LeaderboardController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var leaderboard = await _context.GameProgress
                .Include(p => p.User)
                .GroupBy(p => p.UserId)
                .Select(g => new LeaderboardViewModel
                {
                    UserId = g.Key,
                    UserName = g.First().User.UserName ?? "未知用户",
                    TotalScore = g.Count(p => p.IsCorrect) * 10,
                    CorrectAnswers = g.Count(p => p.IsCorrect),
                    TotalQuestions = g.Count(),
                    AverageTime = g.Average(p => p.TimeTaken)
                })
                .OrderByDescending(l => l.TotalScore)
                .Take(100)
                .ToListAsync();

            return View(leaderboard);
        }
    }
} 