using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Tags.GetTagById;

public class GetTagByIdCommand(int id) : IRequest<TaskTagDto?>
{
    public int Id { get; } = id;
}
