using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopApp.Data;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;

namespace RunGroopApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClub _clubService;

        public ClubController(IClub clubService)
        {
            _clubService = clubService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubService.GetAll();
            return View(clubs);
        }

        //[HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubService.GetIdAsync(id);
            return View(club);
        }
    }
}
