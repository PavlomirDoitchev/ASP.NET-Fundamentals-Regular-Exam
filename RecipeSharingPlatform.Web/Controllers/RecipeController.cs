using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Web.Controllers
{
    using static GCommon.ValidationConstants.Recipe;
    public class RecipeController : BaseController
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;
        public RecipeController(IRecipeService recipeService, ICategoryService categoryService)
        {
            this._recipeService = recipeService;
            this._categoryService = categoryService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = this.GetUserId();
                IEnumerable<RecipeIndexViewModel> allRecipes =
                    await this._recipeService.GetAllRecipesAsync(userId);
                return this.View(allRecipes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? recipeId)
        {
            try
            {
                string? userId = this.GetUserId();
                RecipeDetailsViewModel? recipeDetails =
                    await this._recipeService.GetRecipeDetailsAsync(recipeId, userId);
                if (recipeDetails == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }
                return this.View(recipeDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                RecipeCreateInputModel model = new RecipeCreateInputModel()
                {
                    CreatedOn = DateTime.UtcNow.ToString(CreatedOnFormat),
                    Categories = await this._categoryService.GetCategoryDropdownAsync()
                };
                return this.View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(RecipeCreateInputModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(inputModel);
                }
                bool result = await this._recipeService.CreateRecipeAsync(this.GetUserId()!, inputModel);
                if (result == false)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create the recipe. Please try again later!");
                    return this.View(inputModel);
                }
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? recipeId)
        {
            try
            {
                string userId = this.GetUserId()!;
                RecipeEditInputModel? recipeForEditing =
                    await this._recipeService.GetRecipeForEditingAsync(recipeId, userId);
                if (recipeForEditing == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }
                recipeForEditing.Categories = await this._categoryService.GetCategoryDropdownAsync();
                return this.View(recipeForEditing);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RecipeEditInputModel inputModel)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    return this.View(inputModel);
                }
                bool result = await this._recipeService.EditRecipeAsync(this.GetUserId()!, inputModel);
                if (result == false)
                {
                    ModelState.AddModelError(string.Empty, "Failed to edit the recipe. Please try again later!");
                    return this.View(inputModel);
                }
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? recipeId)
        {
            try
            {
                string userId = this.GetUserId()!;
                RecipeDeleteInputModel recipeForDeleting =
                    await this._recipeService.GetRecipeForDeletingAsync(userId, recipeId);
                if (recipeForDeleting == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }
                return this.View(recipeForDeleting);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(RecipeDeleteInputModel inputModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Invalid model state. Please check the input.");
                    return this.View(inputModel); 
                }
                bool deleteResult = await this._recipeService.DeleteRecipeAsync(this.GetUserId()!, inputModel);
                if (deleteResult == false)
                {
                    ModelState.AddModelError(string.Empty, "Failed to delete the recipe. Please try again later.");
                    return this.View(inputModel); 
                }
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            try
            {
                string userId = this.GetUserId()!;
                if (userId == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }
                IEnumerable<RecipeFavoriteViewModel>? favoriteRecipes
                    = await this._recipeService.GetUserFavoriteRecipesAsync(userId);
                if (favoriteRecipes == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }
                return this.View(favoriteRecipes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Save(int? id)
        {
            try
            {
                string userId = this.GetUserId()!;
                if (id == null)
                    return this.RedirectToAction(nameof(Index));

                bool favAddResult = await this._recipeService.AddRecipeToFavoritesAsync(userId, id.Value);
                if (favAddResult == false)
                {
                    return this.RedirectToAction(nameof(Index));
                }
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int? id)
        {
            try
            {
                string user = this.GetUserId()!;
                if (id == null)
                    return this.RedirectToAction(nameof(Index));

                bool favAddResult = await this._recipeService.RemoveRecipeFromFavoritesAsync(user, id.Value);
                if (favAddResult == false)
                {
                    return this.RedirectToAction(nameof(Index));
                }
                return this.RedirectToAction(nameof(Favorites));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
