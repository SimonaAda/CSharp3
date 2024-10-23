namespace ToDoList.Test;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;


public class DeleteTests
{
    [Fact]
    public void Delete_byId_NotContent_WhenValid()
    {
        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items = [];
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1
        };

        ToDoItemsController.items.Add(toDoItem);

        // Act
        var result = controller.DeleteById(1);
        var noContentResult = result as NoContentResult;

        // Assert
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.NotNull(noContentResult);
        Assert.Empty(ToDoItemsController.items);

    }

    [Fact]
    public void Delete_byId_NotFound_WhenInvalid()
    {
        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items = [];
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1
        };

        // Act
        var result = controller.DeleteById(99);
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);
        Assert.Single(ToDoItemsController.items);

    }
}

