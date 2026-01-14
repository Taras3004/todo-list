using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Tasks.GetTaskById;

public class GetTaskByIdCommand(int id) : IRequest<TaskDetailsResponse?>
{
    public int Id { get; } = id;
}
