using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Data;
using ShoppingOnline.Models;

namespace ShoppingOnline.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly ShoppingOnlineContext _context;

        public AuthenticateController(ShoppingOnlineContext context)
        {
            _context = context;
        }

        // GET: Authenticate
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authenticate.ToListAsync());
        }

        public IActionResult Erorr()
        {
            return View();
        }

        // POST: Authenticate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AdminAuthenticate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminAuthenticate([Bind("Id,LoginUser,Password")] Authenticate Authenticate)
        {
            HttpContext.Session.Clear();
            
            if ((Authenticate.LoginUser.Equals("Admin@hotmail.com") && Authenticate.Password.Equals("Password") ))
            {
                    HttpContext.Session.SetString("LoginUser", Authenticate.LoginUser.ToString());
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction(nameof(UserAuthenticate));
            }            
        }
        
        public IActionResult UserAuthenticate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAuthenticate([Bind("Id,LoginUser,Password")] Authenticate Authenticate)
        {
            HttpContext.Session.Clear();
            var AuthenticateData = _context.Authenticate.Where(m => m.LoginUser == Authenticate.LoginUser && m.Password == Authenticate.Password).FirstOrDefault();

            if ((Authenticate.LoginUser.Equals("Admin@hotmail.com") && Authenticate.Password.Equals("Password") )|| AuthenticateData != null)
            {
                if (AuthenticateData != null)
                {
                    HttpContext.Session.SetString("Id", AuthenticateData.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("LoginUser", Authenticate.LoginUser.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction(nameof(UserAuthenticate));
            }            
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoginUser,Password")] Authenticate Authenticate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Authenticate);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Id", Authenticate.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(Authenticate);
        }

        private bool AuthenticateExists(int id)
        {
            return _context.Authenticate.Any(e => e.Id == id);
        }
    }
}
