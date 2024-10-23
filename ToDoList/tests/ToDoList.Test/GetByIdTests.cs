namespace ToDoList.Test;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using System.Formats.Asn1;
using NuGet.Frameworks;

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
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);

    }
}


