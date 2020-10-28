using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WepApi.Model;

namespace WepApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductController(ShopContext context)
        {
            _context = context;

            //Explicit kontrol af at db er created

            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _context.Products.ToArrayAsync());
        }

        //Øhm skal denne slettes?
        [HttpGet("{id}")]
        public IActionResult GetProducts(int id)
        {
            var product = _context.Products.Find(id);
            return Ok(product);
        }


        //Give ny route når der hentes et product
        //Brug som argument når der skal append til route
        [HttpDelete("{Id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product==null)
            {
                return NotFound();
            }
            return Ok(product);
        }



    }
}
