using Microsoft.EntityFrameworkCore;
using PlaceholderApi.Data;

namespace PlaceholderApi.Services;

public class TodoService : ITodoService
{
    private readonly TodoDbContext _dbContext;

    public TodoService(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Todo>> Read()
    {
        return await _dbContext.Todos.ToListAsync();
    }

    public async Task<List<Todo>> ReadDone()
    {
        return await _dbContext.Todos.Where(t => t.IsDone).ToListAsync();
    }

    public async Task<Todo?> ReadById(int id)
    {
        return await _dbContext.Todos.FindAsync(id);
    }

    public async Task<Todo> Create(Todo todo)
    {
        _dbContext.Todos.Add(todo);
        await _dbContext.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo?> Update(int id, Todo inputTodo)
    {
        var todo = await _dbContext.Todos.FindAsync(id);
        if (todo == null) return null;
        todo.Name = inputTodo.Name;
        todo.Description = inputTodo.Description;
        todo.IsDone = inputTodo.IsDone;
        await _dbContext.SaveChangesAsync();
        return todo;
    }

    public async Task Delete(Todo todo)
    {
        _dbContext.Todos.Remove(todo);
        await _dbContext.SaveChangesAsync();
    }
}