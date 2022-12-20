using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_WebApi.Data;
using Products_WebApi.Dtos;
using Products_WebApi.Entities;

namespace Products_WebApi.Controllers;

[ApiController]
[Route("/products")]
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
                Price = s.Price,
                CategoryID = s.Category.CategoryID
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
    
    [HttpGet("ProductById")]
    public async Task < ActionResult < ProductDto >> GetProductById(int Id) {
        ProductDto product = await DBContext.Products.Select(s => new ProductDto {
            ProductId = s.ProductId,
            Designation = s.Designation,
            Price = s.Price,
            CategoryID = s.Category.CategoryID
        }).FirstOrDefaultAsync(s => s.ProductId == Id);
        if (product == null) {
            return NotFound();
        } else {
            return product;
        }
    }
    
    [HttpPost("InsertProduct")]
    public async Task < HttpStatusCode > InsertProduct(ProductDto p)
    {
        Category category = new Category(2, "B");
        var entity = new Product() {
            ProductId = p.ProductId,
            Designation = p.Designation,
            Price = p.Price,
            Category = category
        };
        DBContext.Products.Add(entity);
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.Created;
    }

    [HttpPut("UpdateProduct")]
    public async Task < HttpStatusCode > UpdateUser(ProductDto product) {
        var entity = await DBContext.Products.FirstOrDefaultAsync(s => s.ProductId == product.ProductId);
        entity.ProductId = product.ProductId;
        entity.Designation = product.Designation;
        entity.Price = product.ProductId;
        //entity.Category = product.Category;
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
    
    [HttpDelete("DeleteProduct/{Id}")]
    public async Task < HttpStatusCode > DeleteUser(int Id) {
        var entity = new Product() {
            ProductId = Id
        };
        DBContext.Products.Attach(entity);
        DBContext.Products.Remove(entity);
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
   
}
