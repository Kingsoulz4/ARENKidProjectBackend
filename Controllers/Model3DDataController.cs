using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using ProjectBackend.Models;

namespace ProjectBackend.Controllers
{
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
        public async Task<IActionResult> Create([Bind("Id,Name,LinkDownload,FileType")] Model3DData model3DData, IFormFile fileToUpload)
        {
            var resultUpload = await UploadHelper.UpLoadFileToFirebaseStorage(fileToUpload, ApiKey, AuthEmail, AuthPassword, Bucket, "something/data");

            Console.WriteLine("Upload res: "+ resultUpload.ToJToken());
            var jsonTokRes = resultUpload.ToJToken();
            var val = jsonTokRes.SelectToken("Value");
            var linkDownload = val.Value<string>("message");
            Console.WriteLine("ModelDDataController Download link: " + linkDownload);

            model3DData.FileType = Path.GetExtension(fileToUpload.FileName);
            model3DData.LinkDownload = linkDownload;

            if (ModelState.IsValid)
            {
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

            var model3DData = await _context.Model3DData.FindAsync(id);
            if (model3DData == null)
            {
                return NotFound();
            }
            return View(model3DData);
        }

        // POST: Model3DData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,LinkDownload,FileType")] Model3DData model3DData)
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

        [HttpPost]
        public async Task<ActionResult> UploadModel3DToCloud(IFormFile  fileToUpload)
        {
            await UploadHelper.UpLoadFileToFirebaseStorage(fileToUpload, ApiKey, AuthEmail, AuthPassword, Bucket, "something/data");

            return Ok();

        }


        // GET: Model3DData/api/list_model
        [HttpGet("Model3DData/api/list_model")]
        public async Task<JsonResult> GetListModelDataAsJson()
        {
            if(_context.Model3DData != null && _context.Model3DData?.Count() != 0)
            {
                var list_model = await _context.Model3DData.ToListAsync();
                return new JsonResult(JsonConvert.SerializeObject(list_model));
            }

            return new JsonResult(NotFound());
        }

    }
}
