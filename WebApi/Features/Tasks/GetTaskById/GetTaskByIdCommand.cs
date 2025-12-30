using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Tasks.GetTaskById;

public class GetTaskByIdCommand(int id) : IRequest<TaskResponse?>
{
    public int Id { get; } = id;
}
