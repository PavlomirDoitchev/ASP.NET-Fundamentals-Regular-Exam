namespace RecipeSharingPlatform.ViewModels.Recipe
{
    public class RecipeFavoriteViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
    }
}
