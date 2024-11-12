namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

public class UpdateTests
{

    [Fact]
    public void Update_ById_NoContent_WhenUpdated()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);


        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true);

        // Act
        var result = controller.UpdateById(toDoItem.ToDoItemId, updatedItem);//(toDoItem.TodoItemId, updatedItem)

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Update_ById_NotFound_WhenInvalid()
    {
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true);

        // Act
        var result = controller.UpdateById(99, updatedItem);//ivaldiID = 99,(invalidId, updatedID)
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);

    }
}


