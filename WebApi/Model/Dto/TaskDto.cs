namespace WebApi.Model.Dto;

public record TaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }

    public string? Description { get; set; }

    public int TodoListId { get; set; }
}
