using AltechTest.DTO;
using AltechTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AltechTest.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>
    {
        new Product
        (
            Guid.NewGuid(),
            "Basket",
            "A woven basket made from natural materials",
            10.99,
            "https://example.com/basket.jpg"
        ),
        new Product
        (
            Guid.NewGuid(),
            "Flip flop",
            "A pair of comfortable sandals for the summer",
            24.99,
            "https://example.com/flip-flops.jpg"
        ),
        new Product
        (
            Guid.NewGuid(),
            "Magic Wand",
            "A magical wand that can make your dreams come true",
            99.99,
            "https://example.com/magic-wand.jpg"
        ),
        new Product
        (
            Guid.NewGuid(),
            "Glitter pen",
            "A pen that sparkles and shines with every stroke",
            5.99,
            "https://example.com/glitter-pen.jpg"
        )
    };
    private readonly ILogger<ProductController> _logger;


    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Get a list with all products.
    /// </summary>
    [HttpGet, Authorize]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(products);
    }
    /// <summary>
    /// Get a specific product
    /// </summary>
    [HttpGet("{id}"), Authorize]
    public ActionResult<Product> GetProduct(Guid id)
    {
        var product = products.FirstOrDefault(e => e.Id == id);
        if (product != null) Ok(product);
        return NotFound();
    }
    /// <summary>
    /// Creates a new product
    /// </summary>
    [HttpPost, Authorize]
    public ActionResult<Guid> CreateProduct([FromBody] CreateProductDto dto)
    {
        var product = new Product(Guid.NewGuid(), dto.Name, dto.Description, dto.Price, dto.Image);
        products.Add(product);

        return Ok(product.Id);
    }
    /// <summary>
    /// Updates an existing product
    /// </summary>
    [HttpPut("{id}"), Authorize]
    public ActionResult UpdateProduct(Guid id, [FromBody] UpdateProductDto dto)
    {
        var product = products.FirstOrDefault(e => e.Id == id);
        if (product != null)
        {
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Image = dto.Image;
            return Ok();
        }
        return NotFound();
    }
    /// <summary>
    /// Deletes an existing product
    /// </summary>
    [HttpDelete("{id}"), Authorize]
    public ActionResult DeleteProduct(Guid id)
    {
        System.Diagnostics.Debug.WriteLine(id);

        var product = products.FirstOrDefault(e => e.Id == id);
        if (product != null)
        {
            products.Remove(product);
            return Ok();
        }
        return NotFound();
    }
}
