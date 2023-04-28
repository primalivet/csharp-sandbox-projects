using Microsoft.EntityFrameworkCore;

namespace PlaceholderApi.Data;

public class TodoDbContext : DbContext
{
    public DbSet<Todo> Todos => Set<Todo>();
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
}