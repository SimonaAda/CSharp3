namespace ToDoList.Test.IntegrationTests;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;


public class DeleteTests
{
    [Fact]
    public void Delete_byId_NotContent_WhenValid()
    {
        //Arrange
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Name",
            Description = "Description",
            IsCompleted = false
        };

        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();

        //Act
        var result = controller.DeleteById(toDoItem.ToDoItemId);
        var noContentResult = result as NoContentResult;

        //Assert
        Assert.IsType<NoContentResult>(result);

        // Assert
        Assert.IsType<NoContentResult>(noContentResult);
        Assert.NotNull(noContentResult);

    }

    [Fact]
    public void Delete_byId_NotFound_WhenInvalid()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Name",
            Description = "Description",
            IsCompleted = false
        };

        context.ToDoItems.Add(toDoItem);
        context.SaveChanges();


        // Act
        var result = controller.DeleteById(99);// var invalidId = 99; controller.DeleteById(invalidId)
        var notFoundResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.NotNull(notFoundResult);

    }
}

