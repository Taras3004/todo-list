using MediatR;

namespace WebApi.Features.TagToTask.DeleteTagToTask;

public class DeleteTagToTaskCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
