using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ProjectBackend.Models;

namespace ProjectBackend.Controllers
{
    public class VideoDataParamsHolder
    {
        public IFormFile? VideoFile { get; set; }
    }

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


        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchPattern)
        {
            if (_context.VideoData == null)
            {
                return Problem("Entity set 'VideoData'  is null.");
            }

            var videoData = from m in _context.VideoData
                         select m;

            if (!String.IsNullOrEmpty(searchPattern))
            {
                Console.WriteLine("VideoData search " + searchPattern);
                videoData = videoData.Where(s => s.Name!.Contains(searchPattern));
            }

            return View(await videoData.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,Link,FilePath,Duration,VideoType")] VideoData videoData, [Bind("VideoFile")] VideoDataParamsHolder videoCreateDataHolder)
        {
            //Need To Modify
            var destinationDir = "./Temp/Videos";
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }
            var filePath = Path.Combine(destinationDir, DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(videoCreateDataHolder.VideoFile!.FileName));
            
            using (var fileStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate))
            {

                await videoCreateDataHolder.VideoFile.CopyToAsync(fileStream);
                videoData.FilePath = Path.GetFileName(filePath);
                videoData.Link = "video/" + Path.GetFileName(filePath);

                if (ModelState.IsValid)
                {
                    _context.Add(videoData);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(videoData);
            }
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

            ViewBag.VideoFile = Path.Combine("./Temp/Videos", videoData.FilePath!);
            
            return View(videoData);
        }

        // POST: VideoData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Link,FilePath,Duration,VideoType")] VideoData videoData, [Bind("VideoFile")] VideoDataParamsHolder videoDataParamsHolder)
        {
            if (id != videoData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Need To Modify
                if(videoDataParamsHolder != null && videoDataParamsHolder.VideoFile != null)
                {
                    var filePath = Path.Combine("./Temp/Videos", Path.GetFileName(videoData.FilePath!));
                    using(var fileStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate))
                    {
                        await videoDataParamsHolder.VideoFile.CopyToAsync(fileStream);
                    }
                }

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
