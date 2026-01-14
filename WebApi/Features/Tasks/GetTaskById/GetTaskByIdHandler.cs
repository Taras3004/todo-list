using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.GetTaskById;

public class GetTaskByIdHandler(TodoListDbContext context) : IRequestHandler<GetTaskByIdCommand, TaskDetailsResponse?>
{
    public async Task<TaskDetailsResponse?> Handle(GetTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.AsNoTracking()
            .Include(x => x.TaskPage)
            .Include(x => x.TagToTasks)
                .ThenInclude(x => x.Tag)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (task == null || task.TaskPage == null)
        {
            return null;
        }

        var tagsResponse = task.TagToTasks.Select(tagToTask => new TaskTagResponse
        {
            Id = tagToTask.Tag.Id,
            Tag = tagToTask.Tag.Tag,
            Color = tagToTask.Tag.Color
        }).ToArray();

        return new TaskDetailsResponse()
        {
            Id = task.Id,
            Name = task.Name,
            Deadline = task.Deadline,
            IsCompleted = task.IsCompleted,
            Description = task.TaskPage.Description,
            TodoListId = task.TodoListId,
            TaskTags = tagsResponse,
        };
    }
}
