using Microsoft.AspNetCore.Mvc;
using Blog.Entity;
using Blog.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    public class ProfileController : Controller
    {
        public readonly DataContext _context;

        public ProfileController(DataContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<ActionResult> Index(string? Nick)
        {
            ProfileViewModel Profile = new ProfileViewModel();

            if (Nick == null)
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Profile.User = await _context.Users
                                    .Include(u => u.Comments)
                                    .Include(q => q.Questions)
                                    .FirstOrDefaultAsync(u => u.UserId == userId);
                return View(Profile);
            }
            else
            {
                Profile.User = await _context.Users
                                    .Include(u => u.Comments)
                                    .Include(q => q.Questions)
                                    .FirstOrDefaultAsync(u => u.Nick == Nick);
                return View(Profile);
            }
        }

        public IActionResult Questions(int QuestionId)
        {
            HomeViewModel model = new HomeViewModel();
            int UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user1 = _context.Users.FirstOrDefault(u => u.UserId == UserId);
            model.Nick = user1.Nick;
            model.Questions = _context.Questions
                                        .Where(q => q.UserId == _context.Questions
                                            .Where(q2 => q2.QuestionId == QuestionId)
                                            .Select(q2 => q2.UserId)
                                            .FirstOrDefault()
                                        )
                                        .Include(q => q.Comments)
                                        .ThenInclude(c => c.User)
                                        .Include(q => q.User)
                                        .ToList();
            return View(model);
        }


    }
}
