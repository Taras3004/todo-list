namespace WebApi.Model.Dto.Requests.Task;

public record CreateTaskRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required int TodoListId { get; init; }
}
