namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }


    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }

    public List<ToDoItem> Read()
    {
        return context.ToDoItems.ToList();

    }

    public ToDoItem ReadById(int toDoItemId)
    {

        return context.ToDoItems.Find(toDoItemId);
    }

    public void Update(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        context.SaveChanges();
    }

    public void Delete(ToDoItem item)
    {
        var itemToDelete = context.ToDoItems.Find(item);

        if (itemToDelete != null)
        {
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
    }
}
