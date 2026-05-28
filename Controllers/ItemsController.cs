using Microsoft.AspNetCore.Mvc;
using task1_framework.Models;
using task1_framework.Services;

namespace task1_framework.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _service;

    public ItemsController(IItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] string? sortBy, [FromQuery] decimal? minPrice)
    {
        var items = _service.GetAll();

        if (minPrice.HasValue)
            items = items.Where(x => x.Price >= minPrice.Value).ToList();

        if (sortBy == "price")
            items = items.OrderBy(x => x.Price).ToList();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(_service.GetById(id));
    }

    [HttpPost]
    public IActionResult Create(CreateItemRequest request)
    {
        var item = _service.Create(request);
        return Created($"/api/items/{item.Id}", item);
    }
}