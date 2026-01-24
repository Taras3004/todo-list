using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.RemoveTag;

public class RemoveTagToTaskHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<RemoveTagToTaskCommand, bool>
{
    public async Task<bool> Handle(RemoveTagToTaskCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var tagToTask = await context.TagToTask.FirstOrDefaultAsync(x => x.TaskTagId == request.TagId &&
                                                                         x.TodoTaskId == request.TaskId &&
                                                                         x.Task.UserId == userId, cancellationToken);

        if (tagToTask == null)
        {
            return false;
        }

        context.TagToTask.Remove(tagToTask);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
