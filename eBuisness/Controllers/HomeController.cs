using DAL;
using eBuisness.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eBuisness.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
      
        public IActionResult Index()
        {
          var teams  =_db.Teams.ToList();
            return View(teams);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}