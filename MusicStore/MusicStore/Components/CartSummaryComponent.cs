using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Components
{

    [ViewComponent(Name ="CartSummary")]
    public class CartSummaryComponent: ViewComponent
    {
        private readonly StoreContext _storeContext;

        public CartSummaryComponent(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = new ShoppingCart(HttpContext, _storeContext);
            ViewData["CartCount"] = cart.GetCount();
            return View();
        }
    }
}
