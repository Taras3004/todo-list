using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Lists.UpdateList;

public class UpdateListCommand(int id, string name, string? description) : IRequest<TodoListResponse?>
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string? Description { get; } = description;
}
