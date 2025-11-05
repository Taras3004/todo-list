using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.TagToTask.GetTagToTasks;

public class GetTagToTasksHandler(TodoListDbContext context) : IRequestHandler<GetTagToTasksCommand, List<TagToTaskDto>>
{
    public async Task<List<TagToTaskDto>> Handle(GetTagToTasksCommand request, CancellationToken cancellationToken)
    {
        var tagToTasks = await context.TagToTask.ToListAsync(cancellationToken);

        var tagToTasksDto = new List<TagToTaskDto>();

        foreach (var tag in tagToTasks)
        {
            tagToTasksDto.Add(new TagToTaskDto()
            {
                Id = tag.Id,
                TodoTaskId = tag.TodoTaskId,
                TaskTagId = tag.TaskTagId,
            });
        }

        return tagToTasksDto;
    }
}
