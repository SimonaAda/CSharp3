using System;

namespace ToDoList.Domain.DTOs;

public class ToDoItemGetResponseDto(int Id, string Name, string Description, bool IsCompleted)
{
    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) => new(item.ToDoItemId, item.Name, item.Description, item.IsCompleted);
}
