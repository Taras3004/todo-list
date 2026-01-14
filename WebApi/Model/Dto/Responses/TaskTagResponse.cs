namespace WebApi.Model.Dto.Responses;

public class TaskTagResponse
{
    public required int Id { get; init; }
    public required string Tag { get; init; }
    public string? Color { get; init; }
}
