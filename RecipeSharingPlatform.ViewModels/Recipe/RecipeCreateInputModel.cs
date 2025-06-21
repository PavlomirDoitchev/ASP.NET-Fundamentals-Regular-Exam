namespace RecipeSharingPlatform.ViewModels.Recipe
{
    using System.ComponentModel.DataAnnotations;
    using static GCommon.ValidationConstants.Recipe;
    public class RecipeCreateInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;
        [Required]
        [MinLength(InstructionsMinLength)]
        [MaxLength(InstructionsMaxLength)]
        public string Instructions { get; set; } = null!;
        public string? ImageUrl { get; set; }
        [Required]
        [MinLength(CreatedOnLength)]
        [MaxLength(CreatedOnLength)]
        public string CreatedOn { get; set; } = null!;
        public int CategoryId { get; set; }
        public IEnumerable<RecipeCreateCategoryDropdownViewModel>? Categories { get; set; }


    }
}
