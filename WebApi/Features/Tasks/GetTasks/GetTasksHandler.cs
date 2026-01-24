using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.GetTasks;

public class GetTasksHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<GetTasksCommand, List<TaskResponse>>
{
    public async Task<List<TaskResponse>> Handle(GetTasksCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var tasksDto = context.Tasks
            .Where(task => task.TodoListId == request.TodoListId && task.UserId == userId)
            .Select(task => new TaskResponse
            {
                Id = task.Id,
                Name = task.Name,
                Deadline = task.Deadline,
                IsCompleted = task.IsCompleted,
                TodoListId = task.TodoListId,
            });

        return await tasksDto.ToListAsync(cancellationToken);
    }
}
