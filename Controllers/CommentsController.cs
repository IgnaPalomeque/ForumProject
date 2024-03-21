using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForumProject.Data;
using ForumProject.Models;

namespace ForumProject.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostComment.Include(p => p.ForumPost);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await _context.PostComment
                .Include(p => p.ForumPost)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (postComment == null)
            {
                return NotFound();
            }

            return View(postComment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["PostID"] = new SelectList(_context.ForumPost, "ID", "Author");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Body,PostID")] PostComment postComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostID"] = new SelectList(_context.ForumPost, "ID", "Author", postComment.PostID);
            return View(postComment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await _context.PostComment.FindAsync(id);
            if (postComment == null)
            {
                return NotFound();
            }
            ViewData["PostID"] = new SelectList(_context.ForumPost, "ID", "Author", postComment.PostID);
            return View(postComment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Body,PostID")] PostComment postComment)
        {
            if (id != postComment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCommentExists(postComment.ID))
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
            ViewData["PostID"] = new SelectList(_context.ForumPost, "ID", "Author", postComment.PostID);
            return View(postComment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await _context.PostComment
                .Include(p => p.ForumPost)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (postComment == null)
            {
                return NotFound();
            }

            return View(postComment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postComment = await _context.PostComment.FindAsync(id);
            if (postComment != null)
            {
                _context.PostComment.Remove(postComment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCommentExists(int id)
        {
            return _context.PostComment.Any(e => e.ID == id);
        }
    }
}
