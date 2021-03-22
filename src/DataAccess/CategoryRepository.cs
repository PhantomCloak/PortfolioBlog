using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CreateCategoryAsync(Category category)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(Category categoryToUpdate)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteCategoryAsync(int categoryId)
        {
            throw new System.NotImplementedException();
        }
    }
}