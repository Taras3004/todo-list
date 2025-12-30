using MediatR;

namespace WebApi.Features.Tags.UpdateTag;

public class UpdateTaskTagCommand(int id, string tag) : IRequest<TaskTagResponse?>
{
    public int Id { get; } = id;
    public string Tag { get; } = tag;
}
