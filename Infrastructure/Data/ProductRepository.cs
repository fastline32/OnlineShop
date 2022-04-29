using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _db;

        public ProductRepository(ShopContext db)
        {
            _db = db;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products
                .Include(p=>p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetListOfProductsAsync()
        {
            return await _db.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _db.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _db.ProductTypes.ToListAsync();
        }
    }
}