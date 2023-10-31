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
    public class TopicDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public TopicDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: TopicData
        public async Task<IActionResult> Index()
        {
              return _context.TopicData != null ? 
                          View(await _context.TopicData.ToListAsync()) :
                          Problem("Entity set 'MvcWordAssetsContext.TopicData'  is null.");
        }

        // GET: TopicData/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TopicData == null)
            {
                return NotFound();
            }

            var topicData = await _context.TopicData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topicData == null)
            {
                return NotFound();
            }

            return View(topicData);
        }

        // GET: TopicData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopicData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Thumb")] TopicData topicData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topicData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topicData);
        }

        // GET: TopicData/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TopicData == null)
            {
                return NotFound();
            }

            var topicData = await _context.TopicData.FindAsync(id);
            if (topicData == null)
            {
                return NotFound();
            }
            return View(topicData);
        }

        // POST: TopicData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Thumb")] TopicData topicData)
        {
            if (id != topicData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicDataExists(topicData.Id))
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
            return View(topicData);
        }

        // GET: TopicData/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TopicData == null)
            {
                return NotFound();
            }

            var topicData = await _context.TopicData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topicData == null)
            {
                return NotFound();
            }

            return View(topicData);
        }

        // POST: TopicData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TopicData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.TopicData'  is null.");
            }
            var topicData = await _context.TopicData.FindAsync(id);
            if (topicData != null)
            {
                _context.TopicData.Remove(topicData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicDataExists(string id)
        {
          return (_context.TopicData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
