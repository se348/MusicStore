﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Administrator")]
    public class AlbumsController : Controller
    {
        private readonly StoreContext _context;

        public AlbumsController(StoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Albums
        public async Task<IActionResult> Index()
        {
            var storeContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(await storeContext.ToListAsync());
        }

        // GET: Admin/Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Admin/Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }

        // POST: Admin/Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumID,GenreID,ArtistID,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Admin/Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            Console.WriteLine(album.ToString());

            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // POST: Admin/Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumID,GenreID,ArtistID,Title,Price,AlbumArtUrl")] Album album)
        {
            if (id != album.AlbumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumID))
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
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Admin/Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Admin/Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'StoreContext.Albums'  is null.");
            }
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
          return _context.Albums.Any(e => e.AlbumID == id);
        }
    }
}
