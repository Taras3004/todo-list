using MediatR;

namespace WebApi.Features.Tasks.DeleteTask;

public class DeleteTaskByIdCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
