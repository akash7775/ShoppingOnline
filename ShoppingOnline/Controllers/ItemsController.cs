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
        public class ItemsController : Controller
        {
            private readonly ShoppingOnlineContext _context;

            public ItemsController(ShoppingOnlineContext context)
            {
                _context = context;
            }

            // GET: Items
            public async Task<IActionResult> Index()
            {
            if (HttpContext.Session.GetString("Id") != null || HttpContext.Session.GetString("LoginUser") != null)
            {
                var value = HttpContext.Session.GetString("Id");
                var name = HttpContext.Session.GetString("LoginUser");
                var Authenticate = _context.Authenticate.Where(x => x.Id == Convert.ToInt32(value)).FirstOrDefault();

                if (name == "Admin@hotmail.com")
                {
                    return View(await _context.Items.ToListAsync());
                    }
                    else
                    {
                    if (_context.Items.ToList().Count == 0)
                    {
                        return RedirectToAction(nameof(NoItem));
                    }
                    else
                    return RedirectToAction("Index", "Item");
                    }
            }
            else
                return RedirectToAction("UserAuthenticate", "Authenticate");
            }

            // GET: Items/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Items = await _context.Items
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Items == null)
                {
                    return NotFound();
                }

                return View(Items);
            }
        public IActionResult NoItem()
        {
            return View();
        }
        
        // GET: Items/Create
        public IActionResult Create()
            {
            return View();
            }

            // POST: Items/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,ItemName,Brand,Size,Price")] Items Items)
            {
                if (ModelState.IsValid)
                {
                    //Items.UserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    _context.Add(Items);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Items);
            }

            // GET: Items/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Items = await _context.Items.FindAsync(id);
                if (Items == null)
                {
                    return NotFound();
                }
                return View(Items);
            }

            // POST: Items/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,Brand,Size,Price")] Items Items)
            {
                if (id != Items.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Items);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ItemsExists(Items.Id))
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
                return View(Items);
            }

            // GET: Items/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Items = await _context.Items
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Items == null)
                {
                    return NotFound();
                }

                return View(Items);
            }

            // POST: Items/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Items = await _context.Items.FindAsync(id);
                _context.Items.Remove(Items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ItemsExists(int id)
            {
                return _context.Items.Any(e => e.Id == id);
            }
        }
    }
