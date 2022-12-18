using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_WebApi.Data;
using Products_WebApi.Dtos;
using Products_WebApi.Entities;

namespace Products_WebApi.Controllers;

[ApiController]
[Route("/api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly DBContext DBContext;

    public ProductsController( DBContext DBContext)
    {
        this.DBContext = DBContext;
    }
    
    
    [HttpGet(Name = "")]
    public async Task<ActionResult<List<ProductDto>>> Get()
    {
        var List = await DBContext.Products.Select(
            s => new ProductDto()
            {
                ProductId = s.ProductId,
                Designation = s.Designation,
                Price = s.Price
            }
        ).ToListAsync();

        if (List.Count < 0)
        {
            return NotFound();
        }
        else
        {
            return List;
        }
    }
}
