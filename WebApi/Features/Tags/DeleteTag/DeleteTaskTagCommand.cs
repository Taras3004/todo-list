using MediatR;

namespace WebApi.Features.Tags.DeleteTag;

public class DeleteTaskTagCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
