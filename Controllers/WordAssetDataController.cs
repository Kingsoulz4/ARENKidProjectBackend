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
    public class WordAssetDataCreateParamsHolder
    {
        public IFormFile FlashCardImage { get; set; }
    }

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

        [HttpGet]
        public async Task<IActionResult> Index(string searchPattern)
        {
            if (_context.WordAssetData == null)
            {
                return Problem("Entity set 'WordAssetData'  is null.");
            }

            var wordAssets = from m in _context.WordAssetData
                             select m;

            if (!String.IsNullOrEmpty(searchPattern))
            {
                Console.WriteLine("WordAsset search " + searchPattern);
                wordAssets = wordAssets.Where(s => s.Text!.Contains(searchPattern));
            }


            return View(await wordAssets.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("ID,Text,PathAsset,SentenceType")] WordAssetData wordAssetData, [Bind("FlashCardImage")] WordAssetDataCreateParamsHolder createStructData)
        {
            if (ModelState.IsValid)
            {
                // Need To Modify
                Console.WriteLine("Create WordAsset ID: " + wordAssetData.ID);
                var destinationDir = $"/Temp/WordAssets/{wordAssetData.ID}";
                if (!Directory.Exists(destinationDir))
                {
                    Console.WriteLine("Created Word Asset Dir");
                    Directory.CreateDirectory(destinationDir);
                }

                if (createStructData != null && createStructData.FlashCardImage != null)
                {
                    var imageDestinationDir = Path.Combine(destinationDir, "images");
                    if (!Directory.Exists(imageDestinationDir))
                    {
                        Directory.CreateDirectory(imageDestinationDir);
                    }
                    var flashCardFilePath = Path.Combine(imageDestinationDir, DateTime.UtcNow.ToFileTimeUtc() + Path.GetExtension(createStructData.FlashCardImage.FileName));

                    var imageTempDir = "./Temp/images";
                    if (!Directory.Exists(imageTempDir))
                    {
                        Directory.CreateDirectory(imageTempDir);
                    }

                    var imageTempDestination = Path.Combine(imageTempDir, Path.GetFileName(flashCardFilePath));
                    using (var fileTempStream = System.IO.File.Open(imageTempDestination, FileMode.OpenOrCreate))
                    {
                        await createStructData.FlashCardImage.CopyToAsync(fileTempStream);
                    }

                    using (var fileStream = System.IO.File.Open(flashCardFilePath, FileMode.OpenOrCreate))
                    {
                        await createStructData.FlashCardImage.CopyToAsync(fileStream);

                        if (wordAssetData.Images == null)
                        {
                            wordAssetData.Images = new();
                        }

                        var imageData = new ImageData();
                        imageData.FilePath = $"images/{Path.GetFileName(flashCardFilePath)}";
                        imageData.Name = wordAssetData.Text + "_FlashCard";
                        imageData.ImageType = 123;
                        _context.Add(imageData);
                        wordAssetData.Images.Add(imageData);


                    }
                }

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

            //var wordAssetData = await _context.WordAssetData.FindAsync(id);
            var st = _context.WordAssetData
            .Include(x => x.Model3Ds)
            .Include(x => x.Audios)
            .Include(x => x.Images)
            .Include(x => x.Videos);
            var wordAssetData = st.FirstOrDefault(m => m.ID == id);
            if (wordAssetData == null)
            {
                return NotFound();
            }

            ViewData["Model3DDataID"] = new SelectList(_context.Model3DData, "Id", "Id");

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
            var filePath = Path.Combine("./Temp", Path.GetRandomFileName() + ".zip");
            using (var fileStream = System.IO.File.Open(filePath, FileMode.Create))
            {
                await fileToUpload.CopyToAsync(fileStream);

                using (var archive = new ZipArchive(fileStream))
                {
                    var extracted = "./Temp/extracted";
                    if (!Directory.Exists(extracted))
                    {
                        Directory.CreateDirectory(extracted);
                    }
                    archive.ExtractToDirectory(extracted);



                }
                return Ok();
            }


        }

        public async Task<ActionResult> AddOrRemove3DModel(long wordAssetID, long model3DID)
        {
            Console.WriteLine("AddOrRemove3DModel " + wordAssetID);

            var wordAssetToUpdate = _context.WordAssetData!.Where(x => x.ID == wordAssetID).Single();

            Console.WriteLine($"AddOrRemove3DModel wordAssetToUpdate text = {wordAssetToUpdate.Text}");

            if (wordAssetToUpdate.Model3Ds == null)
            {
                wordAssetToUpdate.Model3Ds = new();
            }

            var model3D = _context.Model3DData!.Where(x => x.Id == model3DID).Single();
            if (model3D != null || model3D != default)
            {
                wordAssetToUpdate.Model3Ds.Add(model3D);
                Console.WriteLine("AddOrRemove3DModel Added " + wordAssetToUpdate.Model3Ds.Count);
                _context.SaveChanges();
                return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
            }


            var modelBelongTo = wordAssetToUpdate.Model3Ds.FirstOrDefault(m => m.Id == model3DID);

            if (modelBelongTo != null && modelBelongTo != default)
            {
                wordAssetToUpdate.Model3Ds.Remove(modelBelongTo);
                Console.WriteLine("AddOrRemove3DModel Removed " + wordAssetToUpdate.Model3Ds.Count);
            }

            return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
        }
        public async Task<ActionResult> AddOrRemoveImage(long wordAssetID, long imageID)
        {
            Console.WriteLine("AddOrRemoveAudio " + imageID);

            var wordAssetToUpdate = _context.WordAssetData!.Where(x => x.ID == wordAssetID).Single();

            Console.WriteLine($"AddOrRemoveAudio wordAssetToUpdate text = {wordAssetToUpdate.Text}");

            if (wordAssetToUpdate.Images == null)
            {
                wordAssetToUpdate.Images = new();
            }

            var imageData = _context.ImageData!.Where(x => x.Id == imageID).Single();
            if (imageData != null || imageData != default)
            {
                wordAssetToUpdate.Images.Add(imageData);

                //Need modify here
                string sourceFile = $"./Temp/Images/{Path.GetFileName(imageData.FilePath)}";
                var destinationDir = $"./Temp/WordAssets/{wordAssetID}/images";
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }
                string destinationFile = Path.Combine(destinationDir, Path.GetFileName(imageData.FilePath!));
                try
                {
                    System.IO.File.Copy(sourceFile, destinationFile, true);
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }

                Console.WriteLine("AddOrRemoveAudio Added " + wordAssetToUpdate.Images.Count);
                _context.SaveChanges();
                return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
            }


            var imageBelongTo = wordAssetToUpdate.Images.FirstOrDefault(m => m.Id == imageID);

            if (imageBelongTo != null && imageBelongTo != default)
            {
                wordAssetToUpdate.Images.Remove(imageBelongTo);
                Console.WriteLine("AddOrRemoveAudio Removed " + wordAssetToUpdate.Images.Count);
            }

            return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
        }

        public async Task<ActionResult> AddOrRemoveAudio(long wordAssetID, long audioID)
        {
            Console.WriteLine("AddOrRemoveAudio " + audioID);

            var wordAssetToUpdate = _context.WordAssetData!.Where(x => x.ID == wordAssetID).Single();

            Console.WriteLine($"AddOrRemoveAudio wordAssetToUpdate text = {wordAssetToUpdate.Text}");

            if (wordAssetToUpdate.Audios == null)
            {
                wordAssetToUpdate.Audios = new();
            }

            var audioData = _context.AudioData!.Where(x => x.Id == audioID).Single();
            if (audioData != null || audioData != default)
            {
                wordAssetToUpdate.Audios.Add(audioData);

                //Need modify here
                string sourceFile = $"./Temp/Audios/{Path.GetFileName(audioData.FilePath)}";
                var destinationDir = $"./Temp/WordAssets/{wordAssetID}/audio";
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }
                string destinationFile = Path.Combine(destinationDir, Path.GetFileName(audioData.FilePath!));
                try
                {
                    System.IO.File.Copy(sourceFile, destinationFile, true);
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }

                Console.WriteLine("AddOrRemoveAudio Added " + wordAssetToUpdate.Audios.Count);
                _context.SaveChanges();
                return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
            }


            var audioBelongTo = wordAssetToUpdate.Audios.FirstOrDefault(m => m.Id == audioID);

            if (audioBelongTo != null && audioBelongTo != default)
            {
                wordAssetToUpdate.Audios.Remove(audioBelongTo);
                Console.WriteLine("AddOrRemoveAudio Removed " + wordAssetToUpdate.Audios.Count);
            }

            return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
        }
        public async Task<ActionResult> AddOrRemoveVideo(long wordAssetID, long videoID)
        {
            Console.WriteLine("AddOrRemoveVideo " + videoID);

            var wordAssetToUpdate = _context.WordAssetData!.Where(x => x.ID == wordAssetID).Single();

            Console.WriteLine($"AddOrRemoveVideo wordAssetToUpdate text = {wordAssetToUpdate.Text}");

            if (wordAssetToUpdate.Videos == null)
            {
                wordAssetToUpdate.Videos = new();
            }

            var videoData = _context.VideoData!.Where(x => x.Id == videoID).Single();
            if (videoData != null || videoData != default)
            {
                wordAssetToUpdate.Videos.Add(videoData);

                //Need modify here
                string sourceFile = $"./Temp/Videos/{Path.GetFileName(videoData.FilePath)}";
                var destinationDir = $"./Temp/WordAssets/{wordAssetID}/video";
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }
                string destinationFile = Path.Combine(destinationDir, Path.GetFileName(videoData.FilePath!));
                try
                {
                    System.IO.File.Copy(sourceFile, destinationFile, true);
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }

                Console.WriteLine("AddOrRemoveAudio Added " + wordAssetToUpdate.Videos!.Count);
                _context.SaveChanges();
                return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
            }


            var audioBelongTo = wordAssetToUpdate.Videos!.FirstOrDefault(m => m.Id == videoID);

            if (audioBelongTo != null && audioBelongTo != default)
            {
                wordAssetToUpdate.Videos!.Remove(audioBelongTo);
                Console.WriteLine("AddOrRemoveVideo Removed " + wordAssetToUpdate.Videos!.Count);
            }

            return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
        }

        public async Task<IActionResult> ChangeFlashCard(long wordAssetID, IFormFile imageToChange)
        {
            // Need To Modify
            // if(wordAssetToModify == null)
            // {
            //     Console.WriteLine("ChangeFlashCard wordAsset null");
            // }
            // else
            // {
            //     Console.WriteLine("ChangeFlashCard wordAsset not null");
            // }


            var wordAssetToUpdate = _context.WordAssetData!
            .Include(x => x.Images)
            .Where(x => x.ID == wordAssetID).Single();
            // var wordAssetToUpdate = wordAssetToModify;

            if (wordAssetToUpdate.Images == null)
            {
                return Problem("Word Asset Image null");
            }

            var flashCard = wordAssetToUpdate!.Images!.Find(x => x.ImageType == 123);
            var filePath = Path.Combine("./Temp/Images", Path.GetFileName(flashCard!.FilePath!));

            var wordImageAssetDir = $"./Temp/WordAssets/{wordAssetID}/images";
            if (!Directory.Exists(wordImageAssetDir))
            {
                Directory.CreateDirectory(wordImageAssetDir);
            }
            var filePathInWordAssetDir = Path.Combine(wordImageAssetDir, imageToChange.FileName);

            using (var fileInWordAssetStream = System.IO.File.Open(filePathInWordAssetDir, FileMode.OpenOrCreate))
            {
                await imageToChange.CopyToAsync(fileInWordAssetStream);
            }

            using (var fileStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate))
            {
                await imageToChange.CopyToAsync(fileStream);

                return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
            }

            //return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID});
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
            data.Audios = new();
            foreach (var au in mkData.Audio)
            {
                var audioAREn = new AudioData();
                audioAREn = JsonConvert.DeserializeObject<AudioData>(JsonConvert.SerializeObject(au));
                audioAREn.AudioType = au.VoicesId;
                audioAREn.FilePath = au.Link;
                data.Audios.Add(audioAREn);
            }

            data.Images = new();
            foreach (var img in mkData.Image)
            {
                var imageAREn = new ImageData();
                imageAREn.FilePath = img.FilePath;
                imageAREn.Id = img.Id;
                imageAREn.Link = img.Link;
                imageAREn.ImageType = img.ImagesCategoriesId;
                data.Images.Add(imageAREn);
            }

            data.Videos = new();
            foreach (var vid in mkData.Video)
            {
                var vidAREn = new VideoData();
                vidAREn = JsonConvert.DeserializeObject<VideoData>(JsonConvert.SerializeObject(vid));
                vidAREn.VideoType = (int)vid.VideoCategoriesId;
                data.Videos.Add(vidAREn);
            }

            System.IO.File.WriteAllText(jsonFile, JsonConvert.SerializeObject(data));
        }


        [HttpGet("WordAssetData/api/WordAssets/{id}")]
        public async Task<IActionResult> GetWordAssetByID(long id)
        {
            if (_context.Model3DData != null && _context.Model3DData?.Count() != 0)
            {
                var list_model = await _context.WordAssetData!
                .Include(x => x.Audios)
                .Include(x => x.Images)
                .Include(x => x.Model3Ds)
                .Include(x => x.Videos)
                .Where(x => x.ID == id)
                .ToListAsync();
                return new OkObjectResult(new {data = JsonConvert.SerializeObject(list_model)});
            }

            return new JsonResult(NotFound());
        }
    }
}
