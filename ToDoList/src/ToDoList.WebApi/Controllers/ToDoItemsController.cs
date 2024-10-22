namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]

public class ToDoItemsController : ControllerBase
{
    public static List<ToDoItem> items = [];

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
        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        List<ToDoItem> itemsToGet;
        try
        {
            if (items == null)
            {
                return NotFound();
            }
            itemsToGet = items;
        }

        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain));

    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        try
        {
            var toDoItem = items.Find(i => i.ToDoItemId == toDoItemId);
            if (toDoItem == null)
            {
                return NotFound();
            }
            var itemsToGet = ToDoItemGetResponseDto.FromDomain(toDoItem);
            return Ok(itemsToGet);
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var updatedItem = request.ToDomain();
            updatedItem.ToDoItemId = toDoItemId;
            var currentItemIndex = items.FindIndex(i => i.ToDoItemId == toDoItemId);

            if (currentItemIndex == -1)
            {
                return NotFound();
            }

            items[currentItemIndex] = updatedItem;

            return NoContent();
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
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
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

    }
}


