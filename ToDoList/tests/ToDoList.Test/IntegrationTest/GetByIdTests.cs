namespace ToDoList.Test.IntegrationTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;



public class GetByIdTests
{

    [Fact]
    public void Get_ById_OkResult_WhenValid()
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

        //Act
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
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        // Act
        var result = controller.ReadById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        Assert.NotNull(result);

    }
}


