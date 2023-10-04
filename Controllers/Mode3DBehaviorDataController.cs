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

    public class Mode3DBehaviorDataParamsHolder
    {
        public IFormFile? FlashCardImage { get; set; }
    }

    public class Mode3DBehaviorDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public Mode3DBehaviorDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: Mode3DBehaviorData
        public async Task<IActionResult> Index()
        {
            var mvcWordAssetsContext = _context.Mode3DBehaviorData!.Include(m => m.Model3DData);
            return View(await mvcWordAssetsContext.ToListAsync());
        }

        // GET: Mode3DBehaviorData/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Mode3DBehaviorData == null)
            {
                return NotFound();
            }

            var mode3DBehaviorData = await _context.Mode3DBehaviorData
                .Include(m => m.Model3DData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mode3DBehaviorData == null)
            {
                return NotFound();
            }

            return View(mode3DBehaviorData);
        }

        // GET: Mode3DBehaviorData/Create
        public IActionResult Create()
        {
            ViewData["Model3DDataID"] = new SelectList(_context.Model3DData, "Id", "Id");
            return View();
        }

        // POST: Mode3DBehaviorData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model3DDataID,Name,Thumb,Audio,ActionType")] Mode3DBehaviorData mode3DBehaviorData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mode3DBehaviorData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Model3DDataID"] = new SelectList(_context.Model3DData, "Id", "Id", mode3DBehaviorData.Model3DDataID);
            return View(mode3DBehaviorData);
        }

        // GET: Mode3DBehaviorData/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Mode3DBehaviorData == null)
            {
                return NotFound();
            }

            var mode3DBehaviorData = await _context.Mode3DBehaviorData.FindAsync(id);
            if (mode3DBehaviorData == null)
            {
                return NotFound();
            }
            ViewData["Model3DDataID"] = new SelectList(_context.Model3DData, "Id", "Id", mode3DBehaviorData.Model3DDataID);
            return View(mode3DBehaviorData);
        }

        // POST: Mode3DBehaviorData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Model3DDataID,Name,Thumb,Audio,ActionType")] Mode3DBehaviorData mode3DBehaviorData)
        {
            if (id != mode3DBehaviorData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mode3DBehaviorData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Mode3DBehaviorDataExists(mode3DBehaviorData.Id))
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
            ViewData["Model3DDataID"] = new SelectList(_context.Model3DData, "Id", "Id", mode3DBehaviorData.Model3DDataID);
            return View(mode3DBehaviorData);
        }

        // GET: Mode3DBehaviorData/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Mode3DBehaviorData == null)
            {
                return NotFound();
            }

            var mode3DBehaviorData = await _context.Mode3DBehaviorData
                .Include(m => m.Model3DData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mode3DBehaviorData == null)
            {
                return NotFound();
            }

            return View(mode3DBehaviorData);
        }

        // POST: Mode3DBehaviorData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Mode3DBehaviorData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.Mode3DBehaviorData'  is null.");
            }
            var mode3DBehaviorData = await _context.Mode3DBehaviorData.FindAsync(id);
            if (mode3DBehaviorData != null)
            {
                _context.Mode3DBehaviorData.Remove(mode3DBehaviorData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Mode3DBehaviorDataExists(long id)
        {
            return (_context.Mode3DBehaviorData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
