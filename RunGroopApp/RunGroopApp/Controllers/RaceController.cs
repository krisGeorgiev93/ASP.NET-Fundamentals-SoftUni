using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopApp.Data;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.Services;

namespace RunGroopApp.Controllers
{
	public class RaceController : Controller
	{
		private readonly IRace _raceService;
        public RaceController(IRace raceService)
        {
           _raceService = raceService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceService.GetAll();
            return View(races);
        }

        //[HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceService.GetIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            _raceService.Add(race);
            return RedirectToAction("Index");
        }
    }
}
