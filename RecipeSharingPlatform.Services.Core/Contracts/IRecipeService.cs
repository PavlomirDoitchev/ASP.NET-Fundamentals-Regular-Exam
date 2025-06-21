using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Services.Core.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeIndexViewModel>> GetAllRecipesAsync(string? userId);
        Task<RecipeDetailsViewModel?> GetRecipeDetailsAsync(int? id, string? userId);
        Task<bool> CreateRecipeAsync(string userId, RecipeCreateInputModel inputModel);
        Task<RecipeEditInputModel?> GetRecipeForEditingAsync(int? id, string? userId);
        Task<bool> EditRecipeAsync(string userId, RecipeEditInputModel inputModel);
        Task<RecipeDeleteInputModel> GetRecipeForDeletingAsync(string userId, int? recipeId);
        Task<bool> DeleteRecipeAsync(string userId, RecipeDeleteInputModel inputModel);
        Task<IEnumerable<RecipeFavoriteViewModel>?> GetUserFavoriteRecipesAsync(string userId);
        Task<bool> AddRecipeToFavoritesAsync(string userId, int recipeId);
        Task<bool> RemoveRecipeFromFavoritesAsync(string userId, int recipeId);
    }
}
