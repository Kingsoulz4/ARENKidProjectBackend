using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectBackend.Models;

namespace ProjectBackend.Controllers
{
    public class WordAssetsController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public WordAssetsController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: WordAssets
        public async Task<IActionResult> Index(string searchPattern)
        {
            if(_context.WordAssets == null)
            {
                return Problem("Entity set WordAssets null");
            }

            var wordAssets = from w in _context.WordAssets select w;

            if(!string.IsNullOrEmpty(searchPattern))
            {
                wordAssets = wordAssets.Where(x => x.Text!.Contains(searchPattern));
            }

            return View(await wordAssets.ToListAsync());
        }

        // GET: WordAssets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WordAssets == null)
            {
                return NotFound();
            }

            var wordAssets = await _context.WordAssets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wordAssets == null)
            {
                return NotFound();
            }

            return View(wordAssets);
        }

        // GET: WordAssets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WordAssets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,DateCreated, LinkDownLoad")] WordAssets wordAssets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wordAssets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wordAssets);
        }

        // GET: WordAssets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WordAssets == null)
            {
                return NotFound();
            }

            var wordAssets = await _context.WordAssets.FindAsync(id);
            if (wordAssets == null)
            {
                return NotFound();
            }
            return View(wordAssets);
        }

        // POST: WordAssets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,DateCreated")] WordAssets wordAssets)
        {
            if (id != wordAssets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wordAssets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordAssetsExists(wordAssets.Id))
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
            return View(wordAssets);
        }

        // GET: WordAssets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WordAssets == null)
            {
                return NotFound();
            }

            var wordAssets = await _context.WordAssets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wordAssets == null)
            {
                return NotFound();
            }

            return View(wordAssets);
        }

        // POST: WordAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WordAssets == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.WordAssets'  is null.");
            }
            var wordAssets = await _context.WordAssets.FindAsync(id);
            if (wordAssets != null)
            {
                _context.WordAssets.Remove(wordAssets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordAssetsExists(int id)
        {
          return (_context.WordAssets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
