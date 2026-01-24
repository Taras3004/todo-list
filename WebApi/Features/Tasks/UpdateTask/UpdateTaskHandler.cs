using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.UpdateTask;

public class UpdateTaskHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<UpdateTaskCommand, TaskDetailsResponse?>
{
    public async Task<TaskDetailsResponse?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var existingTask = await context.Tasks
            .Include(task => task.TaskPage)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (existingTask == null || existingTask.TaskPage == null)
        {
            return null;
        }

        existingTask.Name = request.Name;
        existingTask.Deadline = request.Deadline;
        existingTask.IsCompleted = request.IsCompleted;
        existingTask.TaskPage.Description = request.Description;

        await context.SaveChangesAsync(cancellationToken);

        return new TaskDetailsResponse()
        {
            Id = existingTask.Id,
            Name = existingTask.Name,
            Deadline = existingTask.Deadline,
            IsCompleted = existingTask.IsCompleted,
            Description = existingTask.TaskPage.Description,
            TodoListId = existingTask.TodoListId,
        };
    }
}
