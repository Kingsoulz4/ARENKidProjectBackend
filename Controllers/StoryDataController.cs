using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectBackend.Models;

namespace ProjectBackend.Controllers
{
    public class StoryDataParamsHolder
    {
        public IFormFile? StoryConfigContent { get; set; }
    }

    public class StoryDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public StoryDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: StoryData
        public async Task<IActionResult> Index()
        {
            return _context.StoryData != null ?
                        View(await _context.StoryData.ToListAsync()) :
                        Problem("Entity set 'MvcWordAssetsContext.StoryData'  is null.");
        }

        // GET: StoryData/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.StoryData == null)
            {
                return NotFound();
            }

            var storyData = await _context.StoryData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storyData == null)
            {
                return NotFound();
            }

            return View(storyData);
        }

        // GET: StoryData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoryData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Thumb,Category,Type,StoryDataConfigContent")] StoryData storyData, [Bind("StoryConfigContent")] StoryDataParamsHolder paramHolder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storyData);
                await _context.SaveChangesAsync();

                var jsonFile = paramHolder.StoryConfigContent;
                if (jsonFile != null && jsonFile.Length > 0)
                {
                    using (var reader = new StreamReader(jsonFile.OpenReadStream()))
                    {
                        var content = reader.ReadToEnd();
                        var jsonData = JsonConvert.DeserializeObject<StoryConfigData>(content);
                        if(jsonData != null)
                        {
                            jsonData.Id = storyData.Id;
                            storyData.StoryDataConfigContent = JsonConvert.SerializeObject(jsonData);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            return Problem("Upload config file fail");
                        }

                    }
                    
                }

                
            }
            return View(storyData);
        }

        // GET: StoryData/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.StoryData == null)
            {
                return NotFound();
            }

            var storyData = await _context.StoryData.FindAsync(id);
            if (storyData == null)
            {
                return NotFound();
            }
            return View(storyData);
        }

        // POST: StoryData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Thumb,Category,Type,StoryDataConfigContent")] StoryData storyData)
        {
            if (id != storyData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storyData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoryDataExists(storyData.Id))
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
            return View(storyData);
        }

        // GET: StoryData/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.StoryData == null)
            {
                return NotFound();
            }

            var storyData = await _context.StoryData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storyData == null)
            {
                return NotFound();
            }

            return View(storyData);
        }

        // POST: StoryData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.StoryData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.StoryData'  is null.");
            }
            var storyData = await _context.StoryData.FindAsync(id);
            if (storyData != null)
            {
                _context.StoryData.Remove(storyData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoryDataExists(long id)
        {
            return (_context.StoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
