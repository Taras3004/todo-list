using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Lists.CreateList;

public class CreateListCommand(string name, string? description) : IRequest<TodoListResponse>
{
    public string Name { get; } = name;
    public string? Description { get; } = description;
}
