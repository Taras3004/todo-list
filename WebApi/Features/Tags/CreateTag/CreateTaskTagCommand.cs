using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Tags.CreateTag;

public class CreateTaskTagCommand(string tag, string? color) : IRequest<TaskTagResponse>
{
    public string Tag { get; } = tag;

    public string? Color { get; } = color;
}
