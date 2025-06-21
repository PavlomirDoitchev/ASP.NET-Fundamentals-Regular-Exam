using System.ComponentModel.DataAnnotations;

namespace RecipeSharingPlatform.ViewModels.Recipe
{
    public class RecipeEditInputModel : RecipeCreateInputModel
    {
        public int Id { get; set; }
        [Required]
        public string AuthorId { get; set; } = null!;

    }
}
