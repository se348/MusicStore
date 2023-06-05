using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext storeContext;
        public StoreController(StoreContext storeContext) 
        { 
            this.storeContext = storeContext;
        }
        public IActionResult ListGenres()
        {
            var genres = storeContext.Genres.OrderBy(a => a.Name).ToList();
            return View(genres);
        }

        public IActionResult ListAlbums(int id)
        {
            var albums = storeContext.Albums.Where(a => a.GenreID == id);
            ViewData["genreName"] = storeContext.Genres.Find(id).Name;
            return View(albums);
        }

        public IActionResult Details(int id)
        {
            var album = storeContext.Albums
                .Include(b => b.Genre)
                .Include(c => c.Artist)
                .Where(a => a.AlbumID == id).FirstOrDefault();
            return View(album);
        }
    }
}
