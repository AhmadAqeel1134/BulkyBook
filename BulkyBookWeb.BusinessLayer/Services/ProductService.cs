using BulkyBook.Business.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDBContext _context;

        public ProductService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Product?> getProductByIdAsync(int Id)
        {
            return await _context.Products.FindAsync(Id);
        }

        public async Task<Product?> createProductAsync(Product prodToBeCreated)
        {
            _context.Products.Add(prodToBeCreated);
            await _context.SaveChangesAsync();
            return prodToBeCreated;

        }

        public async Task updateProductAsync(Product prodToBeUpdated)
        {
            _context.Products.Update(prodToBeUpdated);
            await _context.SaveChangesAsync();

        }

        public async Task deleteProductAsync(int Id)
        {
            var prodToDelete = await _context.Products.FindAsync(Id);
            if (prodToDelete != null)
            {
                _context.Products.Remove(prodToDelete);
               await _context.SaveChangesAsync();
            }
            else
            {
               throw new KeyNotFoundException($"Product with {Id} not found");
            }

        }

        public async Task<IEnumerable<Product>> getAllProductsAsync(bool includeCategory=false)
        {
           if(includeCategory)
            return await _context.Products.Include(i=>i.Category).ToListAsync();
           else
                return await _context.Products.ToListAsync();
        }

        public async Task <IEnumerable<Product>> getProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products.Where(i=>i.CategoryId == categoryId).Include(i=>i.Category).ToListAsync();
        }

    }
}

