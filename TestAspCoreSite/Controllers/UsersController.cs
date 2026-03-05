using Microsoft.AspNetCore.Mvc;
using TestAspCoreSite.Models;

namespace TestAspCoreSite.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> _users =
    [
        new User { Id = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Role = "Admin", CreatedAt = new DateTime(2024, 1, 15) },
        new User { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com", Role = "Editor", CreatedAt = new DateTime(2024, 3, 22) },
        new User { Id = 3, FirstName = "Carol", LastName = "Williams", Email = "carol.williams@example.com", Role = "Viewer", CreatedAt = new DateTime(2024, 5, 10) },
        new User { Id = 4, FirstName = "David", LastName = "Brown", Email = "david.brown@example.com", Role = "Editor", CreatedAt = new DateTime(2024, 7, 3) },
        new User { Id = 5, FirstName = "Eve", LastName = "Davis", Email = "eve.davis@example.com", Role = "Viewer", CreatedAt = new DateTime(2025, 1, 20) },
    ];

    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns all users.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_users);
    }

    /// <summary>
    /// Returns a single user by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            return NotFound(new { message = $"User with ID {id} was not found." });

        return Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        user.Id = _users.Max(u => u.Id) + 1;
        user.CreatedAt = DateTime.UtcNow;
        _users.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            return NotFound(new { message = $"User with ID {id} was not found." });

        _users.Remove(user);
        return NoContent();
    }
}
