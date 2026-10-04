using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.Models;
namespace BulkyBook.Business.Services.IServices
{
    public interface IProductService
    {
        Task<Product?> getProductByIdAsync(int Id);
        Task<Product?> createProductAsync(Product prodToBeCreated);
        Task updateProductAsync(Product prodToBeUpdated);
        Task deleteProductAsync(int Id);

        Task<IEnumerable<Product>> getAllProductsAsync(bool includeCategory=false);
        Task<IEnumerable<Product>> getProductsByCategoryAsync(int categoryId);
    }
}
