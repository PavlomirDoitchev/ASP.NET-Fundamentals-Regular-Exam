using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeSharingPlatform.Data.Models;

namespace RecipeSharingPlatform.Data.Configurations
{
    using static GCommon.ValidationConstants.Recipe;
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> entity)
        {
            entity
                .HasKey(r => r.Id);

            entity
                .Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            entity
                .Property(r => r.Instructions)
                .IsRequired()
                .HasMaxLength(InstructionsMaxLength);

            entity
                .Property(r => r.ImageUrl)
                .IsRequired(false);

            entity
                .Property(r => r.AuthorId)
                .IsRequired();

            //entity.Property(r => r.CreatedOn)
            //    .IsRequired();

            entity
                .Property(r => r.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasQueryFilter(r => r.IsDeleted == false);

            //Relationships
            entity
                .HasOne(r => r.Author)
                .WithMany()
                .HasForeignKey(r => r.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasData(this.GenerateRecipeData());
        }
        private List<Recipe> GenerateRecipeData()
        {

            List<Recipe> seedRecipes = new List<Recipe>()
            {
                new Recipe
                {
                    Id = 1,
                    Title = "Classic Bruschetta",
                    Instructions = "Chop tomatoes, mix with basil and garlic, then spoon onto toasted bread.",
                    ImageUrl = "https://www.unicornsinthekitchen.com/wp-content/uploads/2024/04/Bruschetta-sq-500x500.jpg",
                    AuthorId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CreatedOn = DateTime.Now,
                    CategoryId = 1,
                    IsDeleted = false
                },
                new Recipe
                {
                    Id = 2,
                    Title = "Grilled Salmon",
                    Instructions = "Season salmon with herbs and grill skin-side down for 6–8 minutes.",
                    ImageUrl = "https://feelgoodfoodie.net/wp-content/uploads/2025/04/Grilled-Salmon-09-500x500.jpg",
                    AuthorId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CreatedOn = DateTime.Now,
                    CategoryId = 2,
                    IsDeleted = false
                },
                new Recipe
                {
                    Id = 3,
                    Title = "Chocolate Lava Cake",
                    Instructions = "Prepare cake mix, bake at high heat for 12 min. Serve warm with ice cream.",
                    ImageUrl = "https://www.cookingclassy.com/wp-content/uploads/2022/02/molten-lava-cake-17-500x500.jpg",
                    AuthorId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CreatedOn = DateTime.Now,
                    CategoryId = 3,
                    IsDeleted = false
                }
            };
            return seedRecipes;
        }
    }
}
