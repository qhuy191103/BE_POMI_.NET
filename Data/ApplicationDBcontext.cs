using Microsoft.EntityFrameworkCore;
using restapi.Models;

namespace restapi.Data
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            
        }
        public DbSet<Product> Product {  get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }

        public DbSet<ProductDetail> ProductDetail { get; set; }

    }
}
