using task1_framework.Models;

namespace task1_framework.Services;

public interface IItemService
{
    List<Item> GetAll();
    Item GetById(Guid id);
    Item Create(CreateItemRequest request);
}