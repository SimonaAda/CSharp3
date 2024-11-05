namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence;


public class UpdateTests
{

    [Fact]
    public void Update_ById_NoContent_WhenUpdated()
    {
        // Arrange
        var path = AppContext.BaseDirectory;
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var controller = new ToDoItemsController(context);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        controller.items.Add(toDoItem);

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true);

        // Act
        var result = controller.UpdateById(1, updatedItem);//(toDoItem.TodoItemId, updatedItem)

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Single(controller.items);
        Assert.Equal(updatedItem.Name, controller.items.First().Name);
        Assert.Equal(updatedItem.Description, controller.items.First());
        Assert.True(controller.items.First().IsCompleted);
    }

    [Fact]
    public void Update_ById_NotFound_WhenInvalid()
    {
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var controller = new ToDoItemsController(context);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        controller.items.Add(toDoItem);

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true);

        // Act
        var result = controller.UpdateById(99, updatedItem);//ivaldiID = 99,(invalidId, updatedID)
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);

    }
}


