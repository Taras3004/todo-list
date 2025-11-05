using MediatR;

namespace WebApi.Features.Comments.DeleteComment;

public class DeleteCommentCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
