namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Helpers;

public class PutUnitTests
{
    [Fact]
    public async Task Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
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

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true, "Updated Kategorie");

        repositoryMock.ReadByIdAsync(toDoItem.ToDoItemId).Returns(toDoItem);

        // Act
        var result = await controller.UpdateByIdAsync(toDoItem.ToDoItemId, updatedItem);//(toDoItem.TodoItemId, updatedItem)

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false,
            Category = "Kategorie"
        };

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true, "Updated Kategorie");

        repositoryMock.ReadByIdAsync(99).Returns((ToDoItem)null);

        // Act
        var result = await controller.UpdateByIdAsync(99, updatedItem);//ivaldiID = 99,(invalidId, updatedID)
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);
        await repositoryMock.Received(1).ReadByIdAsync(99);
        await repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<ToDoItem>());


    }

    [Fact]
    public async Task Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        repositoryMock.ReadByIdAsync(toDoItem.ToDoItemId).Returns(toDoItem);

        repositoryMock.When(r => r.UpdateAsync(Arg.Any<ToDoItem>())).Throw (new Exception());

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true, "Updated Kategorie");

        // Act
        var result = await controller.UpdateByIdAsync(toDoItem.ToDoItemId, updatedItem);//(toDoItem.TodoItemId, updatedItem)

        // Assert
        Assert.IsType<ObjectResult>(result);
        await repositoryMock.Received(1).ReadByIdAsync(toDoItem.ToDoItemId);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
    }
}

