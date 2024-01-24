using Homies.Contracts;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly HomiesDbContext homiesDbContext;
       
        public EventController(IEventService eventService, HomiesDbContext homiesDbContext)
        {
            this.eventService = eventService;
            this.homiesDbContext = homiesDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddViewModel eventModel = new AddViewModel()
            {
                Types = GetTypes()
            };

            return View(eventModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel eventModel)
        {
            //Invalid Type
            if (!GetTypes().Any(e => e.Id == eventModel.TypeId))
            {
                ModelState.AddModelError(nameof(eventModel.TypeId), "Type does not exist!");
            }

            if (!ModelState.IsValid)
            {
                return View(eventModel);
            }

            string currentUserId = GetUserId();

            var eventToAdd = new Event()
            {
                Name = eventModel.Name,
                Description = eventModel.Description,
                CreatedOn = DateTime.Now,
                TypeId = eventModel.TypeId,
                OrganiserId = currentUserId,
                Start = eventModel.Start,
                End = eventModel.End
            };

            await homiesDbContext.Events.AddAsync(eventToAdd);
            await homiesDbContext.SaveChangesAsync();

            return RedirectToAction("All", "Event");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await eventService.GetAllEventsAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var model = await eventService.GetMyJoinedEventsAsync(GetUserId());
            return View(model);
        }

        [HttpGet]   
        public async Task<IActionResult> Details(int id)
        {
            var model = await eventService.GetDetails(id);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }


        private IEnumerable<TypeViewModel> GetTypes()
           => homiesDbContext
               .Types
               .Select(t => new TypeViewModel()
               {
                   Id = t.Id,
                   Name = t.Name
               });


        private string GetUserId()
        {
            string id = string.Empty;
            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return id;
        }
         
    }
    
}
