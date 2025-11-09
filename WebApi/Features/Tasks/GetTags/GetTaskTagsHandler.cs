using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tasks.GetTags;

public class GetTaskTagsHandler(TodoListDbContext context) : IRequestHandler<GetTaskTagsCommand, List<TaskTag>>
{
    public async Task<List<TaskTag>> Handle(GetTaskTagsCommand request, CancellationToken cancellationToken)
    {
        var currentTaskTags = context.TagToTask
            .Where(tagToTask => tagToTask.TodoTaskId == request.TaskId)
            .Join(context.TaskTags, tagToTask => tagToTask.TaskTagId, tag => tag.Id, (task, tag) => new TaskTag()
            {
                Id = tag.Id,
                Tag = tag.Tag,
                UserId = tag.UserId,
            });

        return await currentTaskTags.ToListAsync(cancellationToken);
    }
}
