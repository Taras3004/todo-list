using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.DeleteList;

public class DeleteListHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<DeleteListCommand, bool>
{
    public async Task<bool> Handle(DeleteListCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var list = await context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (list == null)
        {
            return false;
        }

        context.Todos.Remove(list);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
