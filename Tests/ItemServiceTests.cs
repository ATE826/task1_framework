using task1_framework.Models;
using task1_framework.Services;
using Xunit;

namespace task1_framework.Tests;

public class ItemServiceTests
{
    [Fact]
    public void ShouldThrow_WhenNameEmpty()
    {
        var service = new ItemService();

        Assert.Throws<ArgumentException>(() =>
        {
            service.Create(new CreateItemRequest
            {
                Name = "",
                Price = 10,
                Quantity = 1
            });
        });
    }

    [Fact]
    public void ShouldThrow_WhenPriceNegative()
    {
        var service = new ItemService();

        Assert.Throws<ArgumentException>(() =>
        {
            service.Create(new CreateItemRequest
            {
                Name = "Book",
                Price = -1,
                Quantity = 1
            });
        });
    }
}