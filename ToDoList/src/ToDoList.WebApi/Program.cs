var builder = WebApplication.CreateBuilder(args);
{
   
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    
    app.MapControllers();
}

app.MapGet("/", () => "Hello World");
app.MapGet("/ahojSvete", () => "Nazdar ty tam!");

app.Run();
