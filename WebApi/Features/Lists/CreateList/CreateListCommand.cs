using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Lists.CreateList;

public class CreateListCommand(string name, string description) : IRequest<TodoListDto>
{
    public string Name { get; } = name;
    public string Description { get; } = description;
}
