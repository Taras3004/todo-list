using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Comments.GetCommentById;

public class GetTaskCommentByIdCommand(int id) : IRequest<TaskCommentResponse?>
{
    public int Id { get; } = id;
}
