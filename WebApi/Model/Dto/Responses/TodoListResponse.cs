namespace WebApi.Model.Dto.Responses;

public record TodoListResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}
