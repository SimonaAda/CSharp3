namespace ToDoList.Persistence;

using Microsoft.EntityFrameworkCore; 
using ToDoList.Domain.Models;

public class ToDoItemsContext(string connectionString = "Data Source=../../data/localdb.db") : DbContext
{
    private readonly string connectionString = connectionString;

    public DbSet<ToDoItem> ToDoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString);
    }

}
