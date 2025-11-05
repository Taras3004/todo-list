using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Tags.UpdateTag;

public class UpdateTagCommand : IRequest<TaskTagDto?>
{
    public int Id { get; set; }
    public string Tag { get; set; }
}
