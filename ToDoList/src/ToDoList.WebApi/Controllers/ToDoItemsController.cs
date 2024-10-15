namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]

public class ToDoItemsController : ControllerBase
{
    private static readonly List<ToDoItem> items = [];

    [HttpGet]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch(Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return Created();
    }

    [HttpGet]
    public IActionResult Read()
    {
        return Ok();
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        return Ok();
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        return Ok();
    }

    [HttpDelete("{toDoItemId:int}")]
     public IActionResult DeleteById(int toDoItemId)
    {
        return Ok();
    }

}
