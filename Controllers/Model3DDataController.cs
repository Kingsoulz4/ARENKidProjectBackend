using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using ProjectBackend.Models;

namespace ProjectBackend.Controllers
{

    public class Model3DDataParamsHolder
    {
        public IFormFile? File3DModel { get; set; }
    }

    [Authorize]
    public class Model3DDataController : Controller
    {
        private static string ApiKey = "AIzaSyCZBeMCx5PYzlkZepR74hOHj-FbXZlgVbQ";
        private static string Bucket = "arenkid-projectbe.appspot.com";
        private static string AuthEmail = "kingsoulz4d@gmail.com";
        private static string AuthPassword = "55444664";

        private readonly MvcWordAssetsContext _context;

        public Model3DDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: Model3DData
        public async Task<IActionResult> Index()
        {
            return _context.Model3DData != null ?
                        View(await _context.Model3DData.ToListAsync()) :
                        Problem("Entity set 'MvcWordAssetsContext.Model3DData'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchPattern)
        {
            if (_context.Model3DData == null)
            {
                return Problem("Entity set 'Model3DData'  is null.");
            }

            var model3DData = from m in _context.Model3DData
                              select m;

            if (!String.IsNullOrEmpty(searchPattern))
            {
                Console.WriteLine("VideoData search " + searchPattern);
                model3DData = model3DData.Where(s => s.Name!.Contains(searchPattern));
            }


            return View(await model3DData.ToListAsync());
        }

        // GET: Model3DData/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Model3DData == null)
            {
                return NotFound();
            }

            var model3DData = await _context.Model3DData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model3DData == null)
            {
                return NotFound();
            }

            return View(model3DData);
        }

        // GET: Model3DData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Model3DData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LinkDownload,FileType, ScaleFactor, Location, NameAsset, WordDescription")] Model3DData model3DData, [Bind("File3DModel")] Model3DDataParamsHolder paramsHolder)
        {
            Console.WriteLine("Model valid " + ModelState.IsValid);
            //Console.WriteLine("Model valid " + ModelState.);
            if (ModelState.IsValid)
            {
                bool uploadDone = false;

                //var resultUpload = await UploadHelper.UpLoadFileToFirebaseStorage(fileToUpload, ApiKey, AuthEmail, AuthPassword, Bucket, "something/data");

                var destinationDir = Path.Combine(ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath(), DataDirectoryNames.Model3DDataDir);

                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }

                var fileToUpload = paramsHolder.File3DModel;

                if (fileToUpload != null)
                {

                    var filePath = Path.Combine(destinationDir, DateTime.Now.ToFileTimeUtc() + Path.GetExtension(fileToUpload.FileName));

                    using (var fileStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate))
                    {
                        await fileToUpload.CopyToAsync(fileStream);
                        uploadDone = true;
                    }

                    while (!uploadDone) ;

                    //Need To Modify
                    var endPoint = $"https://localhost:7253/Model3DData/api/download?fileName={Path.GetFileName(filePath)}";

                    // Console.WriteLine("Upload res: " + resultUpload.ToJToken());
                    // var jsonTokRes = resultUpload.ToJToken();
                    // var val = jsonTokRes.SelectToken("Value");
                    // var linkDownload = val!.Value<string>("message");
                    var linkDownload = endPoint;
                    Console.WriteLine("ModelDDataController Download link: " + linkDownload);

                    model3DData.FileType = Path.GetExtension(fileToUpload.FileName);
                    model3DData.LinkDownload = linkDownload;
                }

                _context.Add(model3DData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model3DData);
        }

        // GET: Model3DData/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Model3DData == null)
            {
                return NotFound();
            }

            var model3DData = _context.Model3DData
            .Include(x => x.Behavior)
            .FirstOrDefault(x => x.Id == id);
            if (model3DData == null)
            {
                return NotFound();
            }

            await Task.Yield();

            return View(model3DData);
        }

        // POST: Model3DData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,LinkDownload,FileType, ScaleFactor, Location, NameAsset, WordDescription")] Model3DData model3DData)
        {
            if (id != model3DData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model3DData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Model3DDataExists(model3DData.Id))
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
            return View(model3DData);
        }

        // GET: Model3DData/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Model3DData == null)
            {
                return NotFound();
            }

            var model3DData = await _context.Model3DData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model3DData == null)
            {
                return NotFound();
            }

            return View(model3DData);
        }

        // POST: Model3DData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Model3DData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.Model3DData'  is null.");
            }
            var model3DData = await _context.Model3DData.FindAsync(id);
            if (model3DData != null)
            {
                _context.Model3DData.Remove(model3DData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Model3DDataExists(long id)
        {
            return (_context.Model3DData?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> AddOrRemoveBehavior(long model3DID, long behaviorID)
        {

            var model3D = _context.Model3DData!
            .Include(x => x.Behavior)
            .Where(x => x.Id == model3DID).Single();

            var behavior = _context.Mode3DBehaviorData!.Where(x => x.Id == behaviorID).Single();

            if (behavior == null)
            {
                return Problem("Behavior not exist");
            }

            var behaviorBelongTo = model3D.Behavior!.Find(x => x.Id == behavior.Id);

            if (!model3D.Behavior!.Any() ||
                 behaviorBelongTo != null)
            {
                model3D.Behavior!.Add(behavior);
            }
            else
            {
                model3D.Behavior.Remove(behaviorBelongTo!);
            }

            await Task.Yield();

            return RedirectToAction(nameof(Edit), new { id = model3DID });
        }


        [HttpPost]
        public async Task<ActionResult> UploadModel3DToCloud(IFormFile fileToUpload)
        {
            await UploadHelper.UpLoadFileToFirebaseStorage(fileToUpload, ApiKey, AuthEmail, AuthPassword, Bucket, "something/data");

            return Ok();

        }


        // GET: Model3DData/api/list_model
        [HttpGet("Model3DData/api/list_model")]
        public async Task<JsonResult> GetListModelDataAsJson()
        {
            if (_context.Model3DData != null && _context.Model3DData?.Count() != 0)
            {
                var list_model = await _context.Model3DData!.ToListAsync();
                return new JsonResult(JsonConvert.SerializeObject(list_model));
            }

            return new JsonResult(NotFound());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Model3DData/api/download")]
        public async Task<IActionResult> DownloadModel3DFile(string filename)
        {
            var filepath = Path.Combine(ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath(), "Model3Ds", filename);

            if (!System.IO.File.Exists(filepath))
            {
                return Problem("Not found");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        [HttpGet]
        [Route("Model3DData/api/download/bundle/{platform}")]
        public async Task<IActionResult> DownloadModel3DAssetBundle(string platform)
        {
            var filepath = Path.Combine(ConfigurationManager.Instance!.GetUnityDataBuildAbsolutePath(), $"{DataDirectoryNames.AssetBundlesDir}/{platform}", "model3d.unity3d");

            if (!System.IO.File.Exists(filepath))
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

        [AllowAnonymous]
        [HttpGet]
        [Route("Model3DData/api/get_info/{id}")]
        public async Task<IActionResult> GetModel3DDataByID(long id)
        {
            var model3DData = _context.Model3DData!
            .Include(x => x.Behavior)
            .Where(x => x.Id == id)
            .Single()
            ;
            if (model3DData == null)
            {
                return NotFound();
            }

            await Task.Yield();

            return new OkObjectResult(new { data = JsonConvert.SerializeObject(model3DData) });
        }

        



    }
}
