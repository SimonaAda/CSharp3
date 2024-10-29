namespace ToDoList.Test;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;


public class DeleteTests
{
    [Fact]
    public void Delete_byId_NotContent_WhenValid()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var controller = new ToDoItemsController(context);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1
        };

        controller.items.Add(toDoItem);

        // Act
        var result = controller.DeleteById(1);
        var noContentResult = result as NoContentResult;

        // Assert
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.NotNull(noContentResult);

    }

    [Fact]
    public void Delete_byId_NotFound_WhenInvalid()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var controller = new ToDoItemsController(context);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1
        };
        controller.items.Add(toDoItem);

        // Act
        var result = controller.DeleteById(99);// var invalidId = 99; controller.DeleteById(invalidId)
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);

    }
}

