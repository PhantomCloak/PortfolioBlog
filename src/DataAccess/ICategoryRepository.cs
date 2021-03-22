using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace DataAccess
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<bool> CreateCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<bool> UpdateCategoryAsync(Category categoryToUpdate);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}