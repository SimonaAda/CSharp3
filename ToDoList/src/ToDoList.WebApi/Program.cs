using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
{


    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<ToDoItemsContext>();
    builder.Services.AddScoped<IRepositoryAsync<ToDoItem>, ToDoItemsRepository>();
}

var app = builder.Build();
{

    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API V1"));
}

app.Run();
