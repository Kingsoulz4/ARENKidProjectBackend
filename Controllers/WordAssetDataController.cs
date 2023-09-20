using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectBackend.Models;

namespace ProjectBackend.Controllers
{
    public class WordAssetDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public WordAssetDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: WordAssetData
        public async Task<IActionResult> Index()
        {
              return _context.WordAssetData != null ? 
                          View(await _context.WordAssetData.ToListAsync()) :
                          Problem("Entity set 'MvcWordAssetsContext.WordAssetData'  is null.");
        }

        // GET: WordAssetData/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.WordAssetData == null)
            {
                return NotFound();
            }

            var wordAssetData = await _context.WordAssetData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wordAssetData == null)
            {
                return NotFound();
            }

            return View(wordAssetData);
        }

        // GET: WordAssetData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WordAssetData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Text,PathAsset,SentenceType")] WordAssetData wordAssetData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wordAssetData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wordAssetData);
        }

        // GET: WordAssetData/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.WordAssetData == null)
            {
                return NotFound();
            }

            var wordAssetData = await _context.WordAssetData.FindAsync(id);
            if (wordAssetData == null)
            {
                return NotFound();
            }
            return View(wordAssetData);
        }

        // POST: WordAssetData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,Text,PathAsset,SentenceType")] WordAssetData wordAssetData)
        {
            if (id != wordAssetData.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wordAssetData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordAssetDataExists(wordAssetData.ID))
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
            return View(wordAssetData);
        }

        // GET: WordAssetData/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.WordAssetData == null)
            {
                return NotFound();
            }

            var wordAssetData = await _context.WordAssetData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wordAssetData == null)
            {
                return NotFound();
            }

            return View(wordAssetData);
        }

        // POST: WordAssetData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.WordAssetData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.WordAssetData'  is null.");
            }
            var wordAssetData = await _context.WordAssetData.FindAsync(id);
            if (wordAssetData != null)
            {
                _context.WordAssetData.Remove(wordAssetData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordAssetDataExists(long id)
        {
          return (_context.WordAssetData?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        // GET: WordAssetData/CreateByFile
        public IActionResult CreateByFile()
        {
            return View();
        }

        // POST: WordAssetData/CreateByFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByFile(IFormFile fileToUpload)
        {
            var filePath = Path.Combine("./Temp", Path.GetRandomFileName()+".zip");
            using(var fileStream = System.IO.File.Open(filePath, FileMode.Create))
            {
                await fileToUpload.CopyToAsync(fileStream);

                using(var archive  = new ZipArchive(fileStream))
                {
                    var extracted = "./Temp/extracted";
                    if(!Directory.Exists(extracted))
                    {
                        Directory.CreateDirectory(extracted);
                    }
                    archive.ExtractToDirectory(extracted);

                    
                    
                }
                return Ok();
            }
            
            
        }

        private void ConvertOldData(string jsonOldData)
        {

            var jsonFile = jsonOldData;

            var jsonData = System.IO.File.ReadAllText(jsonFile);

            var mkData = JsonConvert.DeserializeObject<MKWordData>(jsonData);


            var data = new WordAssetData();
            data.Text = mkData.Text;
            data.ID = mkData.WordId;
            data.PathAsset = mkData.PathWord;
            data.Audio = new();
            foreach (var au in mkData.Audio)
            {
                var audioAREn = new AudioData();
                audioAREn = JsonConvert.DeserializeObject<AudioData>(JsonConvert.SerializeObject(au));
                audioAREn.AudioType = au.VoicesId;
                audioAREn.FilePath = au.Link;
                data.Audio.Add(audioAREn);
            }

            data.Image = new();
            foreach (var img in mkData.Image)
            {
                var imageAREn = new ImageData();
                imageAREn.FilePath = img.FilePath;
                imageAREn.Id = img.Id;
                imageAREn.Link = img.Link;
                imageAREn.ImageType = img.ImagesCategoriesId;
                data.Image.Add(imageAREn);
            }

            data.Video = new();
            foreach (var vid in mkData.Video)
            {
                var vidAREn = new VideoData();
                vidAREn = JsonConvert.DeserializeObject<VideoData>(JsonConvert.SerializeObject(vid));
                vidAREn.VideoType = (int)vid.VideoCategoriesId;
                data.Video.Add(vidAREn);
            }

            System.IO.File.WriteAllText(jsonFile, JsonConvert.SerializeObject(data));
        }
    }
}
