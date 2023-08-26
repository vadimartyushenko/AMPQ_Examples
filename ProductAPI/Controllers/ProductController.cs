using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.QueueProducer;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IRabbitMQProducer _mqProducer;
    public ProductController(IProductService productService, IRabbitMQProducer producer)
    {
        _productService = productService;
        _mqProducer = producer;
    }

    [HttpGet("products")]
    public IEnumerable<Product> Products()
    {
        return _productService.GetAllProducts();
    }

    [HttpGet("getbyid")]
    public Product GetProductById(int id)
    {
        return _productService.GetProductById(id);
    }

    [HttpPost("add")]
    public Product AddProduct(Product product)
    {
        var result = _productService.AddProduct(product);
        
        //inserts product details inside the RabbitMQ queue, and later the consumer will get that data.
        _mqProducer.SendMessage(result);
        return result;
    }

    [HttpPut("update")]
    public Product UpdateProduct(Product product)
    {
        return _productService.UpdateProduct(product);
    }
    
    [HttpDelete("delete")]
    public bool DeleteProduct(int id)
    {
        return _productService.DeleteProductById(id);
    }
}