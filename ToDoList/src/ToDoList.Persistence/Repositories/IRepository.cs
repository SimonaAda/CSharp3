namespace ToDoList.Persistence.Repositories;

using System.Collections.Generic;
using ToDoList.Domain.Models;

public interface IRepository<T> where T : class
{
    public void Create(T item);
    List<ToDoItem> Read();

    ToDoItem ReadById(int toDoItemId);

    public void Update(T updatedItem);

    public void Delete(T itemToDelete);

}

