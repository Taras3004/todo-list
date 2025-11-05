using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.TagToTask.CreateTagToTask;


public class CreateTagToTaskHandler(TodoListDbContext context) : IRequestHandler<CreateTagToTaskCommand, TagToTaskDto?>
{
    public async Task<TagToTaskDto?> Handle(CreateTagToTaskCommand request, CancellationToken cancellationToken)
    {
        var tagToTask = new Model.Entities.TodoDb.TagToTask() { TodoTaskId = request.TodoTaskId, TaskTagId = request.TaskTagId, };

        var existingSameTag = await context.TagToTask.FirstOrDefaultAsync(x =>
            x.TodoTaskId == request.TodoTaskId || x.TaskTagId == request.TaskTagId, cancellationToken);

        if (existingSameTag != null)
        {
            return null;
        }

        await context.TagToTask.AddAsync(tagToTask, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new TagToTaskDto()
        {
            Id = tagToTask.Id, TodoTaskId = tagToTask.TodoTaskId, TaskTagId = tagToTask.TaskTagId,
        };
    }
}
