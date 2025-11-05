using MediatR;
using WebApi.Model.DTO;

namespace WebApi.Features.Tasks.DeleteTask;

public class DeleteTaskByIdCommand : IRequest<bool>
{
    public int Id { get; set; }
}
