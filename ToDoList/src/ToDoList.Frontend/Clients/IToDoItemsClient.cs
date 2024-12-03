using ToDoList.Frontend.Views;

namespace ToDoList.Frontend.Clients
{
    public interface IToDoItemsClient //IToDOItemsClient<T> where T : class
    {
    public Task CreateItemAsync(ToDoItemView itemView);
    public Task<ToDoItemView?> ReadItemByIdAsync(int itemId);
    public Task<List<ToDoItemView>> ReadItemsAsync();
    public Task DeleteItemAsync(ToDoItemView itemView);
    public Task UpdateItemAsync(ToDoItemView itemView);
}
}
