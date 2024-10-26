namespace ToDoList.Test;

using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class GetTests
{

    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items = [];
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        ToDoItemsController.items.Add(toDoItem);

        var toDoItem2 = new ToDoItem
        {
            ToDoItemId = 2,
            Name = "Jmeno 2",
            Description = "Popis 2",
            IsCompleted = true

        };
        ToDoItemsController.items.Add(toDoItem2);

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


