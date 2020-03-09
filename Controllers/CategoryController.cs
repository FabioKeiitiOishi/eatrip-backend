using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eatrip.Data;
using eatrip.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace eatrip.Controllers
{
  [ApiController]
  [Route("v1/categories")]
  public class CategoryController : ControllerBase
  {
    [HttpGet]
    [Route ("")]
    public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context) => 
      await context.Categories.ToListAsync();

    [HttpPost]
    [Route ("")]
    public async Task<ActionResult<Category>> Post(
      [FromServices] DataContext context,
      [FromBody] Category model
    )
    {
      if (ModelState.IsValid)
      {
        context.Categories.Add(model);
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