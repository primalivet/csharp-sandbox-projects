namespace PlaceholderApi.Data;

public class Todo
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public bool IsDone { get; set; }
    public string? Secret { get; set; }
}