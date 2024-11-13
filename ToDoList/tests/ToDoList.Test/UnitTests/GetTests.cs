namespace ToDoList.Test.UnitTests;
using NSubstitute;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using NSubstitute.ExceptionExtensions;
using Microsoft.AspNetCore.Http;

public class GetUnitTests
{

    [Fact]
    public void Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.Read().Returns(
            [
                new ToDoItem{
                    Name = "testName",
                    Description = "testDescription",
                    IsCompleted = false
                }
            ]
            );

        // Act
        var result = controller.Read();
        var okResult = result as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
        repositoryMock.Received(1).Read();
        //jeste by to chtelo check ze jsme dostali zpatky nejaky iterovatelny objekt ktery v sobe ma tolik itemu kolik jich ocekavame podle mocku :)
    }

    [Fact]
    public void Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        // repositoryMock.ReadAll().ReturnsNull();
        repositoryMock.Read().Returns(null as List<ToDoItem>);

        // Act
        var result = controller.Read();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).Read();
    }

    [Fact]
    public void Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.Read().Throws(new Exception());

        // Act
        var result = controller.Read();

        // Assert
        Assert.IsType<ObjectResult>(result);
        repositoryMock.Received(1).Read();
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
    }
}


