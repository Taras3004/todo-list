using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.RemoveTag;

public class RemoveTagToTaskHandler(TodoListDbContext context) : IRequestHandler<RemoveTagToTaskCommand, bool>
{
    public async Task<bool> Handle(RemoveTagToTaskCommand request, CancellationToken cancellationToken)
    {
        var tagToTask = await context.TagToTask.FirstOrDefaultAsync(x => x.TaskTagId == request.TagId &&
                                                                         x.TodoTaskId == request.TaskId, cancellationToken);

        if (tagToTask == null)
        {
            return false;
        }

        context.TagToTask.Remove(tagToTask);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
