namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[ApiController]
[Route("api/[controller]")]

public class ToDoItemsController : ControllerBase
{
    public readonly List<ToDoItem> items = [];
    private readonly ToDoItemsContext context;

    public ToDoItemsController(ToDoItemsContext context)
    {
        this.context = context;
    }


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
    public IActionResult Read()
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
    public IActionResult ReadById(int toDoItemId, ToDoItemGetResponseDto request)
    {
        ToDoItem? itemToGet;
        try
        {
            itemToGet = items.Find(i => i.ToDoItemId == toDoItemId);
            if (itemToGet == null)
            {
                return NotFound();
            }
            return Ok(ToDoItemGetResponseDto.FromDomain(itemToGet));
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {

        var updatedItem = request.ToDomain();//map to Domain object as soon as possible

        try //try to update the item by retrieving it with given id
        {
            var currentItemIndex = items.FindIndex(i => i.ToDoItemId == toDoItemId);

            if (currentItemIndex == -1)
            {
                return NotFound();
            }
            updatedItem.ToDoItemId = toDoItemId;
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
            var itemToDelete = items.Find(item => item.ToDoItemId == toDoItemId);

            if (itemToDelete == null)
            {
                return NotFound(); //404
            }
            items.Remove(itemToDelete);
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent();//204
    }

    public OkObjectResult ReadById(int v) => throw new NotImplementedException();
}


