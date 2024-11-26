namespace ToDoList.Test.UnitTests;
using NSubstitute;
using System.Security.Cryptography.X509Certificates; //ojojoj :D pozor na usingy
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
    public async Task Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAsync().Returns(
            [
                new ToDoItem{
                    Name = "testName",
                    Description = "testDescription",
                    IsCompleted = false,
                    Category = "Kategorie"
                }
            ]
            );

        // Act
        var result = await controller.ReadAsync();
        var okResult = result as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
        await repositoryMock.Received(1).ReadAsync();
    }

    [Fact]
    public async Task Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        // repositoryMock.ReadAll().ReturnsNull();
        repositoryMock.ReadAsync().Returns(null as List<ToDoItem>);

        // Act
        var result = await controller.ReadAsync();

        // Assert
        Assert.IsType<NotFoundResult>(result);
        await repositoryMock.Received(1).ReadAsync();
    }

    [Fact]
    public async Task Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAsync().Throws(new Exception());

        // Act
        var result = await controller.ReadAsync();

        // Assert
        Assert.IsType<ObjectResult>(result);
        await repositoryMock.Received(1).ReadAsync();
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
    }
}


