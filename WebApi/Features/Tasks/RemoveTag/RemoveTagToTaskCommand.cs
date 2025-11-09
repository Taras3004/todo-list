using MediatR;

namespace WebApi.Features.Tasks.RemoveTag;

public class RemoveTagToTaskCommand : IRequest<bool>
{
    public int TaskId { get; set; }
    public int TagId { get; set; }
}
