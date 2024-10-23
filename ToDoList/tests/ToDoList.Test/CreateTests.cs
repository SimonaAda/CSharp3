namespace ToDoList.Test;

using System.Collections.Generic; //zbytecne
using System.Security.Cryptography.X509Certificates; //zbytecne
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;


public class CreateTests
{
    [Fact]
    public void Create_WhenItemIsCreated()
    {
        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items = [];

        //musel jsem upravit aby mi to fungovalo, tobe to nedavalo kompilacni errory?
        var newItem = new ToDoItemCreateRequestDto("new Jmeno", "new Popis", false);

        //Act
        var result = controller.Create(newItem);
        var createdAtActionResult = result as CreatedAtActionResult;

        // Assert
        Assert.IsType<CreatedAtActionResult>(createdAtActionResult);
        Assert.NotNull(createdAtActionResult);
        Assert.Equal(201, createdAtActionResult.StatusCode);
        Assert.Single(ToDoItemsController.items);
        Assert.Equal("new Jmeno", ToDoItemsController.items[0].Name);
    }

}
