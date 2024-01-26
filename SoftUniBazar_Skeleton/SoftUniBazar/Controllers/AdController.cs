using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using System.Security.Claims;
using System.Xml.Linq;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext dbContext;
             

        public AdController(BazarDbContext dbContext)
        {       
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddViewModel adModel = new AddViewModel()
            {
                Categories = GetCategories()
            };

            return View(adModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel adModel)
        {

            if (ModelState.IsValid == false)
            {
                return View(adModel);
            }

            string currentUserId = GetUserId();

            var adToAdd = new Ad()
            {
                Name = adModel.Name,
                Description = adModel.Description,
                CreatedOn = DateTime.Now,
                CategoryId = adModel.CategoryId,
                Price = adModel.Price,
                OwnerId = currentUserId,
                ImageUrl = adModel.ImageUrl
            };

            await dbContext.Ads.AddAsync(adToAdd);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("All", "Ad");
        }

        // Helper methods
        // Get Categories
        private IEnumerable<CategoryViewModel> GetCategories()
            => dbContext
                .Categories
                .Select(t => new CategoryViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                });

        // Get currently logged-in user's Id
        private string GetUserId()
           => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
