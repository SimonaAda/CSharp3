namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]

public class ToDoItemsController : ControllerBase
{

    private readonly ToDoItemsContext context;
    private readonly IRepository<ToDoItem> repository;


    public ToDoItemsController(ToDoItemsContext context, IRepository<ToDoItem> repository)
    {
        this.context = context;
        this.repository = repository;
    }


    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            repository.Create(item);
        }
        catch (Exception ex)
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
            itemsToGet = repository.Read();//context.ToDoItems.ToList();

            if (itemsToGet == null)
            {
                return NotFound();
            }

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
            var itemToGet = repository.ReadById(toDoItemId);
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
            var currentItem = repository.ReadById(toDoItemId);

            if (currentItem == null)
            {
                return NotFound();
            }

            currentItem.Name = updatedItem.Name;
            currentItem.Description = updatedItem.Description;

            repository.Update(updatedItem);

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
            var itemToDelete = repository.ReadById(toDoItemId);

            if (itemToDelete == null)
            {
                return NotFound(); //404
            }

            repository.Delete(itemToDelete);
            return NoContent();//204

        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

}


