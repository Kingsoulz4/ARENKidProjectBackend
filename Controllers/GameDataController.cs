using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GameDataController : Controller
    {
        private readonly MvcWordAssetsContext _context;

        public GameDataController(MvcWordAssetsContext context)
        {
            _context = context;
        }

        // GET: GameData
        public async Task<IActionResult> Index()
        {
              return _context.GameData != null ? 
                          View(await _context.GameData.ToListAsync()) :
                          Problem("Entity set 'MvcWordAssetsContext.GameData'  is null.");
        }

        // GET: GameData/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.GameData == null)
            {
                return NotFound();
            }

            var gameData = await _context.GameData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gameData == null)
            {
                return NotFound();
            }

            return View(gameData);
        }

        // GET: GameData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Type,Thumb")] GameData gameData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameData);
        }

        // GET: GameData/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.GameData == null)
            {
                return NotFound();
            }

            var gameData = await _context.GameData.FindAsync(id);
            if (gameData == null)
            {
                return NotFound();
            }
            return View(gameData);
        }

        // POST: GameData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,Name,Type,Thumb")] GameData gameData)
        {
            if (id != gameData.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameDataExists(gameData.ID))
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
            return View(gameData);
        }

        // GET: GameData/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.GameData == null)
            {
                return NotFound();
            }

            var gameData = await _context.GameData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gameData == null)
            {
                return NotFound();
            }

            return View(gameData);
        }

        // POST: GameData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.GameData == null)
            {
                return Problem("Entity set 'MvcWordAssetsContext.GameData'  is null.");
            }
            var gameData = await _context.GameData.FindAsync(id);
            if (gameData != null)
            {
                _context.GameData.Remove(gameData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameDataExists(long id)
        {
          return (_context.GameData?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        [HttpGet]
        [Route("GameData/api/data/{id}")]
        public async Task<IActionResult> GetGameDataById(long id)
        {
            var model3DData = _context.GameData!
            .Where(x => x.ID == id)
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
