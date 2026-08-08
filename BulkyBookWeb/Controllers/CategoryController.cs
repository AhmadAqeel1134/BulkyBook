using BulkyBookWeb.Migrations;
using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Data;


namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _context;
        public CategoryController (ApplicationDBContext context )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //EF core 
            var categories = _context.Categories.ToList();
            return View("Index",categories);
        }
    }
}
