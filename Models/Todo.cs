using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models;

public class Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; } = false;
}

class TodoDb : DbContext
{
    public TodoDb(DbContextOptions options) : base(options) { }
    public DbSet<Todo> Todos { get; set; } = null!;
}
