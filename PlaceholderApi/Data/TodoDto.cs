namespace PlaceholderApi.Data;

public class TodoDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public bool IsDone { get; set; }

    public TodoDto (Todo todo) => (Id,Name,Description,IsDone) = (todo.Id, todo.Name, todo.Description, todo.IsDone );
}