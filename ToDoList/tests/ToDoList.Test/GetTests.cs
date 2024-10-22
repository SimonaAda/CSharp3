namespace ToDoList.Test;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Diagnostics;
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

        // Act
        var result = controller.Read();
        var value = result.Value;
        var resultResult = result.Result;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        var firstItem = value.First();
        Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);

    }
}


