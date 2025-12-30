using MediatR;

namespace WebApi.Features.Comments.GetCommentById;

public class GetTaskCommentByIdCommand(int id) : IRequest<TaskCommentResponse?>
{
    public int Id { get; } = id;
}
