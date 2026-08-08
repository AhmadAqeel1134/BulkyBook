//using BulkyBookWeb.Migrations;
using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Data;
using BulkyBookWeb.Models;


namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _context;
        public CategoryController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //EF core 
            var categories = _context.Categories.ToList();
            return View("Index", categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreateCategory")]
        public IActionResult CreateCategoryPostAction(Category category)
        {
            //category will get its value because of asp-for which binded form input fields with model fields

            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Categories.Add(category);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //asp -validation-summary =ModelOnly will read only this error sss
                ModelState.AddModelError("", "Duplicate Name or Display Order");
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int? categoryId)
        {

            if (categoryId == null || categoryId == 0)
            {
                return NotFound();
            }

            var categoryToUpdate = _context.Categories.Find(categoryId);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }
            return View(categoryToUpdate);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult UpdateCategoryInfoPostAction(Category updatedCategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Categories.Update(updatedCategory);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error while updating the category information");
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
            {
                return NotFound();
            }

            var categoryToDelete = _context.Categories.Find(categoryId);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            return View(categoryToDelete);

        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCategoryEndpoint(int categoryId)
        {
            var categoryToDelete = _context.Categories.Find(categoryId);
            if (categoryToDelete == null)   
            {
                return NotFound();
            }
            _context.Categories.Remove(categoryToDelete);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

    }

}
