namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;

public class ReadByIdUnitTests
{
    [Fact]
    public async Task Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false,
            Category = "Kategorie"
        };

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(toDoItem);

        //Act
        var result = await controller.ReadByIdAsync(toDoItem.ToDoItemId);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(result);
        var item = okResult.Value as ToDoItemGetResponseDto;
        Assert.NotNull(item);
        Assert.Equal(toDoItem.ToDoItemId, item.Id);
        Assert.Equal(toDoItem.Name, item.Name);
        Assert.Equal(toDoItem.Description, item.Description);
        Assert.False(item.IsCompleted);
        Assert.Equal(toDoItem.Category, item.Category);
        await repositoryMock.Received(1).ReadByIdAsync(toDoItem.ToDoItemId);

    }

    [Fact]
    public async Task Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);


        var result = await controller.ReadByIdAsync(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        Assert.NotNull(result);
        await repositoryMock.Received(1).ReadByIdAsync(99);
    }

    [Fact]
    public async Task Get_ReadByIdUnhandledException_ReturnsInternalServerError()

    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Throws(new Exception());

        // Act

        var result = await controller.ReadByIdAsync(1);

        // Assert
        Assert.IsType<ObjectResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(1);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);

    }
}

