//using BulkyBookWeb.Migrations;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Business.Services.IServices;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;


namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService,ICategoryService categoryService, IWebHostEnvironment webHostEnvironment    )
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }
        private async Task<ProductViewModel> getProductViewModel()
        {
            var categories = await _categoryService.getAllCategoriesAsync();
            ProductViewModel productViewModel = new()
            {
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()
            };
            return productViewModel;
        }


        private async Task saveImageInProductFolder(Product prod, IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productUpdPath = Path.Combine("images", "Products");
                string finalPath = Path.Combine(wwwRootPath, productUpdPath);

                if (!Directory.Exists(finalPath))
                    Directory.CreateDirectory(finalPath);

                using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                prod.ImageUrl = Path.Combine(@"\", productUpdPath, fileName).Replace("\\", "/");
            }

        }

        public async Task<IActionResult> Index()
        { 
            return View();
        }

        public async Task<IActionResult> UpsertProduct(int? prodId)
        {
            var productVM = await getProductViewModel();
            if (prodId == null || prodId == 0)
            {
                return View(productVM);
            }

            //prod exists->make view to edit it
            var prodToEdit = await _productService.getProductByIdAsync(prodId.Value);
            productVM.Product = prodToEdit;
            return View(productVM); 

        }

        [HttpPost]
        [ActionName("UpsertProduct")]
        public async Task<IActionResult> UpsertProductPostAsync(ProductViewModel prodVM, IFormFile? file)
        {

            var productVM = await getProductViewModel();
            if (!ModelState.IsValid)
            {
                return View(productVM);
            }
            try
            {
                if (file != null)
                {
                    await saveImageInProductFolder(prodVM.Product, file);
                }
                if (prodVM.Product.Id == null || prodVM.Product.Id == 0)
                {
                    //create new product
                    Product prodToAdd = prodVM.Product;
                    await _productService.createProductAsync(prodToAdd);
                    TempData["success"] = "Product created successfully";
                }
                else
                {

                    //update existing product
                    Product updatedProduct = prodVM.Product;
                    await _productService.updateProductAsync(updatedProduct);
                    TempData["success"] = "Product updated successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to create Product";
                return View(productVM);
            }
            return RedirectToAction("Index"); 

        }


        #region  API CALLs


        public async Task <IActionResult> GetAllProducts()
        {
            var allProd = await _productService.getAllProductsAsync(true);
            return Json(new { data=allProd});
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid Id"
                });
            }

            try
            {

                var prodToDelete = await _productService.getProductByIdAsync(id.Value);
                if (prodToDelete == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Invalid Id"
                    });
                }
                if (!string.IsNullOrEmpty(prodToDelete.ImageUrl))
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, prodToDelete.ImageUrl.TrimStart('\\','/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                }
                await _productService.deleteProductAsync(id.Value);
                return Json(new
                {
                    success = true,
                    message = "Product Deleted Successfully"
                });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error deleting the product"
                });

            }

        }

        #endregion

    }
}
