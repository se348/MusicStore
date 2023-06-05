using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Areas.Admin.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly StoreContext storeContext;
        public ShoppingCartController(StoreContext storeContext) 
        { 
            this.storeContext = storeContext;
        }
        public IActionResult Index()
        {
            var shoppingCart = new ShoppingCart(HttpContext, storeContext);
            var cartItems = shoppingCart.GetCartItems();
            return View(cartItems);
        }

        public IActionResult AddToCart(int id) 
        {
            var album = storeContext.Albums.Find(id);
            var shoppingCart = new ShoppingCart(HttpContext, storeContext);
            shoppingCart.AddToCart(album);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cartItem = storeContext.Albums.Find(id);
            var shoppingCart = new ShoppingCart(HttpContext, storeContext);
            shoppingCart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult IncreaseAmountByOne(int id)
        {
            var shoppingCart = new ShoppingCart(HttpContext, storeContext);
            shoppingCart.ChangeCount(id, 1);
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseAmountByOne(int id)
        {
            var shoppingCart = new ShoppingCart(HttpContext, storeContext);
            shoppingCart.ChangeCount(id, -1);
            return RedirectToAction("Index");
        }


    }
}
