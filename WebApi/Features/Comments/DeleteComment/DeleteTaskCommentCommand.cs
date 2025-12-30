using MediatR;

namespace WebApi.Features.Comments.DeleteComment;

public class DeleteTaskCommentCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
