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
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.Identity.Name;
            var forumPost = await _context.ForumPost
                .Where(e => e.Author == user)
                .ToListAsync();

            return View(forumPost);
        }
    }
}
