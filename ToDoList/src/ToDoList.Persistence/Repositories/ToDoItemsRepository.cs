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

    public void Update(ToDoItem updatedItem)
    {
        context.ToDoItems.Update(updatedItem);
        context.SaveChanges();
    }

    //pokud budeme pracovat s ToDoItemsRepository pres interface IRepository<ToDoItem>, tak ten nezna tuto metodu
    //interface zna pouze public void Delete(T item) - cili public void Delete(ToDoItem item);
    public void Delete(int toDoItemId)
    {
        var itemToDelete = context.ToDoItems.Find(toDoItemId);

        if (itemToDelete != null)
        {
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
    }

    public void Delete(ToDoItem itemToDelete) => throw new NotImplementedException();
}
