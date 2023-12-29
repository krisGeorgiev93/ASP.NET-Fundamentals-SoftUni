using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Interfaces;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashboardRespository;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboard dashboardService, IPhotoService photoService)
        {
            _dashboardRespository = dashboardService;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRespository.GetAllUserRaces();
            var userClubs = await _dashboardRespository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
    }
}
