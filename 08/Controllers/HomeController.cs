using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyInstagram.Data;
using MyInstagram.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MyInstagram.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            ViewBag.UzivatelskeJmeno = currentUser.UzivatelskeJmeno;
            ViewBag.UzivatelskeId = currentUser.Id;

            var users = await _userManager.Users.ToListAsync();
            ViewData["Users"] = users;

            var followingUsers = currentUser.Following;

            var posts = await _context.Post
                 .Include(p => p.User)
                 .Where(p => followingUsers.Any(u => u == p.UserId)) 
                 .OrderByDescending(p => p.Date)
                .ToListAsync();

            var comments = new List<Comment>();

            foreach (var post in posts)
            {
                if (post.Comments != null && post.Comments.Any())
                {
                    var postComments = await _context.Comment
                        .Where(c => post.Comments.Contains(c.Id))
                        .ToListAsync();

                    comments.AddRange(postComments);
                }
            }
            ViewData["Comments"] = comments;

            return View(posts);
        }

        [Authorize]
        public async Task<IActionResult> ProfileAsync(string? username)
        {
            var userId = "";

            if (username == null)
            {
                 userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else 
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UzivatelskeJmeno == username);
                if (user == null)
                {
                    TempData["Error"] = "User not found.";
                    return RedirectToAction(nameof(Index));
                }
                 userId = user.Id;
            }

            var currentUser = await _userManager.FindByIdAsync(userId);

            if (currentUser == null)
            {
                return View();
            }

            ViewBag.UzivatelskeJmeno = currentUser.UzivatelskeJmeno;
            ViewBag.UzivatelskeId = currentUser.Id;

            var followedUsers = currentUser.Followed;

            var posts = await _context.Post
                 .Include(p => p.User)
                 .Where(p => p.UserId == currentUser.Id)
                 .OrderByDescending(p => p.Date)
                .ToListAsync();

            var comments = new List<Comment>();

            foreach (var post in posts)
            {
                if (post.Comments != null && post.Comments.Any())
                {
                    var postComments = await _context.Comment
                        .Where(c => post.Comments.Contains(c.Id))
                        .ToListAsync();

                    comments.AddRange(postComments);
                }
            }


            var users = await _userManager.Users.ToListAsync();
            ViewData["Users"] = users;
            ViewData["Comments"] = comments;

            ViewData["User"] = currentUser;

            return View(posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FollowUser(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userToFollow = await _userManager.FindByIdAsync(userId);

            if (currentUser == null || userToFollow == null)
            {
                return RedirectToAction("Index"); 
            }

            if (currentUser.Following == null)
            {
                currentUser.Following = new List<string>(); 
            }
            if (!currentUser.Following.Contains(userToFollow.Id))
            {
                currentUser.Following.Add(userToFollow.Id);
                await _userManager.UpdateAsync(currentUser);
            }

            if (userToFollow.Followed == null)
            {
                userToFollow.Followed = new List<string>(); 
            }
            if (!userToFollow.Followed.Contains(currentUser.Id))
            {
                userToFollow.Followed.Add(currentUser.Id);
                await _userManager.UpdateAsync(userToFollow);
            }

            return RedirectToAction("Profile", new { username = userToFollow.UzivatelskeJmeno });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UnfollowUser(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userToUnfollow = await _userManager.FindByIdAsync(userId);

            if (currentUser == null || userToUnfollow == null)
            {
                return NotFound();
            }

            currentUser.Following.Remove(userToUnfollow.Id); // Odstraò uživatele z Following
            userToUnfollow.Followed.Remove(currentUser.Id); // Odstraò uživatele z Followed

            await _userManager.UpdateAsync(currentUser);
            await _userManager.UpdateAsync(userToUnfollow);

            return RedirectToAction("Profile", new { username = userToUnfollow.UzivatelskeJmeno });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> PridatTestovaciDataAsync()
        {
            var user = new User
            {
                Jmeno = "Jirka",
                Prijmeni = "Mlcousek",
                UzivatelskeJmeno = "test1",
                Vek = 25,
                Pohlavi = 1,
                UserName = "test@test.cz",
                Email = "test@test.cz",
                EmailConfirmed = true
            };

            var user1 = new User
            {
                Jmeno = "Pavel",
                Prijmeni = "Zeleny",
                UzivatelskeJmeno = "test2",
                Vek = 22,
                Pohlavi = 1,
                UserName = "test1@test.cz",
                Email = "test1@test.cz",
                EmailConfirmed = true
            };
            var user2 = new User
            {
                Jmeno = "Ondra",
                Prijmeni = "Palenka",
                UzivatelskeJmeno = "test3",
                Vek = 60,
                Pohlavi = 1,
                UserName = "test2@test.cz",
                Email = "test2@test.cz",
                EmailConfirmed = true
            };
            var user3 = new User
            {
                Jmeno = "Jindra",
                Prijmeni = "Sedlacek",
                UzivatelskeJmeno = "test4",
                Vek = 45,
                Pohlavi = 1,
                UserName = "test3@test.cz",
                Email = "test3@test.cz",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, "Test123*");
            var result1 = await _userManager.CreateAsync(user1, "Test123*");
            var result2 = await _userManager.CreateAsync(user2, "Test123*");
            var result3 = await _userManager.CreateAsync(user3, "Test123*");

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }
    }
}
