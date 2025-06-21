using System.ComponentModel.DataAnnotations;

namespace RecipeSharingPlatform.ViewModels.Recipe
{
    public class RecipeDeleteInputModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Author { get; set; } = null!;
        [Required]
        public string AuthorId { get; set; } = null!;

    }
}
