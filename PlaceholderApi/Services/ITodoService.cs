using PlaceholderApi.Data;

namespace PlaceholderApi.Services;

public interface ITodoService
{
    Task<List<Todo>> Read();

    Task<List<Todo>> ReadDone();

    Task<Todo?> ReadById(int id);

    Task<Todo> Create(Todo todo);

    Task<Todo?> Update(int id, Todo todo);

    Task Delete(Todo todo);
}