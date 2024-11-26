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

    private readonly IRepositoryAsync<ToDoItem> repository;


    public ToDoItemsController (IRepositoryAsync<ToDoItem> repository)
    {
        this.repository = repository;
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            await repository.CreateAsync(item);
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return CreatedAtAction(
            nameof(ReadByIdAsync),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public async Task<IActionResult> ReadAsync()
    {
        List<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = await repository.ReadAsync();//context.ToDoItems.ToList();

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
    [ActionName(nameof(ReadByIdAsync))]
    public async Task<IActionResult> ReadByIdAsync(int toDoItemId)
    {
        try
        {
            var itemToGet = await repository.ReadByIdAsync(toDoItemId);
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
    public async Task<IActionResult> UpdateByIdAsync(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {

        var updatedItem = request.ToDomain();//map to Domain object as soon as possible

        try //try to update the item by retrieving it with given id
        {
            var currentItem = await repository.ReadByIdAsync(toDoItemId);

            if (currentItem == null)
            {
                return NotFound();
            }

            currentItem.Name = updatedItem.Name;
            currentItem.Description = updatedItem.Description;
            currentItem.IsCompleted = updatedItem.IsCompleted;
            currentItem.Category = updatedItem.Category;

            await repository.UpdateAsync(currentItem);

            return NoContent();
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public async Task<IActionResult> DeleteByIdAsync(int toDoItemId)
    {
        try
        {
            var itemToDelete = await repository.ReadByIdAsync(toDoItemId);

            if (itemToDelete == null)
            {
                return NotFound(); //404
            }

            await repository.DeleteAsync(toDoItemId);
            return NoContent();//204

        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

}


