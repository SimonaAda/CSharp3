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
    public async Task Post_CreateValidRequest_ReturnsCreatedAtAction()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var newItem = new ToDoItemCreateRequestDto("new Jmeno", "new Popis", false, "new Kategorie");

        //Act
        var result = await controller.CreateAsync(newItem);

        // Assert
        var resultResult = Assert.IsType<CreatedAtActionResult>(result).Value;
        var item = resultResult as ToDoItemGetResponseDto;

        await repositoryMock.Received(1).CreateAsync(Arg.Any<ToDoItem>());
        Assert.NotNull(item);

        Assert.Equal(newItem.Description, item.Description);
        Assert.Equal(newItem.IsCompleted, item.IsCompleted);
        Assert.Equal(newItem.Name, item.Name);
        Assert.Equal(newItem.Category, item.Category);
    }

    [Fact]
    public async Task Post_CreateUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var newItem = new ToDoItemCreateRequestDto("new Jmeno", "new Popis", false, "new Kategorie");

        repositoryMock.When(r => r.CreateAsync(Arg.Any<ToDoItem>())).Throw (new Exception());

        // Act

        var result = await controller.CreateAsync(newItem);

        // Assert
        var resultResult = Assert.IsType<ObjectResult>(result);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }

}



