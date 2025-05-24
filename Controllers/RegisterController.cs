using System.Security.Claims;
using Blog.Data;
using Blog.Entity;
using Blog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class RegisterController : Controller
    {
        public readonly DataContext _context;

        public RegisterController(DataContext context)
        {
            _context = context;
        }

        //***********************************************************************************************************

        public IActionResult Register()
        {
            if(User.Identity!.IsAuthenticated) { return RedirectToAction("Index", "Home"); } return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User reg)
        {
            if(await _context.Users.AnyAsync(u => u.Email == reg.Email))
            {   
                ModelState.AddModelError("email","Email'e kayıtlı kullanıcı bulunmakta");
                return View(reg);
            }
            else if(await _context.Users.AnyAsync(u => u.Nick == reg.Nick))
            {   
                ModelState.AddModelError("nick","Kullanıcı adına kayıtlı kullanıcı bulunmakta");
                return View(reg);
            }
            else 
            {   
                // Kayıt işlemleri...
                if (ModelState.IsValid)
                {
                    reg.PublishedOn = DateTime.UtcNow; reg.Active = true; reg.Admin = false;
                    _context.Users.Add(reg);
                    await _context.SaveChangesAsync();

                    //Cookie ve Claim işlemleri
                    reg = await _context.Users.FirstOrDefaultAsync(u => u.Email == reg.Email && u.Password == reg.Password);
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, reg.UserId.ToString()));
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    //Giriş işlemleri
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                return View(reg);
            }
        }

        //***********************************************************************************************************

        public IActionResult Login()
        {
            if(User.Identity!.IsAuthenticated){ return RedirectToAction("Index", "Home"); } return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(await _context.Users.AnyAsync(u => u.Email == model.Nick && u.Password == model.Password))
            {
                var isUser =await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Nick && u.Password == model.Password );

                var userClaims = new List<Claim>();

                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index","Home");
            }
            else if(await _context.Users.AnyAsync(u => u.Nick == model.Nick && u.Password == model.Password))
            {
                var isUser =await _context.Users.FirstOrDefaultAsync(u => u.Nick == model.Nick && u.Password == model.Password );

                var userClaims = new List<Claim>();

                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("","Girilen bilgiler yanlış");
                return View();
            }

        }

        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        //***********************************************************************************************************

        public Entity.User doldur(User model)
        {
            Entity.User user = new Entity.User();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Title = model.Title;
            user.Birthday = model.Birthday;
            user.Email = model.Email;
            user.Nick = model.Nick;
            user.Password = model.Password;
            user.PublishedOn = DateTime.UtcNow;
            user.Active = true;

            return user;
        }

        




    }
}