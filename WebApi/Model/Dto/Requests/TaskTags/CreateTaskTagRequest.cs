namespace WebApi.Model.Dto.Requests.TaskTags;

public record CreateTaskTagRequest
{
    public required string Tag { get; init; }
}
