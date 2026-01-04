namespace WebApi.Model.Dto.Requests.TaskTags;

public record UpdateTaskTagRequest
{
    public required int Id { get; init; }
    public required string Tag { get; init; }
}
