using Microsoft.AspNetCore.Mvc;
using TestAspCoreSite.Models;

namespace TestAspCoreSite.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> _products =
    [
        new Product { Id = 1, Name = "Wireless Keyboard", Description = "Compact Bluetooth keyboard with long battery life.", Category = "Electronics", Price = 49.99m, Stock = 120 },
        new Product { Id = 2, Name = "Ergonomic Mouse", Description = "Vertical ergonomic mouse to reduce wrist strain.", Category = "Electronics", Price = 34.99m, Stock = 85 },
        new Product { Id = 3, Name = "USB-C Hub", Description = "7-in-1 hub with HDMI, USB-A, SD card, and power delivery.", Category = "Electronics", Price = 29.99m, Stock = 200 },
        new Product { Id = 4, Name = "Standing Desk Mat", Description = "Anti-fatigue mat designed for standing desks.", Category = "Office", Price = 59.99m, Stock = 60 },
        new Product { Id = 5, Name = "Laptop Stand", Description = "Adjustable aluminium stand compatible with most laptops.", Category = "Office", Price = 39.99m, Stock = 95 },
        new Product { Id = 6, Name = "Noise-Cancelling Headphones", Description = "Over-ear headphones with active noise cancellation.", Category = "Electronics", Price = 149.99m, Stock = 40 },
    ];

    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns all products.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_products);
    }

    /// <summary>
    /// Returns a single product by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
            return NotFound(new { message = $"Product with ID {id} was not found." });

        return Ok(product);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing is null)
            return NotFound(new { message = $"Product with ID {id} was not found." });

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Category = product.Category;
        existing.Price = product.Price;
        existing.Stock = product.Stock;

        return Ok(existing);
    }

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
            return NotFound(new { message = $"Product with ID {id} was not found." });

        _products.Remove(product);
        return NoContent();
    }
}
