namespace ToDoList.Test.IntegrationTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence;
using ToDoList.Domain.DTOs;

public class GetTests
{

    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        // Act
        var result = controller.Read();
        var okResult = result as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
        Assert.NotNull(okResult);

        var item = okResult.Value as List<ToDoItemGetResponseDto>;

        Assert.NotNull(item);
        Assert.Equal(toDoItem.ToDoItemId, item.Count);

    }
}


