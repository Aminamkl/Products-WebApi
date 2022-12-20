using Microsoft.EntityFrameworkCore;
using Products_WebApi.Entities;

namespace Products_WebApi.Data;


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