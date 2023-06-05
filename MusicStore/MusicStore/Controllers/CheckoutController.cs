using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{

    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly StoreContext _context;

        public CheckoutController(StoreContext context)
        {
            _context = context;
        }

        
        // GET: Checkout/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Checkout/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,Username,Name,Address,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Username = HttpContext.User.Identity.Name;
                order.OrderDate = DateTime.Now;

                _context.Add(order);
                await _context.SaveChangesAsync();

                var cart = new ShoppingCart(HttpContext, _context);
                cart.CreateOrderItems(order);

                await _context.SaveChangesAsync();

                return RedirectToAction("Complete", new {id = order.OrderID});
            }
            return View(order);
        }


        public IActionResult Complete(int id)
        {
            ViewData["OrderNr."] = id;
            return View();
        }


        private bool OrderExists(int id)
        {
          return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
