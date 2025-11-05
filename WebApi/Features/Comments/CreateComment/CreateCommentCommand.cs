using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Comments.CreateComment;

public class CreateCommentCommand(string content, int todoTaskId) : IRequest<TaskCommentDto>
{
    public string Content { get; } = content;
    public int TodoTaskId { get; } = todoTaskId;
}
