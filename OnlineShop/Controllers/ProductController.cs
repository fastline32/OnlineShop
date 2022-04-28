using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using Core.Entities;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("onlineshop/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ShopContext _db;

        public ProductController(ShopContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var list = await _db.Products.ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _db.Products.FindAsync(id);
        }
    }
}
