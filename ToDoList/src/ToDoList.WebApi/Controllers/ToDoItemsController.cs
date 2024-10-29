namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[ApiController]
[Route("api/[controller]")]

public class ToDoItemsController : ControllerBase
{

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
            context.ToDoItems.Add(item);
            context.SaveChanges();
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
        try
        {
            var itemsToGet = context.ToDoItems.ToList();

            if (itemsToGet == null)
            {
                return NotFound();
            }

        }

        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return Ok(context.ToDoItems.Select(ToDoItemGetResponseDto.FromDomain));

    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId, ToDoItemGetResponseDto request) //nefunguje kontroler ocekava ze dostane ToDoItemGetResponseDto v body http get requestu, ale to neposilame => nezna takovy request
    {
        try
        {
            var itemToGet = context.ToDoItems.Find(toDoItemId);
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
            var currentItem = context.ToDoItems.Find(toDoItemId);

            if (currentItem == null)
            {
                return NotFound();
            }
            /*nefunguje, pojdme si projit co tvuj kod dela
            1) najdes si v context item co ma dane id a mas to jako lokalni projenou currentItem - to je OK :)
            2) zkontolujes si ze jsi nasla dany item - to je OK :)
            3) objektu updatedItem priradis ID
            4) ulozis zmeny do databaze - akorat tady se zadne nestaly
            */
            updatedItem.ToDoItemId = toDoItemId;
            context.SaveChanges();

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
            var itemToDelete = context.ToDoItems.Find(toDoItemId);

            if (itemToDelete == null)
            {
                return NotFound(); //404
            }
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent();//204
    }

}


