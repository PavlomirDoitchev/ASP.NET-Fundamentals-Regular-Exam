using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeSharingPlatform.Data;
using RecipeSharingPlatform.Data.Models;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;
using System.Globalization;

namespace RecipeSharingPlatform.Services.Core
{
    using static GCommon.ValidationConstants.Recipe;
    public class RecipeService : IRecipeService
    {
        private readonly RecipeSharingDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public RecipeService(RecipeSharingDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }



        public async Task<IEnumerable<RecipeIndexViewModel>> GetAllRecipesAsync(string? userId)
        {
            IEnumerable<RecipeIndexViewModel> allRecipes = await _dbContext
                .Recipes
                .Include(r => r.Category)
                .Include(r => r.UsersRecipes)
                .AsNoTracking()
                .Select(r => new RecipeIndexViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl,
                    Category = r.Category.Name,
                    SavedCount = r.UsersRecipes.Count,
                    IsAuthor = userId != null ? r.AuthorId.ToLower() == userId.ToLower() : false,
                    IsSaved = userId != null && r.UsersRecipes.Any(ur => ur.UserId.ToLower() == userId.ToLower())
                })
                .ToArrayAsync();
            return allRecipes;
        }
        public async Task<RecipeDetailsViewModel?> GetRecipeDetailsAsync(int? id, string? userId)
        {
            RecipeDetailsViewModel? recipeViewModel = null;
            if (id.HasValue)
            {
                Recipe? recipe = await this._dbContext
                    .Recipes
                    .Include(r => r.Category)
                    .Include(r => r.Author)
                    .Include(r => r.UsersRecipes)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(r => r.Id == id.Value);
                if (recipe != null)
                {
                    recipeViewModel = new RecipeDetailsViewModel
                    {
                        Id = recipe.Id,
                        Title = recipe.Title,
                        ImageUrl = recipe.ImageUrl,
                        Instructions = recipe.Instructions,
                        CreatedOn = recipe.CreatedOn.ToString(CreatedOnFormat),
                        Category = recipe.Category.Name,
                        Author = recipe.Author.UserName,
                        IsAuthor = userId != null ? recipe.AuthorId.ToLower() == userId.ToLower() : false,
                        IsSaved = userId != null ? recipe.UsersRecipes.Any(ur => ur.UserId.ToLower() == userId.ToLower()) : false

                    };
                }
            }
            return recipeViewModel;
        }
        public async Task<bool> CreateRecipeAsync(string userId, RecipeCreateInputModel inputModel)
        {
            bool result = false;
            IdentityUser? user = await this._userManager.FindByIdAsync(userId);
            Recipe? recipeReference = await this._dbContext
                .Recipes
                .FindAsync(inputModel.CategoryId);

            bool isPublishedOnDateValid = DateTime.TryParseExact(inputModel.CreatedOn, CreatedOnFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime publishedOnDate);

            if (user != null && recipeReference != null && isPublishedOnDateValid)
            {
                Recipe newRecipe = new Recipe
                {
                    Title = inputModel.Title,
                    Instructions = inputModel.Instructions,
                    ImageUrl = inputModel.ImageUrl,
                    CreatedOn = publishedOnDate,
                    CategoryId = inputModel.CategoryId,
                    AuthorId = user.Id
                };
                await this._dbContext.Recipes.AddAsync(newRecipe);
                int affectedRows = await this._dbContext.SaveChangesAsync();
                result = true;

            }
            return result;
        }
        public async Task<RecipeEditInputModel?> GetRecipeForEditingAsync(int? id, string? userId)
        {
            RecipeEditInputModel? recipeEditModel = null;
            if (id != null)
            {
                Recipe? recipe = await this._dbContext
                    .Recipes
                    .Include(r => r.Category)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(r => r.Id == id);
                if (recipe != null)
                {
                    recipeEditModel = new RecipeEditInputModel
                    {
                        Id = recipe.Id,
                        Title = recipe.Title,
                        Instructions = recipe.Instructions,
                        ImageUrl = recipe.ImageUrl,
                        CreatedOn = recipe.CreatedOn.ToString(CreatedOnFormat),
                        CategoryId = recipe.CategoryId,
                        AuthorId = recipe.AuthorId
                    };
                }
            }
            return recipeEditModel;
        }
        public async Task<bool> EditRecipeAsync(string userId, RecipeEditInputModel inputModel)
        {
            bool result = false;
            IdentityUser? user = await this._userManager.FindByIdAsync(userId);
            Recipe? recipe = await this._dbContext.Recipes.FindAsync(inputModel.Id);
            Category? category = await this._dbContext.Categories.FindAsync(inputModel.CategoryId);
            bool isPublishedOnDateValid = DateTime.TryParseExact(inputModel.CreatedOn, CreatedOnFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime publishedOnDate);

            if ((user != null) && (recipe != null) && (category != null && isPublishedOnDateValid)
                && recipe.AuthorId.ToLower() == userId.ToLower())
            {
                recipe.Title = inputModel.Title;
                recipe.Instructions = inputModel.Instructions;
                recipe.ImageUrl = inputModel.ImageUrl;
                recipe.CreatedOn = publishedOnDate;
                recipe.CategoryId = inputModel.CategoryId;
                await this._dbContext.SaveChangesAsync();
                result = true;
            }
            return result;
        }
        public async Task<RecipeDeleteInputModel> GetRecipeForDeletingAsync(string userId, int? recipeId)
        {
            RecipeDeleteInputModel? deleteModel = null;
            if (recipeId != null)
            {
                Recipe? recipeToDelete = await this._dbContext
                    .Recipes
                    .Include(d => d.Author)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(d => d.Id == recipeId);
                if ((recipeToDelete != null) && (recipeToDelete.AuthorId.ToLower() == userId.ToLower()))
                {
                    deleteModel = new RecipeDeleteInputModel
                    {
                        Id = recipeToDelete.Id,
                        Title = recipeToDelete.Title,
                        Author = recipeToDelete.Author.UserName,
                        AuthorId = recipeToDelete.AuthorId
                    };
                }
            }
            return deleteModel;
        }
        public async Task<bool> DeleteRecipeAsync(string userId, RecipeDeleteInputModel inputModel) 
        {
            bool operationResult = false;

            IdentityUser? user = await this._userManager.FindByIdAsync(userId);
            Recipe? recipeToDelete = await this._dbContext.Recipes.FindAsync(inputModel.Id);


            if ((user != null) && (recipeToDelete != null)
                && (recipeToDelete.AuthorId.ToLower() == userId.ToLower()))
            {
                recipeToDelete.IsDeleted = true;
                await this._dbContext.SaveChangesAsync();
                operationResult = true;
            }
            return operationResult;
        }
        public async Task<IEnumerable<RecipeFavoriteViewModel>?> GetUserFavoriteRecipesAsync(string userId)
        {
            IEnumerable<RecipeFavoriteViewModel>? favoriteRecipe = null;
            IdentityUser? user = await this._userManager.FindByIdAsync(userId);
            if (user != null)
            {
                favoriteRecipe = await this._dbContext
                    .UsersRecipes
                    .Include(ur => ur.Recipe) 
                    .ThenInclude(r => r.Category)
                    .Where(ur => ur.UserId.ToLower() == userId.ToLower())
                    .Select(ur => new RecipeFavoriteViewModel
                    {
                        Id = ur.Recipe.Id,
                        Title = ur.Recipe.Title,
                        ImageUrl = ur.Recipe.ImageUrl,
                        Category = ur.Recipe.Category.Name,
                    })
                    .ToArrayAsync();
            }
            return favoriteRecipe;
        }
        public async Task<bool> AddRecipeToFavoritesAsync(string userId, int recipeId) 
        {
            bool result = false;

            IdentityUser? user = await this._userManager.FindByIdAsync(userId);
            Recipe? recipeToAdd
                = await this._dbContext.Recipes.FindAsync(recipeId);

            if ((user != null) && (recipeToAdd != null)
                && (recipeToAdd.AuthorId.ToLower() != userId.ToLower()))
            {
                UserRecipe? userRecipe = await this._dbContext.UsersRecipes
                    .SingleOrDefaultAsync(ur => ur.UserId.ToLower() == userId.ToLower() && ur.RecipeId == recipeId);

                if (userRecipe == null) // This check is the correct one
                {
                    userRecipe = new UserRecipe
                    {
                        UserId = userId,
                        RecipeId = recipeId
                    };
                    await _dbContext.UsersRecipes.AddAsync(userRecipe);
                    await this._dbContext.SaveChangesAsync();
                    result = true;
                }
            }
            return result;
        }
        public async Task<bool> RemoveRecipeFromFavoritesAsync(string userId, int recipeId) 
        {
            bool operationResult = false;

            UserRecipe? userRecipe = await _dbContext.UsersRecipes
                .SingleOrDefaultAsync(ur => ur.UserId.ToLower() == userId.ToLower() && ur.RecipeId == recipeId);

            if (userRecipe != null)
            {
                _dbContext.UsersRecipes.Remove(userRecipe);
                await _dbContext.SaveChangesAsync();
                operationResult = true;
            }

            return operationResult;
        }
    }
}
