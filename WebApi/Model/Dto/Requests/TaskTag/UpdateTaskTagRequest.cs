namespace WebApi.Model.Dto.Requests.TaskTag;

public record UpdateTaskTagRequest
{
    public required int Id { get; init; }
    public required string Tag { get; init; }
}
