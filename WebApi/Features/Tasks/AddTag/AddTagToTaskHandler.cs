using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.AddTag;

public class AddTagToTaskHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<AddTagToTaskCommand, bool>
{
    public async Task<bool> Handle(AddTagToTaskCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == request.TaskId && x.UserId == userId, cancellationToken);

        if (task == null)
        {
            return false;
        }

        var tag = await context.TaskTags.FirstOrDefaultAsync(x => x.Id == request.TagId && x.UserId == userId, cancellationToken);

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
