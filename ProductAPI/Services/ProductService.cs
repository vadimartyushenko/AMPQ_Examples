using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Services;

public class ProductService : IProductService
{
    private readonly ProductDbContext _dbContext;
    
    public ProductService(ProductDbContext dbContext) => _dbContext = dbContext;

    public IEnumerable<Product> GetAllProducts()
    {
        return _dbContext.Products.ToList();
    }

    public Product GetProductById(int id)
    {
       return _dbContext.Products.FirstOrDefault(x => x.Id == id);
    }

    public Product AddProduct(Product product)
    {
        var result = _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return result.Entity;
    }

    public Product UpdateProduct(Product product)
    {
        var result = _dbContext.Products.Update(product);
        _dbContext.SaveChanges();
        return result.Entity;
    }

    public bool DeleteProductById(int id)
    {
        var filtered = _dbContext.Products.FirstOrDefault(x => x.Id == id);
        if (filtered != null) {
            var result = _dbContext.Remove(filtered);
            _dbContext.SaveChanges();
            return true;
        } else {
            return false;
        }
    }
}