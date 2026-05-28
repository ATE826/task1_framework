using task1_framework.Models;

namespace task1_framework.Services;

public class ItemService : IItemService
{
    private readonly List<Item> _items = new();
    private readonly object _lock = new();

    public ItemService()
    {
        _items.Add(new Item
        {
            Id = Guid.NewGuid(),
            Name = "Book",
            Price = 100,
            Quantity = 5
        });
    }

    public List<Item> GetAll() => _items;

    public Item GetById(Guid id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null)
            throw new KeyNotFoundException("Item not found");

        return item;
    }

    public Item Create(CreateItemRequest request)
    {
        Validate(request);

        var item = new Item
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity
        };

        lock (_lock)
        {
            _items.Add(item);
        }

        return item;
    }

    private void Validate(CreateItemRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Name cannot be empty");

        if (request.Price < 0)
            throw new ArgumentException("Price cannot be negative");

        if (request.Quantity < 0)
            throw new ArgumentException("Quantity cannot be negative");

        if (request.Name.Length > 50)
            throw new ArgumentException("Name is too long");
    }
}