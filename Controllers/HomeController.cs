using System.Security.Claims;
using Blog.Data;
using Blog.Entity;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;

    public HomeController(DataContext context)
    {
        _context = context;
    }

    // Listeleme    
    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel();
        if(User.Identity!.IsAuthenticated)
        {
            int UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user1 = _context.Users.FirstOrDefault(u => u.UserId == UserId);
            model.Nick = user1.Nick;
        }
        model.Questions = _context.Questions
                                    .Include(q => q.Comments)
                                    .ThenInclude(c => c.User)
                                    .Include(q=> q.User)
                                    .ToList();
        return View(model);
    }
    // Searche göre Listeleme  
    [HttpPost]
    public async Task<IActionResult> Index(HomeViewModel model)
    {
        if(model.Search == null)
        {
            return RedirectToAction("Index");
        }
        int UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var user1 = _context.Users.FirstOrDefault(u => u.UserId == UserId);
        model.Nick = user1.Nick;
        model.Questions = _context.Questions
                                    .Include(q => q.Comments)
                                    .ThenInclude(c => c.User)
                                    .Include(q => q.User)
                                    .Where(q => q.QuestionName.ToLower().Contains(model.Search.ToLower()))
                                    .ToList();
        
        return View(model);
    }

    // Soru Ekleme
    public async Task<IActionResult> AddQuestion()
    {
        if(User.Identity!.IsAuthenticated)
        {
            return View();
        }

        return RedirectToAction("Index");
    }
    // Soru Ekleme Post
    [HttpPost]
    public async Task<IActionResult> AddQuestion(Question Question)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        
        Question.PublishedOn = DateTime.UtcNow;
        Question.UserId = userId;

        await _context.Questions.AddAsync(Question);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    // Soru Düzenleme
    public async Task<IActionResult> EditQuestion(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        return View(question);
    }
    // Soru Düzenleme Post
    [HttpPost]
    public async Task<IActionResult> EditQuestion(Question Question)
    {
        var _Question = _context.Questions.FirstOrDefault(q => q.QuestionId == Question.QuestionId);
        _Question.QuestionName = Question.QuestionName;
        _Question.QuestionInfo = Question.QuestionInfo;
        _context.Questions.Update(_Question);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // Soru Silme
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // Yorum Ekleme
    [HttpPost]
    public async Task<IActionResult> AddComment(HomeViewModel homeViewModel)
    {
        if(!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        

        homeViewModel.Comment.PublishedOn = DateTime.UtcNow;
        homeViewModel.Comment.UserId = userId;

        await _context.Comments.AddAsync(homeViewModel.Comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    // Yorum Düzenleme
    public async Task<IActionResult> EditComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        return View(comment);
    }
    // Yorum Düzenleme Post
    [HttpPost]
    public async Task<IActionResult> EditComment(Comment _Comment)
    {
        var comment = await _context.Comments.FindAsync(_Comment.CommentId);
        comment.CommentInfo = _Comment.CommentInfo;
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    // Yorum Silme
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
