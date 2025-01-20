using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XiehouYu.Admin.Data;
using XiehouYu.Admin.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace XiehouYu.Admin.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public QuestionsController(ILogger<QuestionsController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("正在测试数据库连接...");
                using (var connection = new MySqlConnection(
                    _configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    _logger.LogInformation("数据库连接测试成功");
                }

                var questions = await _context.Questions.ToListAsync();
                return View(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库操作失败: {Message}", ex.Message);
                TempData["ErrorMessage"] = $"数据库错误: {ex.Message}";
                return View(new List<Question>());
            }
        }

        private async Task SeedSampleData()
        {
            try
            {
                var sampleQuestions = new List<Question>
                {
                    new Question 
                    { 
                        Content = "泥菩萨过河", 
                        Answer = "自身难保", 
                        Difficulty = "easy",
                        Category = "生活",
                        CreatedAt = DateTime.Now
                    },
                    new Question 
                    { 
                        Content = "八仙过海", 
                        Answer = "各显神通", 
                        Difficulty = "easy",
                        Category = "神话",
                        CreatedAt = DateTime.Now
                    }
                };

                await _context.Questions.AddRangeAsync(sampleQuestions);
                await _context.SaveChangesAsync();
                _logger.LogInformation("成功添加示例数据");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加示例数据时发生错误");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question)
        {
            if (ModelState.IsValid)
            {
                question.CreatedAt = DateTime.Now;
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(question);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
} 