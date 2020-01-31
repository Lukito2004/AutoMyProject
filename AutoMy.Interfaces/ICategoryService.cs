using AutoMy.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();
        CategoryDTO GetCategoryById(int id);
        CategoryDTO GetCategoryByName(string name);
        void AddCategory(CategoryDTO category);
        void RemoveCategoryById(int id);
        void UpdateCategory(CategoryDTO category);
        void FillCategoryDatabase();
    }
}
