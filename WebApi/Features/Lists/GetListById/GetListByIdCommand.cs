using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Lists.GetListById;

public class GetListByIdCommand(int id) : IRequest<TodoListDto?>
{
    public int Id { get; } = id;
}
