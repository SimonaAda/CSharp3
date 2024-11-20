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
    public void Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        repositoryMock.ReadById(Arg.Any<int>()).Returns(toDoItem);

        //Act
        var result = controller.ReadById(toDoItem.ToDoItemId);
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
        repositoryMock.Received(Arg.Any<int>()).Read(); //proc tady kontrolujeme Read? Testujeme ReadById :)

    }

    [Fact]
    public void Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);


        var result = controller.ReadById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        Assert.NotNull(result);
        repositoryMock.Received(1).ReadById(99);
    }

    [Fact]
    public void Get_ReadByIdUnhandledException_ReturnsInternalServerError()

    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        repositoryMock.ReadById(Arg.Any<int>()).Throws(new Exception());

        // Act

        var result = controller.ReadById(1);

        // Assert
        Assert.IsType<ObjectResult>(result);
        repositoryMock.Received(1).ReadById(1);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);

    }
}

