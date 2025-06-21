﻿using RecipeSharingPlatform.ViewModels.Recipe;

namespace RecipeSharingPlatform.Services.Core.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<RecipeCreateCategoryDropdownViewModel>> GetCategoryDropdownAsync();
    }
}
