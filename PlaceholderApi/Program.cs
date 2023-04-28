using Microsoft.EntityFrameworkCore;
using PlaceholderApi;
using PlaceholderApi.Data;
using PlaceholderApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodosList")); // add the database context to the dependency injection container
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // handle database related exeptions
builder.Services.AddTransient<ITodoService, TodoService>();
var app = builder.Build();

var todoItems = app.MapGroup("/todoitems");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/done", GetDoneTodos);
todoItems.MapGet("/{id}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/{id}", UpdateTodo);
todoItems.MapDelete("/{id}", RemoveTodo);

static async Task<IResult> GetAllTodos(ITodoService todoService)
{
    return TypedResults.Ok(await todoService.Read());
}

static async Task<IResult> GetDoneTodos(ITodoService todoService)
{
    return TypedResults.Ok(await todoService.ReadDone());
}

static async Task<IResult> GetTodo(int id, ITodoService todoService)
{
    return await todoService.ReadById(id) is Todo todo
        ? TypedResults.Ok(todo)
        : TypedResults.NotFound();
}

static async Task<IResult> CreateTodo(Todo todo, ITodoService todoService)
{
    return TypedResults.Created($"/{todo.Id}", await todoService.Create(todo));
}

static async Task<IResult> UpdateTodo(int id, Todo inputTodo, ITodoService todoService)
{
    return await todoService.Update(id, inputTodo) is Todo todo
        ? TypedResults.NoContent()
        : TypedResults.NotFound();
}

static async Task<IResult> RemoveTodo(int id, TodoDbContext db)
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.Ok(new TodoDto(todo)); // TODO: should return NoContent
    }
    return TypedResults.NotFound();
}

app.Run();
