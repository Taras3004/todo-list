namespace WebApi.Model.Dto.Requests.TaskTag;

public record CreateTaskTagRequest
{
    public required string Tag { get; init; }
}
