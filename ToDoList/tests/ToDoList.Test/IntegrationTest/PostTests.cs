namespace ToDoList.Test.IntegrationTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using Xunit;


public class CreateTests
{
    [Fact]
    public void Create_WhenItemIsCreated()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var newItem = new ToDoItemCreateRequestDto("new Jmeno", "new Popis", false);

        //Act
        var result = controller.Create(newItem);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(value);
        Assert.Equal(newItem.Description, value.Description);
        Assert.Equal(newItem.IsCompleted, value.IsCompleted);
        Assert.Equal(newItem.Name, value.Name);
    }

}
