namespace ToDoList.Test;
<<<<<<< HEAD
using Xunit;
=======

>>>>>>> 5a2c7de695c25f1ea868f86514462e42c322ddf7
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

<<<<<<< HEAD

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

=======
public class DeleteTests
{
    [Fact]
    public void Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        controller.items.Add(toDoItem);

        // Act
        var result = controller.DeleteById(toDoItem.ToDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        controller.items.Add(toDoItem);

        // Act
        var invalidId = -1;
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
>>>>>>> 5a2c7de695c25f1ea868f86514462e42c322ddf7
