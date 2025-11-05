using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Tags.DeleteTag;

public class DeleteTagCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
