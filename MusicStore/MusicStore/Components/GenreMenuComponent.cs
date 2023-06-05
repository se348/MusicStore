using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Components
{

    [ViewComponent(Name = "GenreMenu")]
    public class GenreMenuComponent: ViewComponent
    {
        private readonly StoreContext _storeContext;

        public GenreMenuComponent(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres =  _storeContext.Genres.Take(6).ToList();
            return View(genres);
        }
    }
}
