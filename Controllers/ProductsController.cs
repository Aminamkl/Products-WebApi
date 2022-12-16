using Microsoft.AspNetCore.Mvc;
using Products_WebApi.Models;

namespace Products_WebApi.Controllers;

[ApiController]
[Route("/api/v1/products")]
public class ProductsController : ControllerBase
{
   

    [HttpGet(Name = "")]
    public IEnumerable<Product> Get()
    {
        Product product1 = new Product();
        product1.ProductId = 1;
        product1.Price = 100;
         
        product1.CategoryName = "Category 1";

        return new List<Product> { product1, product1 };
    }
}
