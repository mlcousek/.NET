using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyInstagram.Data;
using MyInstagram.Models;

namespace MyInstagram.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments/Create
        public IActionResult Create(string? userId, int postId)
        {
            
            var post = _context.Post.FirstOrDefault(p => p.Id == postId);
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id");

            ViewBag.Post = post;

            if (userId != null)
            {
                ViewBag.userId = userId;
            }
            else 
            {
                ViewBag.userId = null;
            }

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,PostId")] Comment comment, string userId1)
        {
            if (ModelState.ContainsKey("Post"))
            {
                ModelState.Remove("Post");
            }
            if (ModelState.ContainsKey("userId1"))
            {
                ModelState.Remove("userId1");
            }
            if (ModelState.ContainsKey("UserId"))
            {
                ModelState.Remove("UserId");
            }
            comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {

                _context.Add(comment);
                await _context.SaveChangesAsync();

                var post = await _context.Post
                    .FirstOrDefaultAsync(p => p.Id == comment.PostId);

                if (post == null)
                {
                    return NotFound();
                }
                if (post.Comments == null)
                {
                    post.Comments = new List<int>();
                }

                post.Comments.Add(comment.Id);
                await _context.SaveChangesAsync();


                if(userId1 != null)
                {
                    var user1 = _context.Users.FirstOrDefault(u => u.Id == userId1);
                    return RedirectToAction("Profile", "Home", new { username = user1.UzivatelskeJmeno });
                }

                return RedirectToAction("Index", "Home");
            }
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id, string? userId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            if(userId != null)
            {
                ViewBag.UzivatelskeId = userId;
            }
            else
            {
                ViewBag.UzivatelskeId = null;
            }
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string? userId, [Bind("Id,Text,PostId,UserId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.ContainsKey("Post"))
            {
                ModelState.Remove("Post");
            }

            if (ModelState.ContainsKey("userId"))
            {
                ModelState.Remove("userId");
            }

            var usrId = _context.Comment
                            .Where(c => c.Id == comment.Id)     
                            .Select(c => c.UserId)              
                            .FirstOrDefault();

            comment.UserId = usrId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (userId != null)
                {
                    var user = _context.Users.Find(userId);
                    if (user != null)
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Profile", "Home", new { username = user.UzivatelskeJmeno });

                    }
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");

            }
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comment
        //        .Include(c => c.Post)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int postId, string? userId)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment != null)
            {
                _context.Comment.Remove(comment);

                var post = await _context.Post
                    .FirstOrDefaultAsync(p => p.Id == postId);

                if (post != null && post.Comments.Contains(id))
                {
                    post.Comments.Remove(id);
                }
            }


            if (userId != null) 
            {
                var user = _context.Users.Find(userId);
                if (user != null) 
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Profile", "Home", new { username = user.UzivatelskeJmeno });

                }
                return RedirectToAction("Index", "Home");
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
