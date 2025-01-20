using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XiehouYu.Admin.Data;
using XiehouYu.Admin.Models;
using XiehouYu.Admin.ViewModels;

namespace XiehouYu.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .Select(u => new UserManagementViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    DisplayName = u.DisplayName,
                    CreatedAt = u.CreatedAt,
                    IsLocked = u.LockoutEnd > DateTime.UtcNow,
                    GameCount = u.GameProgresses.Count,
                    AccuracyRate = u.GameProgresses.Any() 
                        ? u.GameProgresses.Count(p => p.IsCorrect) * 100.0 / u.GameProgresses.Count 
                        : 0
                })
                .ToListAsync();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.LockoutEnd > DateTime.UtcNow)
            {
                // 解锁用户
                await _userManager.SetLockoutEndDateAsync(user, null);
                _logger.LogInformation($"用户 {user.UserName} 已被解锁");
            }
            else
            {
                // 锁定用户30天
                await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(30));
                _logger.LogInformation($"用户 {user.UserName} 已被锁定30天");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);
            _logger.LogInformation($"用户 {user.UserName} 已被删除");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var gameStats = await _context.GameProgress
                .Where(p => p.UserId == id)
                .GroupBy(p => 1)
                .Select(g => new GameStatsViewModel
                {
                    TotalGames = g.Count(),
                    CorrectAnswers = g.Count(p => p.IsCorrect),
                    AverageTime = g.Average(p => p.TimeTaken)
                })
                .FirstOrDefaultAsync();

            var rewards = await _context.UserRewards
                .Where(r => r.UserId == id)
                .OrderByDescending(r => r.CreatedAt)
                .Take(10)
                .ToListAsync();

            return View(new UserDetailsViewModel
            {
                User = user,
                GameStats = gameStats,
                RecentRewards = rewards
            });
        }
    }
} 