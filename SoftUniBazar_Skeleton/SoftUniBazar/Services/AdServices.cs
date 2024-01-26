using SoftUniBazar.Contracts;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;

namespace SoftUniBazar.Services
{
    public class AdServices : IAdService
    {
        private readonly BazarDbContext dbContext;

        public AdServices(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAdAsync(AddViewModel model)
        {
            Ad ad = new Ad
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
            };

            await dbContext.Ads.AddAsync(ad);
            await dbContext.SaveChangesAsync();

        }


    }
}
