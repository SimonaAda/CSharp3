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
    public void Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
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

        repositoryMock.ReadById(toDoItem.ToDoItemId).Returns(toDoItem);

        // Act
        var result = controller.UpdateById(toDoItem.ToDoItemId, updatedItem);//(toDoItem.TodoItemId, updatedItem)

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false,
            Category = "Kategorie"
        };

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true, "Updated Kategorie");

        repositoryMock.ReadById(99).Returns((ToDoItem)null);

        // Act
        var result = controller.UpdateById(99, updatedItem);//ivaldiID = 99,(invalidId, updatedID)
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);
        repositoryMock.Received(1).ReadById(99);
        repositoryMock.DidNotReceive().Update(Arg.Any<ToDoItem>());


    }

    [Fact]
    public void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
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

        repositoryMock.ReadById(toDoItem.ToDoItemId).Returns(toDoItem);
        repositoryMock.When(r => r.Update(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Jmeno", "Updated Popis", true, "Updated Kategorie");

        // Act
        var result = controller.UpdateById(toDoItem.ToDoItemId, updatedItem);//(toDoItem.TodoItemId, updatedItem)

        // Assert
        Assert.IsType<ObjectResult>(result);
        repositoryMock.Received(1).ReadById(toDoItem.ToDoItemId);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
    }
}

