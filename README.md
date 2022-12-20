# Partie 2 : Products-WebApi
 
 + Cette partie est une application DotNet Core de type WebAPI qui permet gérer des produits appartenant à des catégories.
 
## La création du projet
  + Via la commande : dotnet new webapi -o Products-WebApi
  
  ![image](https://user-images.githubusercontent.com/52087288/206789980-82a9256b-2cdd-4456-beb7-c05ca79c92ca.png)

## La structure du projet

![image](https://user-images.githubusercontent.com/52087288/208744972-7f3cdaec-05f0-45d9-9245-eefaef023624.png)


## La création des classes principales
  + DBContext
    ```java
    public partial class DBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
      

        public DBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { var connectionString = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
     
    }
    ```
  + Product
    ```java
    public class Product
    {
    [Key]
    public int ProductId { get; set; }
    [Required,MinLength(6),MaxLength(25)]
    public string Designation { get; set; }
    [Required,Range(100,1000000)]
    public double Price { get; set; }
    
    public Category? Category { get; set; }

    }
    ```
  + Category
    ```java
     public class Category
    {
    [Key]
    public  int CategoryID { get;  set; }
    [Required,MinLength(6),MaxLength(25)]
    public  string Name { get;  set; }
    public virtual ICollection<Product> Products { get; set; }

    public Category(int categoryId, string name)
    {
        CategoryID = categoryId;
        Name = name;
    }

    public Category(){ }
    
    }
    ```
  + ProductController
    ```java
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

    ```
  + CategoryRestController
    ```java
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

    ```
  
  
## L'exécution du projet

### Documentation du Produit

![image](https://user-images.githubusercontent.com/52087288/208745152-1cd70f62-99c0-4a83-93ee-b3386a1b5d76.png)

### Documentation du Catégorie

![image](https://user-images.githubusercontent.com/52087288/208745269-bca860ae-41ee-4311-bb42-92f35a0897a3.png)
