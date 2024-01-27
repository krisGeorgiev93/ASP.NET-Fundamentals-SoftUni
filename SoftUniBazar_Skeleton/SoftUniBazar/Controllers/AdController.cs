using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (!GetCategories().Any(a=> a.Id == adModel.CategoryId))
            {
                ModelState.AddModelError(nameof(adModel.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
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

        public async Task<IActionResult> All()
        {
            var adsToDisplay = await dbContext.Ads.
                Select(a => new AdViewShortModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("dd/MM/yyyy H:mm"),
                    Category = a.Category.Name,
                    Price = a.Price,
                    Owner = a.Owner.UserName,
                    ImageUrl = a.ImageUrl
                })
                .ToListAsync();

            return View(adsToDisplay);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var adToAdd = await dbContext
                .Ads
                .FindAsync(id);

            if (adToAdd == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            var entry = new AdBuyer()
            {
                AdId = adToAdd.Id,
                BuyerId = currentUserId,
            };

            if (await dbContext.AdsBuyers.ContainsAsync(entry))
            {
                return RedirectToAction("Cart", "Ad");
            }

            await dbContext.AdsBuyers.AddAsync(entry);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Cart", "Ad");
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
