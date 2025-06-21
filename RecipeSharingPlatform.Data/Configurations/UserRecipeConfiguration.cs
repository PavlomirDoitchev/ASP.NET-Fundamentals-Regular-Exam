using RecipeSharingPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeSharingPlatform.Data.Configurations
{
    public class UserRecipeConfiguration : IEntityTypeConfiguration<UserRecipe>
    {
        public void Configure(EntityTypeBuilder<UserRecipe> entity)
        {
            entity
                .HasKey(ur => new { ur.UserId, ur.RecipeId });

            entity
                .HasQueryFilter(ur => ur.Recipe.IsDeleted == false);

            entity
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(ur => ur.Recipe)
                .WithMany(r => r.UsersRecipes)
                .HasForeignKey(ur => ur.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
