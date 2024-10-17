namespace ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

//tohle jsem zkopiroval ze zadani at to mame vsichni stejne :)
public record ToDoItemGetResponseDto(int Id, string Name, string Description, bool IsCompleted)
{
    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) => new(item.ToDoItemId, item.Name, item.Description, item.IsCompleted);
}
