using Microsoft.OpenApi.Models;
using TodoList.Repositories;
using TodoList.Services.Tasks;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

    // services
    builder.Services.AddScoped<ITaskService, TaskService>();
    builder.Services.AddScoped<ITaskRepository, TaskRepository>();

    // controllers
    builder.Services.AddControllers();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todolist API", Version = "v1" });
    });
}



var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todolist API");
    });
    
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}