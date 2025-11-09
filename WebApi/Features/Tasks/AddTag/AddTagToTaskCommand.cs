using MediatR;

namespace WebApi.Features.Tasks.AddTag;

public class AddTagToTaskCommand : IRequest<bool>
{
    public int TaskId { get; set; }
    public int TagId { get; set; }
}
