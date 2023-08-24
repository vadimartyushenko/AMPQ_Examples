using ProductAPI.Models;

namespace ProductAPI.Services;

public interface IProductService
{
    public IEnumerable<Product> GetAllProducts();
    
    public Product GetProductById(int id);

    public Product AddProduct(Product product);
    
    public Product UpdateProduct(Product product);

    public bool DeleteProductById(int id);
}