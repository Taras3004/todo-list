using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.DTO;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.UpdateTask;

public class UpdateTaskHandler(TodoListDbContext context) : IRequestHandler<UpdateTaskCommand, TaskDto?>
{
    public async Task<TaskDto?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var existingTask = await context.Tasks
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var existingTaskPage = await context.TaskPages
            .FirstOrDefaultAsync(x => x.TodoTaskId == request.Id, cancellationToken);

        if (existingTask == null || existingTaskPage == null)
        {
            return null;
        }

        existingTask.Name = request.Name;
        existingTask.Deadline = request.Deadline;
        existingTask.IsCompleted = request.IsCompleted;

        existingTaskPage.Description = request.Description;

        await context.SaveChangesAsync(cancellationToken);

        return new TaskDto()
        {
            Id = existingTask.Id,
            Name = existingTask.Name,
            Deadline = existingTask.Deadline,
            IsCompleted = existingTask.IsCompleted,
            Description = existingTaskPage.Description,
            TodoListId = existingTask.TodoListId,
        };
    }
}
