namespace ToDoList.Test;

using Microsoft.AspNetCore.Authentication; //zbytecne
using Microsoft.AspNetCore.Mvc.Diagnostics; //zbytecne
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class GetTests
{

    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var controller = new ToDoItemsController();
        ToDoItemsController.items = [];
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        ToDoItemsController.items.Add(toDoItem);

        // Act
        var result = controller.Read();
        var value = result.Value;
        var resultResult = result.Result;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        var firstItem = value.First(); //neprochazi to, tady nas zloby ty problemy co byly na lekci, doporucuji si vratit to aby Read metoda vracela IActionResult, je to pak jednodussi :)
        Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);

        //spis by to chtelo test ze pokud mame v items 3 ukoly, tak dostanu 3 ukoly z Read akce - muze to vracet nespravny pocet

    }
}


