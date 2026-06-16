using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoSummarization.Models;

namespace VideoSummarization.Controllers
{
    [Authorize("Admin")]
    public class UsersController : Controller
    {
        private readonly VideoSummDBContext _context;

        public UsersController(VideoSummDBContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            var users = _context.Users.Include(r => r.Role).ToList();
            return View(users);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            var userExams = _context.Exams.Where(e => e.UserId == user.Id).ToList();

            ViewData["UserName"] = user.Name;
            return View(userExams);
        }

        //// GET: Users/Create
        //public IActionResult Create()
        //{
        //    ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,Password,RoleId")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
        //    return View(user);
        //}

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,Password,RoleId")] User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
        //    return View(user);
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'VideoSummDBContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
