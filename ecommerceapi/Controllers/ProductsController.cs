using Core.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerceapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext db;
        public ProductsController(StoreContext _db)
        {
            db = _db;
        }
        [HttpGet]

        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await db.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await db.Products.FindAsync(id);
        }
    }
}
