using TodoApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Todos") ?? "Data Source=Todos.db";

builder.Services.AddSqlite<TodoDb>(connectionString);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/todos", () => "Here are your todos!");
app.MapGet("/todos/{id:int}", (int id) => { return "Here are your todos! " + id; });
app.MapPut("/", () => "Update todo");
app.MapPost("/", () => "add todo");
app.MapDelete("/todos/{id:int}", (int id) => { return "delete todo " + id; });

app.Run();
