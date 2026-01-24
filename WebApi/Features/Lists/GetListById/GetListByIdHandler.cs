using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.GetListById;

public class GetListByIdHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<GetListByIdCommand, TodoListResponse?>
{
    public async Task<TodoListResponse?> Handle(GetListByIdCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var list = await context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (list == null)
        {
            return null;
        }

        return new TodoListResponse()
        {
            Id = list.Id,
            Name = list.Name,
            Description = list.Description,
        };
    }
}
