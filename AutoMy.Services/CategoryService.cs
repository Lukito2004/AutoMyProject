using AutoMapper;
using AutoMy.Database;
using AutoMy.DomainModels;
using AutoMy.Interfaces;
using AutoMy.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMy.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly AutoMyDBContext database;
        public CategoryService(IMapper _mapper, AutoMyDBContext _database)
        {
            mapper = _mapper;
            database = _database;
        }
        public void AddCategory(CategoryDTO category)
        {
            database.Categories.Add(mapper.Map<Category>(category));
            database.SaveChanges();
        }

        public void FillCategoryDatabase()
        {
            AddCategory(new CategoryDTO() { Name = "Economy" });
            AddCategory(new CategoryDTO() { Name = "Compact" });
            AddCategory(new CategoryDTO() { Name = "Compact Cabrio" });
            AddCategory(new CategoryDTO() { Name = "Intermediate Combi" });
            AddCategory(new CategoryDTO() { Name = "Luxury Car" });
            AddCategory(new CategoryDTO() { Name = "Luxury Convertible" });
            AddCategory(new CategoryDTO() { Name = "SUV" });
            AddCategory(new CategoryDTO() { Name = "Off Road 4x4" });
            AddCategory(new CategoryDTO() { Name = "Pick-up Car" });
        }

        public IEnumerable<CategoryDTO> GetAllCategories() => mapper.Map<IEnumerable<CategoryDTO>>(database.Categories);

        public CategoryDTO GetCategoryById(int id) => mapper.Map<CategoryDTO>(database.Categories.FirstOrDefault(o => o.Id == id));

        public CategoryDTO GetCategoryByName(string name) => mapper.Map<CategoryDTO>(database.Categories.FirstOrDefault(o => o.Name == name));

        public void RemoveCategoryById(int id)
        {
            database.Categories.Remove(database.Categories.FirstOrDefault(o => o.Id == id));
            database.SaveChanges();
        }

        public void UpdateCategory(CategoryDTO category)
        {
            database.Categories.Update(mapper.Map<Category>(category));
            database.SaveChanges();
        }
    }
}
