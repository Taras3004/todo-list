using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.AddTag;

public class AddTagToTaskHandler(TodoListDbContext context) : IRequestHandler<AddTagToTaskCommand, bool>
{
    public async Task<bool> Handle(AddTagToTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == request.TaskId, cancellationToken);

        if (task == null)
        {
            return false;
        }

        var tag = await context.TaskTags.FirstOrDefaultAsync(x => x.Id == request.TagId, cancellationToken);

        if (tag == null)
        {
            return false;
        }

        var tagToTask = new TagToTask
        {
            TaskTagId = request.TagId,
            TodoTaskId = request.TaskId,
        };

        await context.TagToTask.AddAsync(tagToTask, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
