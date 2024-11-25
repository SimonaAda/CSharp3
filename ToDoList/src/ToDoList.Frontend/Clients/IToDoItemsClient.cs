using ToDoList.Frontend.Views;

namespace ToDoList.Frontend.Clients
{
    public interface IToDoItemsClient //IToDOItemsClient<T> where T : class
    {
        public Task<List<ToDoItemView>> ReadItemsAsync();
    }
}
