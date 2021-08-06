using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Data;
using ShoppingOnline.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ShoppingOnline.Controllers
{
    public class ItemController : Controller
    {
        private readonly ShoppingOnlineContext _context;

        public ItemController(ShoppingOnlineContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
          return View(await _context.Items.ToListAsync());
        }
        public async Task<IActionResult> AddItem(int? id)
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");

                YourOrders buyBook = new YourOrders();
                var book = _context.Items.Where(x => x.Id == id).FirstOrDefault();
                buyBook.ItemName = book.ItemName;
                buyBook.Brand = book.Brand;
                buyBook.Size = book.Size;
                buyBook.Price = book.Price;
                buyBook.UserId = Convert.ToInt32(value);
                _context.Add(buyBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "YourOrders");
            }
            else
                return RedirectToAction("UserAuthenticate", "Authenticate");
        }

    }
}
