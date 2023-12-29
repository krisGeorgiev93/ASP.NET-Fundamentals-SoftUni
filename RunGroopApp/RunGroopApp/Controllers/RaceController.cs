using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopApp.Data;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.Services;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Controllers
{
	public class RaceController : Controller
	{
		private readonly IRace _raceService;
        private readonly IPhotoService _photoService;

        public RaceController(IRace raceService, IPhotoService photoService)
        {
           _raceService = raceService;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                _raceService.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(raceVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _raceService.GetIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var raceDetails = await _raceService.GetIdAsync(id);

            if (raceDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(raceDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(raceDetails.Image);
            }

            _raceService.Delete(raceDetails);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceService.GetIdAsync(id);
            if (race == null)
            {
                return View("Error");
            }

            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = (int)race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory
            };
            return View(raceVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", raceVM);
            }

            var userRace = await _raceService.GetIdAsyncNoTracking(id);
            if (userRace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(raceVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address
                };

                _raceService.Update(race);

                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }

        }
    }
}
