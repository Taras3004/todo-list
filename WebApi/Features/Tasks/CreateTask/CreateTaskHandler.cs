using MediatR;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.CreateTask;

public class CreateTaskHandler(TodoListDbContext context) : IRequestHandler<CreateTaskCommand, TaskDto>
{
    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TodoTask()
        {
            Name = request.Name,
            TodoListId = request.TodoListId,
            Deadline = request.Deadline,
        };

        var taskPage = new TodoTaskPage()
        {
            Task = task,
            Description = request.Description,
            UserId = "13123",
        };

        context.Tasks.Add(task);
        context.TaskPages.Add(taskPage);

        await context.SaveChangesAsync(cancellationToken);

        return new TaskDto()
        {
            Id = task.Id,
            Name = task.Name,
            Deadline = task.Deadline,
            Description = taskPage.Description,
            IsCompleted = task.IsCompleted,
            TodoListId = task.TodoListId,
        };
    }
}
