using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;


namespace RunGroopApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClub _clubService;
        private readonly IPhotoService _photoService;

        public ClubController(IClub clubService, IPhotoService photoService)
        {
            _clubService = clubService;
            _photoService = photoService;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                    }
                };
                _clubService.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clubVM);
        }
    }
}
