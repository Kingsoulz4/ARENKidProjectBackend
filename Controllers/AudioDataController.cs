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
    public class AudioDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public AudioDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: AudioData
        public async Task<IActionResult> Index()
        {
              return _context.AudioData != null ? 
                          View(await _context.AudioData.ToListAsync()) :
                          Problem("Entity set 'MvcWordAssetsContext.AudioData'  is null.");
        }

        // GET: AudioData/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.AudioData == null)
            {
                return NotFound();
            }

            var audioData = await _context.AudioData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audioData == null)
            {
                return NotFound();
            }

            return View(audioData);
        }

        // GET: AudioData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AudioData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilePath,Duration,AudioType")] AudioData audioData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(audioData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(audioData);
        }

        // GET: AudioData/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.AudioData == null)
            {
                return NotFound();
            }

            var audioData = await _context.AudioData.FindAsync(id);
            if (audioData == null)
            {
                return NotFound();
            }
            return View(audioData);
        }

        // POST: AudioData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FilePath,Duration,AudioType")] AudioData audioData)
        {
            if (id != audioData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audioData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudioDataExists(audioData.Id))
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
            return View(audioData);
        }

        // GET: AudioData/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.AudioData == null)
            {
                return NotFound();
            }

            var audioData = await _context.AudioData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audioData == null)
            {
                return NotFound();
            }

            return View(audioData);
        }

        // POST: AudioData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.AudioData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.AudioData'  is null.");
            }
            var audioData = await _context.AudioData.FindAsync(id);
            if (audioData != null)
            {
                _context.AudioData.Remove(audioData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudioDataExists(long id)
        {
          return (_context.AudioData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
