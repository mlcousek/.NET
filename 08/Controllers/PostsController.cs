using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyInstagram.Data;
using MyInstagram.Data.Migrations;
using MyInstagram.Models;

namespace MyInstagram.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public PostsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,ImgPath,LikesSerialized,UserId")] Post post)
        {
            if (ModelState.ContainsKey("User"))
            {
                ModelState.Remove("User");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            post.UserId = userId;
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile", "Home", new { username = user.UzivatelskeJmeno });

            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,ImgPath,LikesSerialized,UserId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.ContainsKey("User"))
            {
                ModelState.Remove("User");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);


                return RedirectToAction("Profile", "Home", new { username = user.UzivatelskeJmeno });
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string userId)
        {
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                _context.Post.Remove(post);
            }

            var user = _context.Users.Find(userId);

            await _context.SaveChangesAsync();
            return RedirectToAction("Profile", "Home", new { username = user.UzivatelskeJmeno });
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> LikePost(string? userId, int postId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var post = await _context.Post.FindAsync(postId);

            if (currentUser != null && post != null)
            {
                if (post.Likes == null)
                {
                    post.Likes = new List<string>();
                }

                if (!post.Likes.Contains(currentUser.Id))
                {
                    post.Likes.Add(currentUser.Id);
                }
                else
                {
                    post.Likes.Remove(currentUser.Id); 
                }

                await _context.SaveChangesAsync();
            }

            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var user = await _userManager.FindByIdAsync(userId);
                return RedirectToAction("Profile", "Home", new { username = user.UzivatelskeJmeno });
            }
        }
    }
}
