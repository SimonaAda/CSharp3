namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
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
