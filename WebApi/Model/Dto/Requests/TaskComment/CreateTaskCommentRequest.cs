namespace WebApi.Model.Dto.Requests.TaskComment;

public record CreateTaskCommentRequest
{
    public required string Content { get; init; }
    public required int TodoTaskId { get; init; }
}
