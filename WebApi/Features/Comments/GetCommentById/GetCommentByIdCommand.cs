using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Comments.GetCommentById;

public class GetCommentByIdCommand(int id) : IRequest<TaskCommentDto?>
{
    public int Id { get; } = id;
}
