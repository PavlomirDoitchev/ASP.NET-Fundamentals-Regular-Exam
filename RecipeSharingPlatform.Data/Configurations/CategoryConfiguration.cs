using RecipeSharingPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeSharingPlatform.Data.Configurations
{
    using static GCommon.ValidationConstants.Category;
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity
                .HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            entity
                .HasData(GenerateCategoryData());
        }
        private List<Category> GenerateCategoryData() 
        {
            List<Category> seedCategories = new List<Category>()
            {
                new Category { Id = 1, Name = "Appetizer" },
                new Category { Id = 2, Name = "Main Dish" },
                new Category { Id = 3, Name = "Dessert" },
                new Category { Id = 4, Name = "Soup" },
                new Category { Id = 5, Name = "Salad" },
                new Category { Id = 6, Name = "Beverage" }
            };
            return seedCategories;
        }
    }
}
