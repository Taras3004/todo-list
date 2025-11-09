using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.GetTasks;

public class GetTasksHandler(TodoListDbContext context) : IRequestHandler<GetTasksCommand, List<TaskDto>>
{
    public async Task<List<TaskDto>> Handle(GetTasksCommand request, CancellationToken cancellationToken)
    {
        var tasksDto = context.Tasks.Where(task => task.TodoListId == request.TodoListId).Join(context.TaskPages, task => task.Id, page => page.TodoTaskId, (task, page) => new TaskDto()
        {
            Id = task.Id,
            Name = task.Name,
            Deadline = task.Deadline,
            IsCompleted = task.IsCompleted,
            Description = page.Description,
            TodoListId = task.TodoListId,
        });

        return await tasksDto.ToListAsync(cancellationToken);
    }
}
