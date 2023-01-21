using Microsoft.EntityFrameworkCore;

namespace API_Task.Models
{
    public class ApiDbContext : DbContext 
    {
        public DbSet<Product> Products { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
