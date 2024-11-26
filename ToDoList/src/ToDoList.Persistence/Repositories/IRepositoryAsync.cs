namespace ToDoList.Persistence.Repositories;

using System.Collections.Generic;
using System.Runtime.InteropServices;
using ToDoList.Domain.Models;

public interface IRepositoryAsync<T> where T : class
{
    public Task CreateAsync(T item);
    Task<List<ToDoItem>> ReadAsync();

    Task<ToDoItem> ReadByIdAsync(int toDoItemId);

    public Task UpdateAsync(T item);

    public Task DeleteAsync(int toDoItemId);

}
