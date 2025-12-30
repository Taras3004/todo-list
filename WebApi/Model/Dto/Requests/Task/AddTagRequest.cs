namespace WebApi.Model.Dto.Requests.Task;

public record AddTagToTaskRequest
{
    public required int TagId { get; init; }
}
