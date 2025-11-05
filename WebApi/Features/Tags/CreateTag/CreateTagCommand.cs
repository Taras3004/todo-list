using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Tags.CreateTag;

public class CreateTagCommand(string name, string description) : IRequest<TaskTagDto>
{
    public string Tag { get; set; }
}
