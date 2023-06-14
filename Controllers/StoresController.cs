using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudWithGeocoding.Models;
using CrudWithGeocoding.Services;
using CrudWithGeocoding.Models.Geocoding;

namespace CrudWithGeocoding.Controllers
{
    public class StoresController : Controller
    {
        private readonly AppDbContext _context;

        public StoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
              return _context.Stores != null ? 
                          View(await _context.Stores.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Stores'  is null.");
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            ViewBag.Companies = _context.Companies.ToList();
            return View();
        }

        // POST: Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,Name,Address,City,Zip,Country,Longitude,Latitude")] Store store)
        {
            //Add GeocodingService and provide your API Key
            var geocodingService = new GeocodingService("AIzaSyDvZAscT76jYQm_nFAfcfUn30As6DHmoRU");
            if (ModelState.IsValid)
            {
                var geocodingResponseAsTuple = await geocodingService.GetGeocodingResponseAsTuple(store.Address, store.City);
                store.Latitude = geocodingResponseAsTuple.Item1;
                store.Longitude = geocodingResponseAsTuple.Item2;

                _context.Add(store);
                var company = _context.Companies.Find(store.CompanyId);
                company.Stores.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewBag.Companies = _context.Companies.ToList();
            return View(store);
        }

        // POST: Stores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CompanyId,Name,Address,City,Zip,Country,Longitude,Latitude")] Store store)
        {
            if (id != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Add GeocodingService and provide your API Key
                var geocodingService = new GeocodingService("AIzaSyDvZAscT76jYQm_nFAfcfUn30As6DHmoRU");
                try
                {
                    var geocodingResponseAsTuple = await geocodingService.GetGeocodingResponseAsTuple(store.Address, store.City);
                    store.Latitude = geocodingResponseAsTuple.Item1;
                    store.Longitude = geocodingResponseAsTuple.Item2;

                    _context.Update(store);
                    _context.Entry(store).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Id))
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
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Stores == null)
            {
                return Problem("Entity set 'AppDbContext.Stores'  is null.");
            }
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(Guid id)
        {
          return (_context.Stores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
