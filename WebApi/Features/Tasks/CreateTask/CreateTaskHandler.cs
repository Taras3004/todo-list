using MediatR;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.CreateTask;

public class CreateTaskHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<CreateTaskCommand, TaskResponse>
{
    public async Task<TaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var task = new TodoTask()
        {
            Name = request.Name,
            TodoListId = request.TodoListId,
            Deadline = request.Deadline,
            UserId = userId,
        };

        var taskPage = new TodoTaskPage()
        {
            Task = task,
            Description = request.Description,
            UserId = userId,
        };

        context.Tasks.Add(task);
        context.TaskPages.Add(taskPage);

        await context.SaveChangesAsync(cancellationToken);

        return new TaskResponse()
        {
            Id = task.Id,
            Name = task.Name,
            Deadline = task.Deadline,
            IsCompleted = task.IsCompleted,
            TodoListId = task.TodoListId,
        };
    }
}
