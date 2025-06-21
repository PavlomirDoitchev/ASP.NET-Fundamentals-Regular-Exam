using Microsoft.EntityFrameworkCore;
using RecipeSharingPlatform.Data;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Services.Core
{
    public class CategoryService : ICategoryService
    {
        private readonly RecipeSharingDbContext _dbContext;

        public CategoryService(RecipeSharingDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<RecipeCreateCategoryDropdownViewModel>> GetCategoryDropdownAsync()
        {
            IEnumerable<RecipeCreateCategoryDropdownViewModel> categoriesDropdown 
                = await this._dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new RecipeCreateCategoryDropdownViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();
            return categoriesDropdown;
        }
    }
}
