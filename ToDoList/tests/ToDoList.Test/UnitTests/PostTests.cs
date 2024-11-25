namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Http;

public class PostUnitTests
{
    [Fact]
    public void Post_CreateValidRequest_ReturnsCreatedAtAction()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var newItem = new ToDoItemCreateRequestDto("new Jmeno", "new Popis", false, "new Kategorie");

        //Act
        var result = controller.Create(newItem);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<CreatedAtActionResult>(result.Result);
        repositoryMock.Received(1).Create(Arg.Any<ToDoItem>());
        Assert.NotNull(value);

        Assert.Equal(newItem.Description, value.Description);
        Assert.Equal(newItem.IsCompleted, value.IsCompleted);
        Assert.Equal(newItem.Name, value.Name);
        Assert.Equal(newItem.Category, value.Category);
    }

    public void Post_CreateUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var newItem = new ToDoItemCreateRequestDto("new Jmeno", "new Popis", false, "new Kategorie");

        repositoryMock.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

        // Act

        var result = controller.Create(newItem);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }

}



