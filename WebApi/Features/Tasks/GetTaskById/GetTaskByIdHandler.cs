using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.GetTaskById;

public class GetTaskByIdHandler(TodoListDbContext context) : IRequestHandler<GetTaskByIdCommand, TaskResponse?>
{
    public async Task<TaskResponse?> Handle(GetTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var taskPage = await context.TaskPages.AsNoTracking()
            .FirstOrDefaultAsync(x => x.TodoTaskId == task!.Id, cancellationToken);

        if (task == null || taskPage == null)
        {
            return null;
        }

        return new TaskResponse()
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
