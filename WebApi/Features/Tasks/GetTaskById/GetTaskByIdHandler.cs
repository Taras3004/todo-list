using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.DTO;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.GetTaskById;

public class GetTaskByIdHandler(TodoListDbContext context) : IRequestHandler<GetTaskByIdCommand, TaskDto?>
{
    public async Task<TaskDto?> Handle(GetTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var taskPage = await context.TaskPages.AsNoTracking()
            .FirstOrDefaultAsync(x => x.TodoTaskId == task.Id, cancellationToken);

        if (task == null || taskPage == null)
        {
            return null;
        }

        return new TaskDto()
        {
            Id = task.Id,
            Name = task.Name,
            Description = taskPage.Description,
            Deadline = task.Deadline,
            IsCompleted = task.IsCompleted,
            TodoListId = task.TodoListId,
        };
    }
}
