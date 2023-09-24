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
    public class ImageCreateDataHolder
    {
        public IFormFile? imageFile { get; set; }
    }

    public class ImageDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public ImageDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: ImageData
        public async Task<IActionResult> Index()
        {
            return _context.ImageData != null ?
                        View(await _context.ImageData.ToListAsync()) :
                        Problem("Entity set 'MvcWordAssetsContext.ImageData'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchPattern)
        {
            if (_context.ImageData == null)
            {
                return Problem("Entity set 'AudioData'  is null.");
            }

            var imageData = from m in _context.ImageData
                         select m;

            if (!String.IsNullOrEmpty(searchPattern))
            {
                Console.WriteLine("ImageData search " + searchPattern);
                imageData = imageData.Where(s => s.Name!.Contains(searchPattern));
            }


            return View(await imageData.ToListAsync());
        }

        // GET: ImageData/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ImageData == null)
            {
                return NotFound();
            }

            var imageData = await _context.ImageData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageData == null)
            {
                return NotFound();
            }

            return View(imageData);
        }

        // GET: ImageData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImageData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Link,FilePath,ImageType")] ImageData imageData, [Bind("imageFile")] ImageCreateDataHolder imageCreateDataHolder)
        {

            if (ModelState.IsValid)
            {
                if (imageCreateDataHolder != null && imageCreateDataHolder.imageFile != null)
                {
                    //Need To Modify
                    var destinationDir = "./Temp/Images";
                    if (!Directory.Exists(destinationDir))
                    {
                        Directory.CreateDirectory(destinationDir);
                    }

                    var filePath = Path.Combine(destinationDir, DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(imageCreateDataHolder.imageFile!.FileName));

                    using (var fileStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate))
                    {

                        await imageCreateDataHolder.imageFile.CopyToAsync(fileStream);

                        
                    }

                    imageData.FilePath = Path.GetFileName(filePath);
                    imageData.Link = $"images/{imageData.FilePath}";
                }

                
                _context.Add(imageData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageData);
        }

        // GET: ImageData/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ImageData == null)
            {
                return NotFound();
            }

            var imageData = await _context.ImageData.FindAsync(id);
            if (imageData == null)
            {
                return NotFound();
            }
            return View(imageData);
        }

        // POST: ImageData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Link,FilePath,ImageType")] ImageData imageData)
        {
            if (id != imageData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageDataExists(imageData.Id))
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
            return View(imageData);
        }

        // GET: ImageData/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ImageData == null)
            {
                return NotFound();
            }

            var imageData = await _context.ImageData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageData == null)
            {
                return NotFound();
            }

            return View(imageData);
        }

        // POST: ImageData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ImageData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.ImageData'  is null.");
            }
            var imageData = await _context.ImageData.FindAsync(id);
            if (imageData != null)
            {
                _context.ImageData.Remove(imageData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageDataExists(long id)
        {
            return (_context.ImageData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}