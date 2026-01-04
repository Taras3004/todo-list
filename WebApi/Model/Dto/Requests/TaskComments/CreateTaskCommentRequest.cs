namespace WebApi.Model.Dto.Requests.TaskComments;

public record CreateTaskCommentRequest
{
    public required string Content { get; init; }
    public required int TodoTaskId { get; init; }
}
