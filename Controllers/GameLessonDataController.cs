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
    public class GameLessonDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public GameLessonDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: GameLessonData
        public async Task<IActionResult> Index()
        {
            var mvcWordAssetsContext = _context.GameLessonData.Include(g => g.GameData).Include(g => g.WordAssetData);
            return View(await mvcWordAssetsContext.ToListAsync());
        }

        // GET: GameLessonData/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.GameLessonData == null)
            {
                return NotFound();
            }

            var gameLessonData = await _context.GameLessonData
                .Include(g => g.GameData)
                .Include(g => g.WordAssetData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLessonData == null)
            {
                return NotFound();
            }

            return View(gameLessonData);
        }

        // GET: GameLessonData/Create
        public IActionResult Create()
        {
            ViewData["GameDataID"] = new SelectList(_context.GameData, "ID", "ID");
            ViewData["WordAssetDataID"] = new SelectList(_context.WordAssetData, "ID", "ID");
            return View();
        }

        // POST: GameLessonData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameDataID,WordAssetDataID,WordTeaching,WordDisturbing")] GameLessonData gameLessonData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameLessonData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameDataID"] = new SelectList(_context.GameData, "ID", "ID", gameLessonData.GameDataID);
            ViewData["WordAssetDataID"] = new SelectList(_context.WordAssetData, "ID", "ID", gameLessonData.WordAssetDataID);
            return View(gameLessonData);
        }

        // GET: GameLessonData/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.GameLessonData == null)
            {
                return NotFound();
            }

            var gameLessonData = await _context.GameLessonData.FindAsync(id);
            if (gameLessonData == null)
            {
                return NotFound();
            }
            ViewData["GameDataID"] = new SelectList(_context.GameData, "ID", "ID", gameLessonData.GameDataID);
            ViewData["WordAssetDataID"] = new SelectList(_context.WordAssetData, "ID", "ID", gameLessonData.WordAssetDataID);
            return View(gameLessonData);
        }

        // POST: GameLessonData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GameDataID,WordAssetDataID,WordTeaching,WordDisturbing")] GameLessonData gameLessonData)
        {
            if (id != gameLessonData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameLessonData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameLessonDataExists(gameLessonData.Id))
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
            ViewData["GameDataID"] = new SelectList(_context.GameData, "ID", "ID", gameLessonData.GameDataID);
            ViewData["WordAssetDataID"] = new SelectList(_context.WordAssetData, "ID", "ID", gameLessonData.WordAssetDataID);
            return View(gameLessonData);
        }

        // GET: GameLessonData/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.GameLessonData == null)
            {
                return NotFound();
            }

            var gameLessonData = await _context.GameLessonData
                .Include(g => g.GameData)
                .Include(g => g.WordAssetData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLessonData == null)
            {
                return NotFound();
            }

            return View(gameLessonData);
        }

        // POST: GameLessonData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.GameLessonData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.GameLessonData'  is null.");
            }
            var gameLessonData = await _context.GameLessonData.FindAsync(id);
            if (gameLessonData != null)
            {
                _context.GameLessonData.Remove(gameLessonData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameLessonDataExists(long id)
        {
          return (_context.GameLessonData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
