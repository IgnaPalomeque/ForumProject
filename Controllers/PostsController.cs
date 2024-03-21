using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForumProject.Data;
using ForumProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace ForumProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.ForumPost.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPost
                .Include(e => e.Comments)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (forumPost == null)
            {
                return NotFound();
            }

            return View(forumPost);
        }

		// GET: Posts/Create
		[Authorize]
		public IActionResult Create()
        {
            //Takes the User name account to set to the author of the post
            string author = User.Identity.Name;
            ViewData["Author"] = author;

			ViewData["DateCreated"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

			return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create(string author,[Bind("ID,Title,Description,Author,DateCreated")] ForumPost forumPost)
        {
            if (ModelState.IsValid)
            {
                forumPost.Author = author;
                _context.Add(forumPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumPost);
        }

		// GET: Posts/Edit/5
		[Authorize]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPost.FindAsync(id);
            if (forumPost == null)
            {
                return NotFound();
            }
            return View(forumPost);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Author,DateCreated")] ForumPost forumPost)
        {
            if (id != forumPost.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumPostExists(forumPost.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(forumPost);
        }

		// GET: Posts/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPost
                .FirstOrDefaultAsync(m => m.ID == id);
            if (forumPost == null)
            {
                return NotFound();
            }

            return View(forumPost);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forumPost = await _context.ForumPost.FindAsync(id);
            if (forumPost != null)
            {
                _context.ForumPost.Remove(forumPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumPostExists(int id)
        {
            return _context.ForumPost.Any(e => e.ID == id);
        }

        //Creates a new comment for the post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateComment(int postID,[Bind("ID,Body,PostID")] PostComment postComment)
        {
            if (ModelState.IsValid)
            {
                /*
                Need to add an author field to the PostComment model and then set it to the Author like the ID
                postComment.CommentAuthor = author;*/
                postComment.CommentAuthor = User.Identity.Name;
                
                postComment.PostID = postID;//sets the PostID to the ID of the post parameter that takes the function from the view

                _context.Add(postComment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Posts", new {id = postID});//Make it redirect to the same post page
            }
            return View(postComment);
        }
    }
}
