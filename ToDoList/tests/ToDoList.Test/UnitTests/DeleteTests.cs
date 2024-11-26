namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;

public class DeleteUnitTests
{
    [Fact]
    public async Task Delete_DeleteByIdValidItemId_ReturnsNoCntent()
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
        var result = await controller.DeleteByIdAsync(toDoItem.ToDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(toDoItem.ToDoItemId);
        await repositoryMock.Received(1).DeleteAsync(toDoItem);
    }

    [Fact]
    public async Task Delete_DeleteByInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);


        repositoryMock.ReadByIdAsync(99).Returns((ToDoItem)null);

        // Act

        var result = await controller.DeleteByIdAsync(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(99);
        await repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public async Task Delete_DeleteByIdUnhandledException_ReturnsInternalServiceError()
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

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Throws(new Exception());

        // Act

        var result = await controller.DeleteByIdAsync(1);

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<ToDoItem>());
    }
}

