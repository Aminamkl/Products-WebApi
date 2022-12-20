using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products_WebApi.Entities;

[Table("categories")]
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

    public Category()
    {
    }
}