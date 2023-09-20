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
    public class VideoDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public VideoDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: VideoData
        public async Task<IActionResult> Index()
        {
              return _context.VideoData != null ? 
                          View(await _context.VideoData.ToListAsync()) :
                          Problem("Entity set 'MvcWordAssetsContext.VideoData'  is null.");
        }

        // GET: VideoData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VideoData == null)
            {
                return NotFound();
            }

            var videoData = await _context.VideoData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoData == null)
            {
                return NotFound();
            }

            return View(videoData);
        }

        // GET: VideoData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VideoData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Link,FilePath,Duration,VideoType")] VideoData videoData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videoData);
        }

        // GET: VideoData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VideoData == null)
            {
                return NotFound();
            }

            var videoData = await _context.VideoData.FindAsync(id);
            if (videoData == null)
            {
                return NotFound();
            }
            return View(videoData);
        }

        // POST: VideoData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Link,FilePath,Duration,VideoType")] VideoData videoData)
        {
            if (id != videoData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoDataExists(videoData.Id))
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
            return View(videoData);
        }

        // GET: VideoData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VideoData == null)
            {
                return NotFound();
            }

            var videoData = await _context.VideoData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoData == null)
            {
                return NotFound();
            }

            return View(videoData);
        }

        // POST: VideoData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VideoData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.VideoData'  is null.");
            }
            var videoData = await _context.VideoData.FindAsync(id);
            if (videoData != null)
            {
                _context.VideoData.Remove(videoData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoDataExists(int id)
        {
          return (_context.VideoData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
