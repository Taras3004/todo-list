using MediatR;

namespace WebApi.Features.Lists.GetListById;

public class GetListByIdCommand(int id) : IRequest<TodoListResponse?>
{
    public int Id { get; } = id;
}
