namespace ToDoList.Test;

using Microsoft.AspNetCore.Authentication; //zbytecne
using Microsoft.AspNetCore.Mvc.Diagnostics; //zbytecne
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Http.HttpResults; //zbytecne
using System.Runtime.CompilerServices; //zbytecne

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

        //musel jsem upravit aby mi to fungovalo, tobe to nedavalo kompilacni errory?
        var updatedItem = new ToDoItemUpdateRequestDto(Name: "Updated Jmeno", Description: "Update Popis", IsCompleted: true);

        // Act
        var result = controller.UpdateById(1, updatedItem);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Single(ToDoItemsController.items);
        Assert.Equal("Updated Jmeno", ToDoItemsController.items.First().Name); //"Updated Jmeno" neni dobre mit hardcoded, muzeme vyuzit updatedItem.Name
        Assert.True(ToDoItemsController.items.First().IsCompleted);
        //chtelo by to jeste test Description
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
        //musel jsem upravit aby mi to fungovalo, tobe to nedavalo kompilacni errory?
        var updatedItem = new ToDoItemUpdateRequestDto(Name: "Update Jmeno", Description: "Update Popis", IsCompleted: true);

        // Act
        var result = controller.UpdateById(99, updatedItem);
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);

    }
}


