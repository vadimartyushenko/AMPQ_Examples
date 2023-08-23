using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Data;

public class ProductDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ProductDbContext(IConfiguration configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    
    public DbSet<Product> Products { get; set; }
}