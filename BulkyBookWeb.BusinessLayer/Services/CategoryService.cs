using BulkyBook.Business.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Business.Services
{
    public class CategoryService : ICategoryService
    {

        //Dependency Injection ->Construtor Injection

        private readonly ApplicationDBContext _context;
        public CategoryService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> getAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
           
        }

        public async Task<Category?> getCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<Category?> createCategoryAsync(Category categoryToBeCreated)
        {
                _context.Categories.Add(categoryToBeCreated);
                await _context.SaveChangesAsync();
                return categoryToBeCreated;
        }

        public async Task deleteCategoryAsync(int id)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id);
            if (categoryToDelete != null) { 
                _context.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Category with {id} not found");
            }
        }

        public async Task updateCategoryAsync(Category categoryToBeUpdated)
        {
          _context.Categories.Update(categoryToBeUpdated);
            await _context.SaveChangesAsync();
        }
    }
}
