namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]

public class ToDoItemsController : ControllerBase
{
    private static readonly List<ToDoItem> items = [];

    [HttpPost]
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
        try
        {

            if (items == null)
            {
                return NotFound();
            }

            var ToDoItem = items.ToDoItemGetResponseDto.FromDomain.ToList();
            items.Add(ToDoItem);
            return Ok(ToDoItem);
        }
        catch(Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId, [FromBody] ToDoItemGetResponseDto request)
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

        try
        {
            var toDoItem = items.Find(item => item.ToDoItemId == toDoItemId);

            if (toDoItem == null)
            {
                return NotFound();
            }
            items.Remove(toDoItem);
            return NoContent();
        }
        catch(Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

    }
}


