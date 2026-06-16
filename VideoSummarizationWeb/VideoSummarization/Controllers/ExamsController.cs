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
    [Authorize("User")]
    public class ExamsController : Controller
    {
        private readonly VideoSummDBContext _context;

        public ExamsController(VideoSummDBContext context)
        {
            _context = context;
        }

        // GET: Exams
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Name == userName);

            var exams = _context.Exams.Where(e => e.UserId == user.Id).ToList();
            return View(exams);
        }

        //// GET: Exams/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Exams == null)
        //    {
        //        return NotFound();
        //    }

        //    var exam = await _context.Exams
        //        .Include(e => e.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (exam == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(exam);
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.Name == userName);
                exam.UserId = user.Id;

                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exam);
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Exams == null)
        //    {
        //        return NotFound();
        //    }

        //    var exam = await _context.Exams.FindAsync(id);
        //    if (exam == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(exam);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Exam exam)
        //{
        //    if (id != exam.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(exam);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ExamExists(exam.Id))
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
        //    return View(exam);
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
