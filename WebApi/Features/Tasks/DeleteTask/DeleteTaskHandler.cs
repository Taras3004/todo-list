using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.DeleteTask;

public class DeleteTaskHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<DeleteTaskByIdCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (task == null)
        {
            return false;
        }

        context.Tasks.Remove(task);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
