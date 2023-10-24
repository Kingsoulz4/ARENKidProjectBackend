using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectBackend.Models;

namespace ProjectBackend.Controllers
{
    public class WordAssetDataCreateParamsHolder
    {
        public IFormFile? FlashCardImage { get; set; }
    }

    [Authorize]
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
            Console.WriteLine(ConfigurationManager.Instance!.Configuration!["Something"]);
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

                var doneFile1 = false;
                var doneFile2 = false;

                _context.Add(wordAssetData);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Created wordasset ID: {wordAssetData.ID}");

                var destinationDir = $"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/WordAssets/{wordAssetData.ID}";
                if (!Directory.Exists(destinationDir))
                {
                    Console.WriteLine("Created Word Asset Dir");
                    Directory.CreateDirectory(destinationDir);
                }

                if (createStructData != null && createStructData.FlashCardImage != null)
                {
                    var imageDestinationDir = Path.Combine(destinationDir, "Images");
                    if (!Directory.Exists(imageDestinationDir))
                    {
                        Directory.CreateDirectory(imageDestinationDir);
                    }
                    var flashCardFilePath = Path.Combine(imageDestinationDir, $"flashcard_word_id_{wordAssetData.ID}{Path.GetExtension(createStructData.FlashCardImage.FileName)}");

                    var imageTempDir = $"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/Flashcards";

                    Console.WriteLine("Create image TempDir" + imageTempDir);

                    if (!Directory.Exists(imageTempDir))
                    {
                        Directory.CreateDirectory(imageTempDir);
                    }

                    var imageTempDestination = Path.Combine(imageTempDir, Path.GetFileName(flashCardFilePath));

                    using (var fileTempStream = System.IO.File.Open(imageTempDestination, FileMode.OpenOrCreate))
                    {
                        await createStructData.FlashCardImage.CopyToAsync(fileTempStream);
                        doneFile1 = true;
                    }

                    using (var fileStream = System.IO.File.Open(flashCardFilePath, FileMode.OpenOrCreate))
                    {
                        await createStructData.FlashCardImage.CopyToAsync(fileStream);

                        if (wordAssetData.Images == null)
                        {
                            wordAssetData.Images = new();
                        }

                        var imageData = new ImageData
                        {
                            FilePath = $"{Path.GetFileName(flashCardFilePath)}",
                            Link = $"Flashcards/{Path.GetFileName(flashCardFilePath)}",
                            Name = $"{wordAssetData.Text}_{wordAssetData.ID}_FlashCard",
                            ImageType = (long)AssetDataType.ImageType.Flashcard
                        };
                        _context.Add(imageData);
                        wordAssetData.Images.Add(imageData);
                        await _context.SaveChangesAsync();
                        doneFile2 = true;
                    }
                }

                // _context.Add(wordAssetData);

                while (!(doneFile1 && doneFile2)) ;

                UpdateConfigFile(wordAssetData.ID);

                AssetBuilder.ImportAssetAndBuildAssetBundle();

                //Need To Modify
                var endPoint = $"https://localhost:7253/WordAssetData/api/download?fileName={wordAssetData.ID}.unity3d";;

                var wordAssetInfoDownload = new WordAssets{
                    Id = wordAssetData.ID,
                    Text = wordAssetData.Text,
                    LinkDownLoad = endPoint,
                    DateCreated = DateTime.Now

                };

                _context.Add(wordAssetInfoDownload);
                
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

            var flashCard = wordAssetData.Images!.Find(x => x.ImageType == (long)AssetDataType.ImageType.Flashcard);

            // Need To Modify
            ViewData["Model3DDataID"] = new SelectList(_context.Model3DData, "Id", "Id");
            ViewBag.FlashCardPath = $"{ConfigurationManager.Instance!.GetUnityDataBuildRelativePath()}/{flashCard!.Link}";

            await Task.Yield();

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

                    UpdateConfigFile(wordAssetData.ID);

                    AssetBuilder.ImportAssetAndBuildAssetBundle();
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

            await Task.Yield();

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
                string sourceFile = $"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/Images/{Path.GetFileName(imageData.FilePath)}";
                var destinationDir = $"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/WordAssets/{wordAssetID}/Images";
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

            await Task.Yield();

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
                string sourceFile = $"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/Audios/{Path.GetFileName(audioData.FilePath)}";
                var destinationDir = $"{ConfigurationManager.Instance.GetUnityDataBuildAbsolutePath()}/WordAssets/{wordAssetID}/Audios";
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

            await Task.Yield();

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
                string sourceFile = $"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/Videos/{Path.GetFileName(videoData.FilePath)}";
                var destinationDir = $"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/WordAssets/{wordAssetID}/Videos";
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

            await Task.Yield();

            return RedirectToAction(nameof(Edit), new { id = wordAssetToUpdate.ID });
        }

        public async Task<IActionResult> ChangeFlashCard(long wordAssetID, IFormFile imageToChange)
        {
            // Need To Modify
            var wordAssetToUpdate = _context.WordAssetData!
            .Include(x => x.Images)
            .Where(x => x.ID == wordAssetID).Single();

            if (wordAssetToUpdate.Images == null)
            {
                return Problem("Word Asset Image null");
            }

            var flashCard = wordAssetToUpdate!.Images!.Find(x => x.ImageType == (long)AssetDataType.ImageType.Flashcard);
            var filePath = Path.Combine($"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/Flashcards", Path.GetFileName(flashCard!.FilePath!));

            var wordImageAssetDir = $"{ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath()}/WordAssets/{wordAssetID}/Images";
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
        }

        private async void UpdateConfigFile(long id)
        {
            if (_context.Model3DData != null && _context.Model3DData?.Count() != 0)
            {
                var list_model = await _context.WordAssetData!
                .Include(wordAssetData => wordAssetData.Audios)
                .Include(wordAssetData => wordAssetData.Images)
                .Include(wordAssetData => wordAssetData.Model3Ds)
                    .ThenInclude(model3D => model3D.Behavior)
                .Include(wordAssetData => wordAssetData.Videos)
                
                
                .Where(x => x.ID == id)
                .ToListAsync();
                var data = JsonConvert.SerializeObject(list_model.FirstOrDefault());

                var filePath = Path.Combine(ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath(), $"{DataDirectoryNames.WordAssetsDir}/{id}/{id}.json");

                using (var fileStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(data);
                    await fileStream.WriteAsync(info, 0, info.Length);
                }
            }
        }

        private void ConvertOldData(string jsonOldData)
        {

            var jsonFile = jsonOldData;

            var jsonData = System.IO.File.ReadAllText(jsonFile);

            var mkData = JsonConvert.DeserializeObject<MKWordData>(jsonData);

            var data = new WordAssetData();
            data.Text = mkData!.Text;
            data.ID = mkData.WordId;
            data.PathAsset = mkData.PathWord;
            data.Audios = new();
            foreach (var au in mkData.Audio!)
            {
                var audioAREn = new AudioData();
                audioAREn = JsonConvert.DeserializeObject<AudioData>(JsonConvert.SerializeObject(au));
                audioAREn!.AudioType = au.VoicesId;
                audioAREn.FilePath = au.Link;
                data.Audios.Add(audioAREn);
            }

            data.Images = new();
            foreach (var img in mkData.Image!)
            {
                var imageAREn = new ImageData();
                imageAREn.FilePath = img.FilePath;
                imageAREn.Id = img.Id;
                imageAREn.Link = img.Link;
                imageAREn.ImageType = img.ImagesCategoriesId;
                data.Images.Add(imageAREn);
            }

            data.Videos = new();
            foreach (var vid in mkData.Video!)
            {
                var vidAREn = new VideoData();
                vidAREn = JsonConvert.DeserializeObject<VideoData>(JsonConvert.SerializeObject(vid));
                if (vidAREn != null)
                {
                    vidAREn.VideoType = (int)vid.VideoCategoriesId;
                    data.Videos.Add(vidAREn);
                }
                else
                {

                }
            }

            System.IO.File.WriteAllText(jsonFile, JsonConvert.SerializeObject(data));
        }


        [HttpGet("WordAssetData/api/WordAssets/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                return new OkObjectResult(new { data = JsonConvert.SerializeObject(list_model) });
            }

            return new JsonResult(NotFound());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("WordAssetData/api/download/{platform}/{fileName}")] //Need To Modify
        public async Task<IActionResult> DownloadWordAssetDataFile(string platform, string fileName)
        {
            var filepath = Path.Combine(ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath(), $"{DataDirectoryNames.AssetBundlesDir}/{platform}", fileName);

            if(!System.IO.File.Exists(filepath))
            {
                return Problem("File Not Found");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("WordAssetData/api/download/flashcard/{platform}")]
        public async Task<IActionResult> DownloadFlashcardBundle(string platform
        )
        {
            var filepath = Path.Combine(ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath(), $"{DataDirectoryNames.AssetBundlesDir}/{platform}", "flashcards.unity3d");

            if(!System.IO.File.Exists(filepath))
            {
                return Problem("File Not Found");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("WordAssetData/api/download/flashcard/zip")]
        public async Task<IActionResult> DownloadFlashcardZip()
        {
            var filepath = Path.Combine(ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath(), "Flashcards.zip");

            if(!System.IO.File.Exists(filepath))
            {
                return Problem("File Not Found");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }
    }
}
