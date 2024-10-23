namespace ToDoList.Test;

using Microsoft.AspNetCore.Authentication; //zbytecne
using Microsoft.AspNetCore.Mvc.Diagnostics; //zbytecne
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using System.Formats.Asn1; //zbytecne
using NuGet.Frameworks; //zbytecne

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
            Name = "Jmeno", //slo by to parametrizovat
            Description = "Popis", //slo by to parametrizovat
            IsCompleted = false //slo by to parametrizovat
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
        var toDoItem = new ToDoItem //nema vliv na test, zbytecne to prodluzuje delku kodu
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        ToDoItemsController.items.Add(toDoItem); //nema vliv na test, zbytecne to prodluzuje delku kodu

        // Act
        var result = controller.ReadById(99);
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);

    }
}


