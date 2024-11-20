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
    public void Delete_DeleteByIdValidItemId_ReturnsNoCntent()
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
        //mohli bychom to nastavit aby to reagovalo pouze pro ReadById(toDoItem.ToDoItemId), ale tohle taky muze byt :)
        repositoryMock.ReadById(Arg.Any<int>()).Returns(toDoItem);

        //Act
        var result = controller.DeleteById(toDoItem.ToDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).ReadById(toDoItem.ToDoItemId);
        repositoryMock.Received(1).Delete(toDoItem);
    }

    [Fact]
    public void Delete_DeleteByInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);


        repositoryMock.ReadById(99).Returns((ToDoItem)null);

        // Act

        var result = controller.DeleteById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).ReadById(99);
        repositoryMock.DidNotReceive().Delete(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Delete_DeleteByIdUnhandledException_ReturnsInternalServiceError()
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

        repositoryMock.ReadById(Arg.Any<int>()).Throws(new Exception());

        // Act

        var result = controller.DeleteById(1);

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
        repositoryMock.Received(1).ReadById(1);
        repositoryMock.DidNotReceive().Delete(Arg.Any<ToDoItem>());
    }
}

