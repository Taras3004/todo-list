using MediatR;
using WebApi.Model.DTO;

namespace WebApi.Features.Tasks.GetTaskById;

public class GetTaskByIdCommand : IRequest<TaskDto?>
{
    public int Id { get; set; }
}
