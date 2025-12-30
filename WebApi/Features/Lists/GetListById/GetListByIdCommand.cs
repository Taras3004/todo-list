using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Lists.GetListById;

public class GetListByIdCommand(int id) : IRequest<TodoListResponse?>
{
    public int Id { get; } = id;
}
