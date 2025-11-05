using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.GetTagById;

public class GetTagByIdHandler(TodoListDbContext context) : IRequestHandler<GetTagByIdCommand, TaskTagDto?>
{
    public async Task<TaskTagDto?> Handle(GetTagByIdCommand request, CancellationToken cancellationToken)
    {
        var tag = await context.TaskTags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tag == null)
        {
            return null;
        }

        return new TaskTagDto()
        {
            Id = tag.Id,
            Tag = tag.Tag,
        };
    }
}
