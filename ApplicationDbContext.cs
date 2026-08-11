
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace MoviCatalogApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
            public DbSet<Movi> Movis { get; set; }
        public DbSet<Category> Categorys { get; set; } 
    
    }

}

