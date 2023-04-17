using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPICore.Data;
using ProductsAPICore.Model;

namespace ProductsAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsAPICoreContext _context;

        public ProductsController(ProductsAPICoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        //{
        //    return await _context.Product.ToListAsync();
        //}
        public IActionResult GetProduct()
        {
           return Ok(_context.Product.ToList());
        }

        //[HttpPost]
        //[Route("add")]
        //public async Task<IActionResult> AddProduct(AddProductRequest addProductRequest)
        //{
        //    //having our own id
        //    var product = new Product()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = addProductRequest.Name,
        //        Price = addProductRequest.Price
        //    };

        //    await _context.Product.AddAsync(product);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}



        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        //[HttpPut("{id}")]
        //[Route("update")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductRequest updateProductRequest)
        {
            //update product by id must not have id in request body
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                product.Name = updateProductRequest.Name;
                product.Price = updateProductRequest.Price;

                await _context.SaveChangesAsync();
                return Ok(product);
            }
            return NotFound();
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
