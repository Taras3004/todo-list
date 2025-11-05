using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.TagToTask.GetTagToTaskById;

public class GetTagToTaskByIdHandler(TodoListDbContext context) : IRequestHandler<GetTagToTaskByIdCommand, TagToTaskDto?>
{
    public async Task<TagToTaskDto?> Handle(GetTagToTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var tag = await context.TagToTask.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tag == null)
        {
            return null;
        }

        return new TagToTaskDto()
        {
            Id = tag.Id,
            TodoTaskId = tag.TodoTaskId,
            TaskTagId = tag.TaskTagId,
        };
    }
}
