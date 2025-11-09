using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Tasks.GetTaskById;

public class GetTaskByIdCommand(int id) : IRequest<TaskDto?>
{
    public int Id { get; } = id;
}
