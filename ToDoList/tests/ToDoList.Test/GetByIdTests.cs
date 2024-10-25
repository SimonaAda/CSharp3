namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;


public class GetByIdTests
{

    [Fact]
    public void Get_ById_OkResult_WhenValid()
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

        // Act
        var result = controller.ReadById(1);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
        Assert.NotNull(okResult);
        var item = okResult.Value as ToDoItemGetResponseDto;
        Assert.NotNull(item);
        Assert.Equal(1, item.Id);
        Assert.Equal("Jmeno", item.Name);
        Assert.Equal("Popis", item.Description);
        Assert.False(item.IsCompleted);

    }

    [Fact]
    public void Get_ById_NotFound_WhenInvalid()
    {
        //Arrange
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

        // Act
        var result = controller.ReadById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        Assert.NotNull(result);

    }
}


