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
            //EF core
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

    }
}
