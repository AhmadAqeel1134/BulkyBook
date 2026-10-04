using System.Diagnostics;
using System.Runtime.CompilerServices;
using BulkyBook.Business.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
            
         

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService=productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.getAllProductsAsync(true);
            
            return View(products);
        }

        public async Task<IActionResult> ProductDetails(int? productId)
        {
            if (productId == 0 || productId == null)
                return RedirectToAction("Index");
            var prodToOpen = await _productService.getProductByIdAsync(productId.Value,true);
            return View(prodToOpen);
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
