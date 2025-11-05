using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Lists.UpdateList;

public class UpdateListCommand : IRequest<TodoListDto?>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
