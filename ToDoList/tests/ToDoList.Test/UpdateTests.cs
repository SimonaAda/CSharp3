namespace ToDoList.Test;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

public class UpdateTests
{

    [Fact]
    public void Update_ById_NoContent_WhenUpdated()
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

        var updatedItem = new ToDoItemUpdateRequestDto
        {
            Name = "Updated Jmeno",
            Description = "Updated Popis",
            IsCompleted = true
        };

        // Act
        var result = controller.UpdateById(1, updatedItem);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Single(ToDoItemsController.items);
        Assert.Equal("Updated Jmeno", ToDoItemsController.items.First().Name);
        Assert.True(ToDoItemsController.items.First().IsCompleted);
    }

    [Fact]
    public void Update_ById_NotFound_WhenInvalid()
    {
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

        var updatedItem = new ToDoItemUpdateRequestDto
        {
            Name = "Updated Jmeno",
            Description = "Updated Popis",
            IsCompleted = true
        };

        // Act
        var result = controller.UpdateById(99, updatedItem);
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);

    }
}


