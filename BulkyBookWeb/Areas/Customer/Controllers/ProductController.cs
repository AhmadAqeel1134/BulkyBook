//using BulkyBookWeb.Migrations;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Business.Services.IServices;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;


namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;   
        }

        public async Task<IActionResult> Index()
        { 
            return View();
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreateProduct")]
        public async Task<IActionResult> CreateProductPostAsync(Product prod)
        {
          if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _productService.createProductAsync(prod);
                TempData["success"] = "Product created successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to create Product";
                return View();
            }
            return RedirectToAction("Index"); 

        }

        public async Task<IActionResult> UpdateProduct(int? prodId)
        {

            if (prodId ==0 || prodId==null)
            {
                return NotFound();
            }

            var updProduct = await _productService.getProductByIdAsync(prodId.Value);
            if(updProduct == null)
                return NotFound();
            return View(updProduct);

            
        }

        [HttpPost]
        [ActionName("UpdateProduct")]
        public async Task<IActionResult> UpdateProductPostAsync(Product updProd)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
               await _productService.updateProductAsync(updProd);
               TempData["success"] = "Product updated successfully";
            }catch(Exception ex)
            {
                TempData["error"] = "Failed to update Product";
                return View();
            }
            return RedirectToAction("Index");   
        }

        public async Task<IActionResult> DeleteProduct(int? prodId)
        {
            if (prodId == 0 || prodId == null)
                return NotFound();
            var prodToDelete = await _productService.getProductByIdAsync(prodId.Value);
            if (prodToDelete == null)
                return NotFound();
            return View(prodToDelete);
        }

        [HttpPost]
        [ActionName("DeleteProduct")]
        public async Task <IActionResult> DeleteProductPostAsync(int ? id)
        {
            try
            {
                await _productService.deleteProductAsync(id.Value);
                TempData["success"] = "Product Deleted successfully";
            }
            catch(Exception ex)
            {
                TempData["error"] = "Failed to delete Product";
                return View();
            }
            return RedirectToAction("Index");
        }



        #region  API CALLs


        public async Task <IActionResult> GetAllProducts()
        {
            var allProd = await _productService.getAllProductsAsync(true);
            return Json(new { data=allProd});
        }


        #endregion

    }
}
