using ToDoList.Persistence;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddControllers();
    builder.Services.AddDbContext<ToDoItemsContext>();
}

var app = builder.Build();
{

    app.MapControllers();
}

app.MapGet("/", () => "Hello World");
app.MapGet("/ahojSvete", () => "Nazdar ty tam!");

app.Run();
