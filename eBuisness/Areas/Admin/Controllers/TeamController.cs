using Core;
using DAL;
using eBuisness.Areas.Admin.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace eBuisness.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public TeamController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            var teams = _db.Teams.ToList();
            return View(teams);
        }
        //Create get
        public IActionResult Create()
        {
            return View();
        }
        //Create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeamVM teamVM)
        {
            if (!ModelState.IsValid)
            {
                return View(teamVM);
            }
            if (teamVM.Image == null)
            {
                return View();
            };
            if (!teamVM.Image.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Yalniz sekil foramtinda sece bilersinz");
                return View();
            }
            //if (teamVM.Image.Length / 1024 > 10)
            //{
            //    ModelState.AddModelError("Image", "Max lenght 10Mb");
            //    return View();
            //}
            string filename = Guid.NewGuid().ToString()+teamVM.Image.FileName;
            string wwwroot = _env.WebRootPath;
            string resultPath = Path.Combine(wwwroot, "assets", "img","team", filename);
            using (FileStream file = new FileStream(resultPath, FileMode.Create))
            {
                await teamVM.Image.CopyToAsync(file);
            }
            Team teams = new Team
            {
                Name = teamVM.Name,
                Position = teamVM.Position,
                Image = filename
            };
            _db.Teams.Add(teams);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {
            var team = _db.Teams.Find(id);
            if (team == null) return NotFound();
            return View(team);
        }
        //delete get
        public IActionResult Delete(int id)
        {
            var team = _db.Teams.Find(id);
            if (team == null) return NotFound();
            return View(team);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public IActionResult DeleteTeam(int id)
        {
            var team = _db.Teams.Find(id);
            if (team == null) return NotFound();
            string path = Path.Combine(_env.WebRootPath, "assets", "img","team", team.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _db.Teams.Remove(team);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
