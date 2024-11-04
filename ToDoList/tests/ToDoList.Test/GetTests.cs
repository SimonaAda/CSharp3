namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetTests
{

    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        // Act
        var result = controller.Read();
        var okResult = result as OkObjectResult;

        // Assert

        Assert.IsType<OkObjectResult>(okResult);
        Assert.NotNull(okResult);

        var items = okResult.Value as List<ToDoItemGetResponseDto>;
        Assert.NotNull(items);
        Assert.Equal(2, items.Count);

    }
}


