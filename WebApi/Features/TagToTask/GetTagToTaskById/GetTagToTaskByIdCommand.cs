using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.TagToTask.GetTagToTaskById;

public class GetTagToTaskByIdCommand(int id) : IRequest<TagToTaskDto?>
{
    public int Id { get; } = id;
}
