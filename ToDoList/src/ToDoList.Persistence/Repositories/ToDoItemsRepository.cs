namespace ToDoList.Persistence.Repositories;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepositoryAsync<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }


    public async Task CreateAsync(ToDoItem item)
    {
        await context.ToDoItems.AddAsync(item);
        await context.SaveChangesAsync();
    }

    public async Task<List<ToDoItem>> ReadAsync()
    {
        return await context.ToDoItems.ToListAsync();

    }

    public async Task<ToDoItem> ReadByIdAsync(int toDoItemId)
    {

       return await context.ToDoItems.FindAsync(toDoItemId);
    }

    public async Task UpdateAsync(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ToDoItem item)
    {
        var itemToDelete = await context.ToDoItems.FindAsync(item);

        if (itemToDelete != null)
        {
            context.ToDoItems.Remove(itemToDelete);
            await context.SaveChangesAsync();
        }
    }

}
