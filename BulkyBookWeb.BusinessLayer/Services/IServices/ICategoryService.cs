using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Business.Services.IServices
{
    public interface ICategoryService
    {
        Task<Category?> getCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> getAllCategoriesAsync();
        Task<Category?> createCategoryAsync(Category categoryToBeCreated);
        Task updateCategoryAsync(Category categoryToBeUpdated);
        Task deleteCategoryAsync(int id);

    }
}
