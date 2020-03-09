using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eatrip.Data;
using eatrip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eatrip.Controllers
{
  [ApiController]
  [Route ("v1/products")]
  public class ProductController : ControllerBase
  {
    [HttpGet]
    [Route ("")]
    public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context) 
    {
      var products = await context.Products.Include(prod => prod.Category).ToListAsync();

      return products;
    }
      
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
    {
      var product = await context.Products.Include(prod => prod.Category)
        .AsNoTracking()
        .FirstOrDefaultAsync(prod => prod.Id == id);

      return product;
    } 

    [HttpGet]
    [Route("categories/{id:int}")]
    public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int categoryId)
    {
      var products = await context.Products.Include(prod => prod.Category)
        .AsNoTracking()
        .Where(prod => prod.CategoryId == categoryId)
        .ToListAsync();
      
      return products;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Product>> Post(
      [FromServices] DataContext context,
      [FromBody] Product model
    )
    {
      if (ModelState.IsValid)
      {
        context.Products.Add(model);
        await context.SaveChangesAsync();

        return model;
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
  }
}