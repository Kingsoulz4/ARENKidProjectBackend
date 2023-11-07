using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Details(long? id)
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
        public async Task<IActionResult> Edit(long? id)
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Thumb")] TopicData topicData)
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
        public async Task<IActionResult> Delete(long? id)
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
        public async Task<IActionResult> DeleteConfirmed(long id)
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

        private bool TopicDataExists(long id)
        {
          return (_context.TopicData?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("TopicData/api/data")]
        public async Task<IActionResult> GetModel3DDataByID()
        {
            var listTopicData = await _context.TopicData!
            .Include(x => x.WordAssetDatas)
            .Where(x => !x.Name.Equals("Normal Sentences"))
            .ToListAsync()
            ;
            if (listTopicData == null)
            {
                return NotFound();
            }

            List<Dictionary<string, object>> listData = new();
            foreach(var topicData in listTopicData)
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(topicData));
                List<long> listWordAssetID = new();
                topicData.WordAssetDatas!.ForEach(x => listWordAssetID.Add(x.ID));
                data["list_word_asset"] = listWordAssetID;
                listData.Add(data);
            }

            await Task.Yield();

            return new OkObjectResult(new { data = JsonConvert.SerializeObject(listData) });
        }
    }
}
