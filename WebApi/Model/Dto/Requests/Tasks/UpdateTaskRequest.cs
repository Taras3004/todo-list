namespace WebApi.Model.Dto.Requests.Tasks;

public record UpdateTaskRequest
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public DateTime Deadline { get; init; }

    public bool IsCompleted { get; init; }

    public string? Description { get; init; }
}
