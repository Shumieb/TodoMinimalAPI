using TodoApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// database connection string
var connectionString = builder.Configuration.GetConnectionString("Todos") ?? "Data Source=Todos.db";

// add an sqlite database
builder.Services.AddSqlite<TodoDb>(connectionString);

var app = builder.Build();

// get all todos
app.MapGet("/todos", async (TodoDb db) => await db.Todos.ToListAsync());

// get todos by Id
app.MapGet("/todo/{id}", async (TodoDb db, int id) => await db.Todos.FindAsync(id));

// add a new todo
app.MapPost("/todo", async (TodoDb db, Todo todo) =>
{
    await db.Todos.AddAsync(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todo/{todo.Id}", todo);
});

// update a todo
app.MapPut("/todo/{id}", async (TodoDb db, Todo updatedTodo, int id) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Name = updatedTodo.Name;
    todo.Description = updatedTodo.Description;
    todo.IsComplete = updatedTodo.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// delete a todo
app.MapDelete("/todo/{id}", async (TodoDb db, int id) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null)
    {
        return Results.NotFound();
    }
    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok();
});

// filter by isComplete
app.MapGet("/todos/completed", async (TodoDb db) => await db.Todos
                                                            .Where(todo => todo.IsComplete == true)
                                                            .ToListAsync());

app.Run();
