using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_WebApi.Data;
using Products_WebApi.Entities;

namespace Products_WebApi.Controllers;

[ApiController]
[Route("/categories")]
public class CategoriesController : ControllerBase
{
    private readonly DBContext DBContext;

    public CategoriesController( DBContext DBContext)
    {
        this.DBContext = DBContext;
    }
    
    
    [HttpGet(Name = "/all")]
    public async Task<ActionResult<List<Category>>> Get()
    {
        var List = await DBContext.Categories.Select(
            s => new Category()
            {
                CategoryID = s.CategoryID,
                Name = s.Name
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
    
    
    [HttpGet("CategoryById")]
    public async Task < ActionResult < Category >> GetCategoryById(int Id) {
        Category category = await DBContext.Categories.Select(s => new Category {
            CategoryID = s.CategoryID,
            Name = s.Name
        }).FirstOrDefaultAsync(s => s.CategoryID == Id);
        if (category == null) {
            return NotFound();
        } else {
            return category;
        }
    }
    
    [HttpPost("InsertCategory")]
    public async Task < HttpStatusCode > InsertCategory(Category p)
    {
        var entity = new Category() {
            CategoryID = p.CategoryID,
            Name = p.Name
        };
        DBContext.Categories.Add(entity);
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.Created;
    }

    [HttpPut("UpdateCategory")]
    public async Task < HttpStatusCode > UpdateUser(Category category) {
        var entity = await DBContext.Categories.FirstOrDefaultAsync(s => s.CategoryID == category.CategoryID);
        entity.CategoryID = category.CategoryID;
        entity.Name = category.Name;
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
    
    [HttpDelete("DeleteCategory/{Id}")]
    public async Task < HttpStatusCode > DeleteCategory(int Id) {
        var entity = new Category() {
            CategoryID = Id
        };
        DBContext.Categories.Attach(entity);
        DBContext.Categories.Remove(entity);
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
   
}
