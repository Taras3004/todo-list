using MediatR;

namespace WebApi.Features.Comments.CreateComment;

public class CreateTaskCommentCommand(string content, int todoTaskId) : IRequest<TaskCommentResponse>
{
    public string Content { get; } = content;
    public int TodoTaskId { get; } = todoTaskId;
}
