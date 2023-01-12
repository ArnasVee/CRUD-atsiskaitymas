using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using atsiskaitymas.Data;
using atsiskaitymas.Models;

namespace atsiskaitymas.Controllers
{
    public class RappersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RappersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rappers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Rapper.ToListAsync());
        }

        // GET: Rappers/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Rappers/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Rapper.Where( j => j.Name.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Rappers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rapper == null)
            {
                return NotFound();
            }

            var rapper = await _context.Rapper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rapper == null)
            {
                return NotFound();
            }

            return View(rapper);
        }

        // GET: Rappers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rappers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Affiliation,Rhyme")] Rapper rapper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rapper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rapper);
        }

        // GET: Rappers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rapper == null)
            {
                return NotFound();
            }

            var rapper = await _context.Rapper.FindAsync(id);
            if (rapper == null)
            {
                return NotFound();
            }
            return View(rapper);
        }

        // POST: Rappers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Affiliation,Rhyme")] Rapper rapper)
        {
            if (id != rapper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rapper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RapperExists(rapper.Id))
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
            return View(rapper);
        }

        // GET: Rappers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rapper == null)
            {
                return NotFound();
            }

            var rapper = await _context.Rapper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rapper == null)
            {
                return NotFound();
            }

            return View(rapper);
        }

        // POST: Rappers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rapper == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rapper'  is null.");
            }
            var rapper = await _context.Rapper.FindAsync(id);
            if (rapper != null)
            {
                _context.Rapper.Remove(rapper);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RapperExists(int id)
        {
          return _context.Rapper.Any(e => e.Id == id);
        }
    }
}
