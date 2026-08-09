//using BulkyBookWeb.Migrations;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Business.Services.IServices;
using System.Threading.Tasks;


namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            //EF core 
            var categories = await  _categoryService.getAllCategoriesAsync();
            return View("Index", categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreateCategory")]
        public async Task<IActionResult> CreateCategoryPostAction(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
              await   _categoryService.createCategoryAsync(category);
                TempData["success"] = "Category created successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to created category";
                return View();
            }

            return RedirectToAction("Index");
        }

        public async Task <IActionResult> Update(int? categoryId)
        {

            if (categoryId == null || categoryId == 0)
            {
                return NotFound();
            }

            var categoryToUpdate = await _categoryService.getCategoryByIdAsync(categoryId.Value);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }
            return View(categoryToUpdate);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateCategoryInfoPostAction(Category updatedCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
          
            try
            {
                await _categoryService.updateCategoryAsync(updatedCategory);
                TempData["success"] = "Category updated successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to update category";
                return View();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
            {
                return NotFound();
            }

            var categoryToDelete = await _categoryService.getCategoryByIdAsync(categoryId.Value);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            return View(categoryToDelete);

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCategoryEndpoint(int categoryId)
        {   
            try
            {
                await _categoryService.deleteCategoryAsync(categoryId);
                TempData["success"] = "Category Deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error deleting category";
                return View();
            }
            return RedirectToAction("Index");
        }

    }

}
