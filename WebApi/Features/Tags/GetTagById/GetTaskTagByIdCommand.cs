using MediatR;

namespace WebApi.Features.Tags.GetTagById;

public class GetTaskTagByIdCommand(int id) : IRequest<TaskTagResponse?>
{
    public int Id { get; } = id;
}
