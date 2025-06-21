namespace RecipeSharingPlatform.ViewModels.Recipe
{
    public class RecipeDetailsViewModel : BaseRecipeViewModel
    {
        public string Instructions { get; set; } = null!;
        public string? Author { get; set; } = null!;
        public string CreatedOn { get; set; } = null!;
       
    }
}
