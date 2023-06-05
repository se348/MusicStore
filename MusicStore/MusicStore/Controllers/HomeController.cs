using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;
using System.Diagnostics;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StoreContext storeContext;

        public HomeController(ILogger<HomeController> logger, StoreContext storeContext)
        {
            _logger = logger;
            this.storeContext = storeContext;
        }

        public IActionResult Index()
        {
            var albums  = storeContext.Albums.OrderBy(a => Guid.NewGuid()).Take(6).ToList();
            return View(albums);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}